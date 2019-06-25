using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.FirstName
{
    /// <summary>
    /// Create an instance of FirstNameManager
    /// </summary>
    public class FirstNameManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of FirstNameManager
        /// </summary>
        /// <param name="vault"></param>
        public FirstNameManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new FirstName string to be aliased within static vault
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="tags"></param>
        /// <returns>id, firstName, firstNameAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<FirstNameResponse> Create(string firstname, List<string> tags)
        {
            return await this.Create(firstname, null, tags);
        }

        /// <summary>
        /// Create a new FirstName string to be aliased within static vault
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="gender"></param>
        /// <param name="tags"></param>
        /// <returns>id, firstName, firstNameAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<FirstNameResponse> Create(string firstname, string gender = null, List<string> tags = null)
        {
            var result = _vault.Encrypt(firstname);
            var payload = new FirstNameRequest
            {
                FirstName = result.EncryptedData,
                FirstNameHash = _vault.Hash(firstname),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/firstname";
            if (gender != null) url += $"/{gender}";

            var response = await _vault.Client.Post<FirstNameRequest, FirstNameResponse>(url, payload);
            response.FirstName = _vault.Decrypt(response.Iv, response.AuthTag, response.FirstName);
            return response;
        }

        /// <summary>
        /// Retrieve the FirstName string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>id, firstName, firstNameAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<FirstNameResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<FirstNameResponse>($"/vault/static/{_vault.VaultId}/firstname/{aliasId}");
            response.FirstName = _vault.Decrypt(response.Iv, response.AuthTag, response.FirstName);
            return response;
        }

        /// <summary>
        /// Delete the FirstName alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/firstname/{aliasId}");
        }
    }
}
