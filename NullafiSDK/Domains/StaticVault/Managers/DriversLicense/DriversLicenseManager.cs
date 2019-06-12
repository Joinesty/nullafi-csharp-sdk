using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.DriversLicense
{
    public class DriversLicenseManager
    {
        private readonly StaticVault _vault;

        public DriversLicenseManager(StaticVault vault)
        {
            _vault = vault;
        }

        public async Task<DriversLicenseResponse> Create(string driversLicense, List<string> tags)
        {
            return await this.Create(driversLicense, null, tags);
        }

        public async Task<DriversLicenseResponse> Create(string driversLicense, string state = null, List<string> tags = null)
        {
            var result = _vault.Encrypt(driversLicense);
            var payload = new DriversLicenseRequest
            {
                DriversLicense = result.EncryptedData,
                DriversLicenseHash = _vault.Hash(driversLicense),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/driverslicense";
            if (state != null) url += $"/{state}";

            var response = await _vault.Client.Post<DriversLicenseRequest, DriversLicenseResponse>(url, payload);
            response.DriversLicense = _vault.Decrypt(response.Iv, response.AuthTag, response.DriversLicense);
            return response;
        }

        public async Task<DriversLicenseResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<DriversLicenseResponse>($"/vault/static/{_vault.VaultId}/driverslicense/{aliasId}");
            response.DriversLicense = _vault.Decrypt(response.Iv, response.AuthTag, response.DriversLicense);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/driverslicense/{aliasId}");
        }
    }
}
