using System;
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
        /// <returns></returns>
        public SsnManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new SSN string to be aliased within static vault
        /// </summary>
        /// <param name="ssn"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// Retrieve the SSN string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task<SsnResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<SsnResponse>($"/vault/static/{_vault.VaultId}/ssn/{aliasId}");
            response.Ssn = _vault.Decrypt(response.Iv, response.AuthTag, response.Ssn);
            return response;
        }

        /// <summary>
        /// Delete the SSN alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/ssn/{aliasId}");
        }
    }
}
