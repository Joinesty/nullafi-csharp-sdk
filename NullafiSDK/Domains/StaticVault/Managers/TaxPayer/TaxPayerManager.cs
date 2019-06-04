using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.TaxPayer
{
    public class TaxPayerManager
    {
        StaticVault vault;

        public TaxPayerManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<TaxPayerModel> Create(string taxpayer, List<string> tags)
        {
            var result = this.vault.Encrypt(taxpayer);
            var payload = new TaxPayerModel
            {
                TaxPayer = result.EncryptedData,
                TaxPayerHash = this.vault.Hash(taxpayer),
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<TaxPayerModel, TaxPayerModel>($"/vault/static/${this.vault.VaultId}/taxpayer", payload);
            response.TaxPayer = this.vault.Decrypt(response.Iv, response.AuthTag, response.TaxPayer);
            return response;
        }

        public async Task<TaxPayerModel> Retrieve(string tokenId)
        {
            var response = await this.vault.client.Get<TaxPayerModel>($"/vault/static/{this.vault.VaultId}/taxpayer/{tokenId}");
            response.TaxPayer = this.vault.Decrypt(response.Iv, response.AuthTag, response.TaxPayer);
            return response;
        }

        public async void Delete(string tokenId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/taxpayer/{tokenId}");
        }
    }
}
