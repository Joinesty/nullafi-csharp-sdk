using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.CommunicationVault.Managers.Email
{
    /// <summary>
    /// EmailManager
    /// </summary>
    public class EmailManager
    {
        private readonly CommunicationVault _vault;

        /// <summary>
        /// Create an instance of EmailManager
        /// </summary>
        /// <param name="vault"></param>
        public EmailManager(CommunicationVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new Email to be aliased within communication vault
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieve the Email alias from a communication vault. Returns an array of matching values. Array will be sorted by date created.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task<EmailResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<EmailResponse>($"/vault/communication/{_vault.VaultId}/email/{aliasId}");
            response.Email = _vault.Decrypt(response.Iv, response.AuthTag, response.Email);
            return response;
        }

        /// <summary>
        /// Retrieve the Email alias from real email.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<List<EmailResponse>> RetrieveFromRealData(string email, List<string> tags = null)
        {
            var hash = this._vault.Hash(email);
            var url = $"/vault/communication/email?hash={hash}";
            
            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags)}";
            }

            var responses = await _vault.Client.Get<List<EmailResponse>>(url);

            foreach (var response in responses)
            {
                response.Email = _vault.Decrypt(response.Iv, response.AuthTag, response.Email);
            }

            return responses;
        }

        /// <summary>
        /// Delete the Email alias from communication vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/communication/{_vault.VaultId}/email/{aliasId}");
        }
    }
}
