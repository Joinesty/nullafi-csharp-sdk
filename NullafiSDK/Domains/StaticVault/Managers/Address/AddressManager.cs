using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.Address
{
    public class AddressManager
    {
        StaticVault vault;

        public AddressManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<AddressModel> Create(string address, List<string> tags)
        {
            var result = this.vault.Encrypt(address);
            var payload = new AddressModel
            {
                Address = result.EncryptedData,
                AddressHash = this.vault.Hash(address),
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<AddressModel, AddressModel>($"/vault/static/${this.vault.VaultId}/address", payload);
            response.Address = this.vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            return response;
        }

        public async Task<AddressModel> Retrieve(string tokenId)
        {
            var response = await this.vault.client.Get<AddressModel>($"/vault/static/{this.vault.VaultId}/address/{tokenId}");
            response.Address = this.vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            return response;
        }

        public async void Delete(string tokenId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/address/{tokenId}");
        }
    }
}
