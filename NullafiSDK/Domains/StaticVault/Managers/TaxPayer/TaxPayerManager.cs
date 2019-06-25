using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.TaxPayer
{
    /// <summary>
    /// TaxPayer Manager
    /// </summary>
    public class TaxPayerManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of TaxPayer Manager
        /// </summary>
        /// <param name="vault"></param>
        public TaxPayerManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new TaxPayer string to be aliased within static vault
        /// </summary>
        /// <param name="taxpayer"></param>
        /// <param name="tags"></param>
        /// <returns>id, taxPayer, taxPayerAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<TaxPayerResponse> Create(string taxpayer, List<string> tags = null)
        {
            var result = _vault.Encrypt(taxpayer);
            var payload = new TaxPayerRequest
            {
                TaxPayer = result.EncryptedData,
                TaxPayerHash = _vault.Hash(taxpayer),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await _vault.Client.Post<TaxPayerRequest, TaxPayerResponse>($"/vault/static/{_vault.VaultId}/taxpayer", payload);
            response.TaxPayer = _vault.Decrypt(response.Iv, response.AuthTag, response.TaxPayer);
            return response;
        }

        /// <summary>
        /// Retrieve the TaxPayer string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>id, taxPayer, taxPayerAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<TaxPayerResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<TaxPayerResponse>($"/vault/static/{_vault.VaultId}/taxpayer/{aliasId}");
            response.TaxPayer = _vault.Decrypt(response.Iv, response.AuthTag, response.TaxPayer);
            return response;
        }

        /// <summary>
        /// Delete the TaxPayer alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/taxpayer/{aliasId}");
        }
    }
}
