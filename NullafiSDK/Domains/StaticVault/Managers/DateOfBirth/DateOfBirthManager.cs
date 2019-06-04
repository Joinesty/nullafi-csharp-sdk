using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.DateOfBirth
{
    public class DateOfBirthManager
    {
        StaticVault vault;

        public DateOfBirthManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<DateOfBirthModel> Create(string dateofbirth, List<string> tags)
        {
            var result = this.vault.Encrypt(dateofbirth);
            var payload = new DateOfBirthModel
            {
                DateOfBirth = result.EncryptedData,
                DateOfBirthHash = this.vault.Hash(dateofbirth),
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<DateOfBirthModel, DateOfBirthModel>($"/vault/static/${this.vault.VaultId}/dateofbirth", payload);
            response.DateOfBirth = this.vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        public async Task<DateOfBirthModel> Retrieve(string tokenId)
        {
            var response = await this.vault.client.Get<DateOfBirthModel>($"/vault/static/{this.vault.VaultId}/dateofbirth/{tokenId}");
            response.DateOfBirth = this.vault.Decrypt(response.Iv, response.AuthTag, response.DateOfBirth);
            return response;
        }

        public async void Delete(string tokenId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/dateofbirth/{tokenId}");
        }
    }
}
