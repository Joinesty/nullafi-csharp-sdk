using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NullafiSDK.Domains.CommunicationVault.Managers.Email
{
    public class EmailManager
    {
        CommunicationVault vault;

        public EmailManager(CommunicationVault vault)
        {
            this.vault = vault;
        }

        public async Task<EmailModel> Create(string email, List<string> tags)
        {
            var result = this.vault.Encrypt(email);
            var payload = new EmailModel
            {
                Email = result.EncryptedData,
                EmailHash = this.vault.Hash(email),
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<EmailModel, EmailModel>($"/vault/communication/${this.vault.VaultId}/email", payload);
            response.Email = this.vault.Decrypt(response.Iv, response.AuthTag, response.Email);
            return response;
        }

        public async Task<EmailModel> Retrieve(string tokenId)
        {
            var response = await this.vault.client.Get<EmailModel>($"/vault/communication/{this.vault.VaultId}/email/{tokenId}");
            response.Email = this.vault.Decrypt(response.Iv, response.AuthTag, response.Email);
            return response;
        }

        public async void Delete(string tokenId)
        {
            await this.vault.client.Delete($"/vault/communication/{this.vault.VaultId}/email/{tokenId}");
        }
    }
}
