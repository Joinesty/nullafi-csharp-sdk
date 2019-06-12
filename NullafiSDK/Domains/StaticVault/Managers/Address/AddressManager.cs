using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.Address
{
    public class AddressManager
    {
        StaticVault vault;

        public AddressManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<AddressResponse> Create(string address, List<string> tags)
        {
            return await this.Create(address, null, tags);
        }

        public async Task<AddressResponse> Create(string address, string state = null, List<string> tags = null)
        {
            var result = this.vault.Encrypt(address);
            var payload = new AddressRequest
            {
                Address = result.EncryptedData,
                AddressHash = this.vault.Hash(address),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{this.vault.VaultId}/address";
            if (state != null) url += $"/{state}";

            var response = await this.vault.client.Post<AddressRequest, AddressResponse>(url, payload);
            response.Address = this.vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            return response;
        }

        public async Task<AddressResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<AddressResponse>($"/vault/static/{this.vault.VaultId}/address/{aliasId}");
            response.Address = this.vault.Decrypt(response.Iv, response.AuthTag, response.Address);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/address/{aliasId}");
        }
    }
}
