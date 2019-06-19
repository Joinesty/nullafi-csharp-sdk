using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Passport
{
    /// <summary>
    /// Passport Manager
    /// </summary>
    public class PassportManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of Passport Manager
        /// </summary>
        /// <param name="vault"></param>
        /// <returns></returns>
        public PassportManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new LastName string to be aliased within static vault
        /// </summary>
        /// <param name="passport"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<PassportResponse> Create(string passport, List<string> tags = null)
        {
            var result = _vault.Encrypt(passport);
            var payload = new PassportRequest
            {
                Passport = result.EncryptedData,
                PassportHash = _vault.Hash(passport),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await _vault.Client.Post<PassportRequest, PassportResponse>($"/vault/static/{_vault.VaultId}/passport", payload);
            response.Passport = _vault.Decrypt(response.Iv, response.AuthTag, response.Passport);
            return response;
        }

        /// <summary>
        /// Retrieve the Passport string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task<PassportResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<PassportResponse>($"/vault/static/{_vault.VaultId}/passport/{aliasId}");
            response.Passport = _vault.Decrypt(response.Iv, response.AuthTag, response.Passport);
            return response;
        }

        /// <summary>
        /// Delete the Passport alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/passport/{aliasId}");
        }
    }
}
