using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.CommunicationVault.Managers.Email
{
    public class EmailManager
    {
        private readonly CommunicationVault _vault;

        public EmailManager(CommunicationVault vault)
        {
            _vault = vault;
        }

        public async Task<EmailResponse> Create(string email, List<string> tags = null)
        {
            var result = _vault.Encrypt(email);
            var payload = new EmailRequest
            {
                Email = result.EncryptedData,
                EmailHash = _vault.Hash(email),
                Iv = result.Iv,
                AuthTag = result.AuthTag,
                Tags = tags
            };

            var response = await _vault.Client.Post<EmailRequest, EmailResponse>($"/vault/communication/{_vault.VaultId}/email", payload);
            response.Email = _vault.Decrypt(response.Iv, response.AuthTag, response.Email);
            return response;
        }

        public async Task<EmailResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<EmailResponse>($"/vault/communication/{_vault.VaultId}/email/{aliasId}");
            response.Email = _vault.Decrypt(response.Iv, response.AuthTag, response.Email);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/communication/{_vault.VaultId}/email/{aliasId}");
        }
    }
}
