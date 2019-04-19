using NullafiSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NullafiSDK.Managers.Tokens
{
    public class AddressManager
    {
        StaticVault vault;

        public AddressManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<AddressModel> PostAddress(string address, List<string> tags)
        {
            var result = this.vault.Encrypt(address);
            var payload = new AddressModel
            {
                Address = result.EncryptedData,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<AddressModel, AddressModel>($"/vault/static/${this.vault.VaultId}/address", payload);
            response.Address = this.vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            return response;
        }

        public async Task<AddressModel> GetAddress(string tokenId)
        {
            return await this.vault.client.Get<AddressModel>($"/vault/static/{this.vault.VaultId}/address/{tokenId}");
        }

        public async void DeleteAddress(string tokenId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/address/{tokenId}");
        }
    }
}
