using System;
using System.Linq;
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
        public RandomManager(StaticVault vault)
        {
          _vault = vault;
        }

        /// <summary>
        /// Create a new Random string to be aliased within static vault
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, random, randomAlias, tags, iv, authTag, tags, createdAt</returns>
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
        /// Retrieve the Random string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: id, random, randomAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<RandomResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<RandomResponse>($"/vault/static/{_vault.VaultId}/random/{aliasId}");
            response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            return response;
        }

        /// <summary>
        /// Retrieve the Random alias from real data.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<List<RandomResponse>> RetrieveFromRealData(string data, List<string> tags = null)
        {
            var hash = this._vault.Hash(data);
            var url = $"/vault/static/{_vault.VaultId}/random?hash={Uri.EscapeDataString(hash)}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags.Select(item => Uri.EscapeDataString(item)))}";
            }

            var responses = await _vault.Client.Get<List<RandomResponse>>(url);

            foreach (var response in responses)
            {
                response.Data = _vault.Decrypt(response.Iv, response.AuthTag, response.Data);
            }

            return responses;
        }

        /// <summary>
        /// Delete the Random alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/random/{aliasId}");
        }
    }
}
