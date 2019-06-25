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
        public GenericManager(StaticVault vault)
        {
          _vault = vault;
        }

        /// <summary>
        /// Create a new Gender string to be aliased within static vault
        /// </summary>
        /// <param name="data"></param>
        /// <param name="regexTemplate"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, generic, genericAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<GenericResponse> Create(string data, string regexTemplate, List<string> tags = null)
        {   
            var result = _vault.Encrypt(data);
            var payload = new GenericRequest
            {
                Data = result.EncryptedData,
                DataHash = _vault.Hash(data),
                Template = regexTemplate,
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await _vault.Client.Post<GenericRequest, GenericResponse>($"/vault/static/{_vault.VaultId}/generic", payload);
            response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            return response;
        }

        /// <summary>
        /// Retrieve the Generic string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: id, generic, genericAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<GenericResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<GenericResponse>($"/vault/static/{_vault.VaultId}/generic/{aliasId}");
            response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            return response;
        }

        /// <summary>
        /// Retrieve the Generic alias from real data.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<GenericResponse> RetrieveFromRealData(string data, List<string> tags = null)
        {
            var hash = this._vault.Hash(data);
            var url = $"/vault/static/generic?hash={hash}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags)}";
            }

            var response = await _vault.Client.Get<GenericResponse>(url);
            response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            return response;
        }

        /// <summary>
        /// Delete the Generic alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/gender/{aliasId}");
        }
    }
}
