using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.FirstName
{
    public class FirstNameManager
    {
        StaticVault vault;

        public FirstNameManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<FirstNameModel> Create(string firstname, List<string> tags)
        {
            var result = this.vault.Encrypt(firstname);
            var payload = new FirstNameModel
            {
                FirstName = result.EncryptedData,
                FirstNameHash = this.vault.Hash(firstname),
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<FirstNameModel, FirstNameModel>($"/vault/static/${this.vault.VaultId}/firstname", payload);
            response.FirstName = this.vault.Decrypt(response.Iv, response.AuthTag, response.FirstName);
            return response;
        }

        public async Task<FirstNameModel> Retrieve(string tokenId)
        {
            var response = await this.vault.client.Get<FirstNameModel>($"/vault/static/{this.vault.VaultId}/firstname/{tokenId}");
            response.FirstName = this.vault.Decrypt(response.Iv, response.AuthTag, response.FirstName);
            return response;
        }

        public async void Delete(string tokenId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/firstname/{tokenId}");
        }
    }
}
