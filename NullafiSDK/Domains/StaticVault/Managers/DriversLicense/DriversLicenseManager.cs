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

        public async Task<DateOfBirthModel> Create(string address, List<string> tags)
        {
            var result = this.vault.Encrypt(address);
            var payload = new DateOfBirthModel
            {
                Address = result.EncryptedData,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<DateOfBirthModel, DateOfBirthModel>($"/vault/static/${this.vault.VaultId}/dateofbirth", payload);
            return response;
        }

        public async Task<DateOfBirthModel> Retrieve(string tokenId)
        {
            return await this.vault.client.Get<DateOfBirthModel>($"/vault/static/{this.vault.VaultId}/dateofbirth/{tokenId}");
        }

        public async void Delete(string tokenId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/dateofbirth/{tokenId}");
        }
    }
}
