using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.FirstName
{
    public class FirstNameManager
    {
        StaticVault vault;

        public FirstNameManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<FirstNameResponse> Create(string firstname, List<string> tags)
        {
            var result = this.vault.Encrypt(firstname);
            var payload = new FirstNameRequest
            {
                FirstName = result.EncryptedData,
                FirstNameHash = this.vault.Hash(firstname),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<FirstNameRequest, FirstNameResponse>($"/vault/static/{this.vault.VaultId}/firstname", payload);
            response.FirstName = this.vault.Decrypt(response.Iv, response.AuthTag, response.FirstName);
            return response;
        }

        public async Task<FirstNameResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<FirstNameResponse>($"/vault/static/{this.vault.VaultId}/firstname/{aliasId}");
            response.FirstName = this.vault.Decrypt(response.Iv, response.AuthTag, response.FirstName);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/firstname/{aliasId}");
        }
    }
}
