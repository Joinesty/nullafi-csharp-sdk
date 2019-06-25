using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Race
{
    /// <summary>
    /// Race Manager
    /// </summary>

    public class RaceManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of Race Manager
        /// </summary>
        /// <param name="vault"></param>
        /// <returns></returns>
        public RaceManager(StaticVault vault)
        {
          _vault = vault;
        }

        /// <summary>
        /// Create a new Race string to be aliased within static vault
        /// </summary>
        /// <param name="race"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<RaceResponse> Create(string race, List<string> tags = null)
            {
            var result = _vault.Encrypt(race);
            var payload = new RaceRequest
            {
            Race = result.EncryptedData,
            RaceHash = _vault.Hash(race),
            Tags = tags,
            Iv = result.Iv,
            AuthTag = result.AuthTag
            };

            var response = await _vault.Client.Post<RaceRequest, RaceResponse>($"/vault/static/{_vault.VaultId}/race", payload);
            response.Race = _vault.Decrypt(response.Iv, response.AuthTag, response.Race);
            return response;
          }

        /// <summary>
        /// Retrieve the race string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task<RaceResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<RaceResponse>($"/vault/static/{_vault.VaultId}/race/{aliasId}");
            response.Race = _vault.Decrypt(response.Iv, response.AuthTag, response.Race);
            return response;
        }

        /// <summary>
        /// Retrieve the Race alias from real race.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="race"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<List<RaceResponse>> RetrieveFromRealData(string race, List<string> tags = null)
        {
            var hash = this._vault.Hash(race);
            var url = $"/vault/static/race?hash={hash}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags)}";
            }

            var responses = await _vault.Client.Get<List<RaceResponse>>(url);

            foreach (var response in responses)
            {
                response.Race = _vault.Decrypt(response.Iv, response.AuthTag, response.Race);
            }

            return responses;
        }

        /// <summary>
        /// Delete the Race alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/race/{aliasId}");
        }
    }
}
