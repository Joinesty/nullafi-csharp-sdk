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
        public PlaceOfBirthManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new PlaceOfBirth string to be aliased within static vault
        /// </summary>
        /// <param name="placeofbirth"></param>
        /// <param name="tags"></param>
        /// <returns>id, placeOfBirth, placeOfBirthAlias, tags, iv, authTag, tags, createdAt</returns>
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
        /// <returns>id, placeOfBirth, placeOfBirthAlias, tags, iv, authTag, tags, createdAt</returns>
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
        /// Retrieve the PlaceOfBirth string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>id, placeOfBirth, placeOfBirthAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<PlaceOfBirthResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<PlaceOfBirthResponse>($"/vault/static/{_vault.VaultId}/placeofbirth/{aliasId}");
            response.PlaceOfBirth = _vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
            return response;
        }

        /// <summary>
        /// Delete the PlaceOfBirth alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/placeofbirth/{aliasId}");
        }
    }
}
