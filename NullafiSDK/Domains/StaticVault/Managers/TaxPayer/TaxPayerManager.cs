using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.TaxPayer
{
    public class TaxPayerManager
    {
        private readonly StaticVault _vault;

        public TaxPayerManager(StaticVault vault)
        {
            _vault = vault;
        }

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

        public async Task<TaxPayerResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<TaxPayerResponse>($"/vault/static/{_vault.VaultId}/taxpayer/{aliasId}");
            response.TaxPayer = _vault.Decrypt(response.Iv, response.AuthTag, response.TaxPayer);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/taxpayer/{aliasId}");
        }
    }
}
