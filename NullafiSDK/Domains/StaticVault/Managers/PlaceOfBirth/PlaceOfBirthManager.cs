using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.PlaceOfBirth
{
    /// <summary>
    /// PlaceOfBirth Manager
    /// </summary>
    public class PlaceOfBirthManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of PlaceOfBirth Manager
        /// </summary>
        /// <param name="vault"></param>
        /// <returns></returns>
        public PlaceOfBirthManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new PlaceOfBirth string to be aliased within static vault
        /// </summary>
        /// <param name="placeofbirth"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<PlaceOfBirthResponse> Create(string placeofbirth, List<string> tags)
        {
            return await this.Create(placeofbirth, null, tags);
        }

        /// <summary>
        /// Create a new PlaceOfBirth string to be aliased within static vault
        /// </summary>
        /// <param name="placeofbirth"></param>
        /// <param name="state"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<PlaceOfBirthResponse> Create(string placeofbirth, string state = null, List<string> tags = null)
        {
            var result = _vault.Encrypt(placeofbirth);
            var payload = new PlaceOfBirthRequest
            {
                PlaceOfBirth = result.EncryptedData,
                PlaceOfBirthHash = _vault.Hash(placeofbirth),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/placeofbirth";
            if (state != null) url += $"/{state}";

            var response = await _vault.Client.Post<PlaceOfBirthRequest, PlaceOfBirthResponse>(url, payload);
            response.PlaceOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
            return response;
        }

        /// <summary>
        /// Retrieve the PlaceOfBirth string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task<PlaceOfBirthResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<PlaceOfBirthResponse>($"/vault/static/{_vault.VaultId}/placeofbirth/{aliasId}");
            response.PlaceOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
            return response;
        }

        /// <summary>
        /// Retrieve the Place of Birth alias from real place of birth.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="placeOfBirth"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<List<PlaceOfBirthResponse>> RetrieveFromRealData(string placeOfBirth, List<string> tags = null)
        {
            var hash = this._vault.Hash(placeOfBirth);
            var url = $"/vault/static/placeofbirth?hash={hash}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags)}";
            }

            var responses = await _vault.Client.Get<List<PlaceOfBirthResponse>>(url);

            foreach (var response in responses)
            {
                response.PlaceOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
            }

            return responses;
        }

        /// <summary>
        /// Delete the PlaceOfBirth alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/placeofbirth/{aliasId}");
        }
    }
}
