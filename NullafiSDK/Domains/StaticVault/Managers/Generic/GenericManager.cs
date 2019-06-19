using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Generic
{
    /// <summary>
    /// GenericManager
    /// </summary>
    public class GenericManager
{
    private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of GenericManager
        /// </summary>
        /// <param name="vault"></param>
        /// <returns></returns>
        public GenericManager(StaticVault vault)
        {
          _vault = vault;
        }

        /// <summary>
        /// Create a new Gender string to be aliased within static vault
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<GenericResponse> Create(string data, List<string> tags = null)
        {   
            var result = _vault.Encrypt(data);
            var payload = new GenericRequest
            {
                Data = result.EncryptedData,
                DataHash = _vault.Hash(data),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await _vault.Client.Post<GenericRequest, GenericResponse>($"/vault/static/{_vault.VaultId}/generic", payload);
            response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            return response;
        }

        /// <summary>
        /// Retrieve the Generic string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task<GenericResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<GenericResponse>($"/vault/static/{_vault.VaultId}/generic/{aliasId}");
            response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            return response;
        }

        /// <summary>
        /// Delete the Generic alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/gender/{aliasId}");
        }
    }
}
