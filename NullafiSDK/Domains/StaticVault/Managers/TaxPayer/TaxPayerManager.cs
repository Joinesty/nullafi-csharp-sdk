using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.TaxPayer
{
    public class TaxPayerManager
    {
        StaticVault vault;

        public TaxPayerManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<TaxPayerResponse> Create(string taxpayer, List<string> tags)
        {
            var result = this.vault.Encrypt(taxpayer);
            var payload = new TaxPayerRequest
            {
                TaxPayer = result.EncryptedData,
                TaxPayerHash = this.vault.Hash(taxpayer),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<TaxPayerRequest, TaxPayerResponse>($"/vault/static/${this.vault.VaultId}/taxpayer", payload);
            response.TaxPayer = this.vault.Decrypt(response.Iv, response.AuthTag, response.TaxPayer);
            return response;
        }

        public async Task<TaxPayerResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<TaxPayerResponse>($"/vault/static/{this.vault.VaultId}/taxpayer/{aliasId}");
            response.TaxPayer = this.vault.Decrypt(response.Iv, response.AuthTag, response.TaxPayer);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/taxpayer/{aliasId}");
        }
    }
}
