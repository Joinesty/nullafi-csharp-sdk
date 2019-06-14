using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Random
{
    /// <summary>
    /// Random Manager
    /// </summary>
    public class RandomManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of Random Manager
        /// </summary>
        /// <param name="vault"></param>
        /// <returns></returns>
        public RandomManager(StaticVault vault)
        {
          _vault = vault;
        }

        /// <summary>
        /// Create a new Random string to be aliased within static vault
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<RandomResponse> Create(string data, List<string> tags = null)
        {
            var result = _vault.Encrypt(data);
            var payload = new RandomRequest
            {
              Data = result.EncryptedData,
              DataHash = _vault.Hash(data),
              Tags = tags,
              Iv = result.Iv,
              AuthTag = result.AuthTag
            };

            var response = await _vault.Client.Post<RandomRequest, RandomResponse>($"/vault/static/{_vault.VaultId}/random", payload);
            response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            return response;
        }

        /// <summary>
        /// Retrieve the Random string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task<RandomResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<RandomResponse>($"/vault/static/{_vault.VaultId}/random/{aliasId}");
            response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            return response;
        }

        /// <summary>
        /// Delete the Random alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/random/{aliasId}");
        }
    }
}
