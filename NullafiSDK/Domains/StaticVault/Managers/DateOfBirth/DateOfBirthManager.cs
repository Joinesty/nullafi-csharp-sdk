using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.DateOfBirth
{
    public class DateOfBirthManager
    {
        StaticVault vault;

        public DateOfBirthManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<DateOfBirthResponse> Create(string dateofbirth, List<string> tags)
        {
            var result = this.vault.Encrypt(dateofbirth);
            var payload = new DateOfBirthRequest
            {
                DateOfBirth = result.EncryptedData,
                DateOfBirthHash = this.vault.Hash(dateofbirth),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<DateOfBirthRequest, DateOfBirthResponse>($"/vault/static/${this.vault.VaultId}/dateofbirth", payload);
            response.DateOfBirth = this.vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        public async Task<DateOfBirthResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<DateOfBirthResponse>($"/vault/static/{this.vault.VaultId}/dateofbirth/{aliasId}");
            response.DateOfBirth = this.vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/dateofbirth/{aliasId}");
        }
    }
}
