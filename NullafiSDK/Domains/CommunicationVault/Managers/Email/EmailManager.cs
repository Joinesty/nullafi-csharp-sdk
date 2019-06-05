using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nullafi.Domains.CommunicationVault.Managers.Email
{
    public class EmailManager
    {
        CommunicationVault vault;

        public EmailManager(CommunicationVault vault)
        {
            this.vault = vault;
        }

        public async Task<EmailResponse> Create(string email, List<string> tags)
        {
            var result = this.vault.Encrypt(email);
            var payload = new EmailRequest
            {
                Email = result.EncryptedData,
                EmailHash = this.vault.Hash(email),
                Iv = result.Iv,
                AuthTag = result.AuthTag,
                Tags = tags
            };

            var response = await this.vault.client.Post<EmailRequest, EmailResponse>($"/vault/communication/${this.vault.VaultId}/email", payload);
            response.Email = this.vault.Decrypt(response.Iv, response.AuthTag, response.Email);
            return response;
        }

        public async Task<EmailResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<EmailResponse>($"/vault/communication/{this.vault.VaultId}/email/{aliasId}");
            response.Email = this.vault.Decrypt(response.Iv, response.AuthTag, response.Email);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/communication/{this.vault.VaultId}/email/{aliasId}");
        }
    }
}
