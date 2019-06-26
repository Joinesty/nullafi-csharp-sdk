using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.LastName
{
    /// <summary>
    /// LastNameManager
    /// </summary>
    public class LastNameManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of LastNameManager
        /// </summary>
        /// <param name="vault"></param>
        public LastNameManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new LastName string to be aliased within static vault
        /// </summary>
        /// <param name="lastname"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, lastName, lastNameAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<LastNameResponse> Create(string lastname, List<string> tags)
        {
            return await this.Create(lastname, null, tags);
        }

        /// <summary>
        /// Create a new LastName string to be aliased within static vault
        /// </summary>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, lastName, lastNameAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<LastNameResponse> Create(string lastname, string gender = null, List<string> tags = null)
        {
            var result = _vault.Encrypt(lastname);
            var payload = new LastNameRequest
            {
                LastName = result.EncryptedData,
                LastNameHash = _vault.Hash(lastname),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/lastname";
            if (gender != null) url += $"/{gender}";

            var response = await _vault.Client.Post<LastNameRequest, LastNameResponse>(url, payload);
            response.LastName = _vault.Decrypt(response.Iv, response.AuthTag, response.LastName);
            return response;
        }

        /// <summary>
        /// Retrieve the LastName string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: id, lastName, lastNameAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<LastNameResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<LastNameResponse>($"/vault/static/{_vault.VaultId}/lastname/{aliasId}");
            response.LastName = _vault.Decrypt(response.Iv, response.AuthTag, response.LastName);
            return response;
        }

        /// <summary>
        /// Retrieve the Last Name alias from real last name.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<List<LastNameResponse>> RetrieveFromRealData(string lastName, List<string> tags = null)
        {
            var hash = this._vault.Hash(lastName);
            var url = $"/vault/static/lastname?hash={hash}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags)}";
            }

            var responses = await _vault.Client.Get<List<LastNameResponse>>(url);

            foreach (var response in responses)
            {
                response.LastName = _vault.Decrypt(response.Iv, response.AuthTag, response.LastName);
            }

            return responses;
        }

        /// <summary>
        /// Delete the LastName alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/lastname/{aliasId}");
        }
    }
}
