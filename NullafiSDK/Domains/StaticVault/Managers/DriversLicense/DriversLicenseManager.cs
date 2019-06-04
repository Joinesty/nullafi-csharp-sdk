using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.DriversLicense
{
    public class DriversLicenseManager
    {
        StaticVault vault;

        public DriversLicenseManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<DriversLicenseModel> Create(string driversLicense, List<string> tags)
        {
            var result = this.vault.Encrypt(driversLicense);
            var payload = new DriversLicenseModel
            {
                DriversLicense = result.EncryptedData,
                DriversLicenseHash = this.vault.Hash(driversLicense),
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<DriversLicenseModel, DriversLicenseModel>($"/vault/static/${this.vault.VaultId}/driverslicense", payload);
            response.DriversLicense = this.vault.Decrypt(response.Iv, response.AuthTag, response.DriversLicense);
            return response;
        }

        public async Task<DriversLicenseModel> Retrieve(string tokenId)
        {
            var response = await this.vault.client.Get<DriversLicenseModel>($"/vault/static/{this.vault.VaultId}/driverslicense/{tokenId}");
            response.DriversLicense = this.vault.Decrypt(response.Iv, response.AuthTag, response.DriversLicense);
            return response;
        }

        public async void Delete(string tokenId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/driverslicense/{tokenId}");
        }
    }
}
