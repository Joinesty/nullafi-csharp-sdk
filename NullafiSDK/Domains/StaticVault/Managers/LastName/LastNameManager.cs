using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.LastName
{
    public class LastNameManager
    {
        StaticVault vault;

        public LastNameManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<LastNameResponse> Create(string lastname, List<string> tags, string gender)
        {
            var result = this.vault.Encrypt(lastname);
            var payload = new LastNameRequest
            {
                LastName = result.EncryptedData,
                LastNameHash = this.vault.Hash(lastname),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            String url = $"/vault/static/{this.vault.VaultId}/lastname";
            if (gender != null) url += $"/{gender}";

            var response = await this.vault.client.Post<LastNameRequest, LastNameResponse>(url, payload);
            response.LastName = this.vault.Decrypt(response.Iv, response.AuthTag, response.LastName);
            return response;
        }

        public async Task<LastNameResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<LastNameResponse>($"/vault/static/{this.vault.VaultId}/lastname/{aliasId}");
            response.LastName = this.vault.Decrypt(response.Iv, response.AuthTag, response.LastName);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/lastname/{aliasId}");
        }
    }
}
