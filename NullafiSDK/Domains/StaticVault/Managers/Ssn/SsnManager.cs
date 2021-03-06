using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Ssn
{
    /// <summary>
    /// SSN Manager
    /// </summary>
    public class SsnManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of SSN Manager
        /// </summary>
        /// <param name="vault"></param>
        public SsnManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new SSN string to be aliased within static vault
        /// </summary>
        /// <param name="ssn"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, ssn, ssnAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<SsnResponse> Create(string ssn, List<string> tags)
        {
            return await this.Create(ssn, null, tags);
        }

        /// <summary>
        /// Create a new SSN string to be aliased within static vault
        /// </summary>
        /// <param name="ssn"></param>
        /// <param name="state"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, ssn, ssnAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<SsnResponse> Create(string ssn, string state = null, List<string> tags = null)
        {
            var result = _vault.Encrypt(ssn);
            var payload = new SsnRequest
            {
                Ssn = result.EncryptedData,
                SsnHash = _vault.Hash(ssn),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/ssn";
            if (state != null) url += $"/{state}";

            var response = await _vault.Client.Post<SsnRequest, SsnResponse>(url, payload);
            response.Ssn = _vault.Decrypt(response.Iv, response.AuthTag, response.Ssn);
            return response;
        }

        /// <summary>
        /// Retrieve the SSN string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: id, ssn, ssnAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<SsnResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<SsnResponse>($"/vault/static/{_vault.VaultId}/ssn/{aliasId}");
            response.Ssn = _vault.Decrypt(response.Iv, response.AuthTag, response.Ssn);
            return response;
        }

        /// <summary>
        /// Retrieve the SSN alias from real ssn.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="ssn"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<List<SsnResponse>> RetrieveFromRealData(string ssn, List<string> tags = null)
        {
            var hash = this._vault.Hash(ssn);
            var url = $"/vault/static/{_vault.VaultId}/ssn?hash={Uri.EscapeDataString(hash)}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags.Select(item => Uri.EscapeDataString(item)))}";
            }

            var responses = await _vault.Client.Get<List<SsnResponse>>(url);

            foreach (var response in responses)
            {
                response.Ssn = _vault.Decrypt(response.Iv, response.AuthTag, response.Ssn);
            }

            return responses;
        }

        /// <summary>
        /// Delete the SSN alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/ssn/{aliasId}");
        }
    }
}
