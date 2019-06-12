using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.DriversLicense
{
    public class DriversLicenseManager
    {
        StaticVault vault;

        public DriversLicenseManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<DriversLicenseResponse> Create(string driversLicense, List<string> tags, string state)
        {
            var result = this.vault.Encrypt(driversLicense);
            var payload = new DriversLicenseRequest
            {
                DriversLicense = result.EncryptedData,
                DriversLicenseHash = this.vault.Hash(driversLicense),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            String url = $"/vault/static/{this.vault.VaultId}/driverslicense";
            if (state != null) url += $"/{state}";

            var response = await this.vault.client.Post<DriversLicenseRequest, DriversLicenseResponse>(url, payload);
            response.DriversLicense = this.vault.Decrypt(response.Iv, response.AuthTag, response.DriversLicense);
            return response;
        }

        public async Task<DriversLicenseResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<DriversLicenseResponse>($"/vault/static/{this.vault.VaultId}/driverslicense/{aliasId}");
            response.DriversLicense = this.vault.Decrypt(response.Iv, response.AuthTag, response.DriversLicense);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/driverslicense/{aliasId}");
        }
    }
}
