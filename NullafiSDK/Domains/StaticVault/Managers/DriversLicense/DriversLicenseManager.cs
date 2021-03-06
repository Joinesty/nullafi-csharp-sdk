using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.DriversLicense
{
    /// <summary>
    /// DateOfBirthManager
    /// </summary>
    public class DriversLicenseManager
    {
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of DriversLicenseManager
        /// </summary>
        /// <param name="vault"></param>
        public DriversLicenseManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new DriversLicense string to be aliased within static vault
        /// </summary>
        /// <param name="driversLicense"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, driversLicense, driversLicenseAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<DriversLicenseResponse> Create(string driversLicense, List<string> tags)
        {
            return await this.Create(driversLicense, null, tags);
        }

        /// <summary>
        /// Create a new DriversLicense string to be aliased within static vault
        /// </summary>
        /// <param name="driversLicense"></param>
        /// <param name="state"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, driversLicense, driversLicenseAlias, tags, iv, authTag, tags, createdAt</returns>
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

        /// <summary>
        /// Retrieve the DriversLicense string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: id, driversLicense, driversLicenseAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<DriversLicenseResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<DriversLicenseResponse>($"/vault/static/{_vault.VaultId}/driverslicense/{aliasId}");
            response.DriversLicense = _vault.Decrypt(response.Iv, response.AuthTag, response.DriversLicense);
            return response;
        }

        /// <summary>
        /// Retrieve the Drivers License alias from real drivers license.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="driversLicense"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<List<DriversLicenseResponse>> RetrieveFromRealData(string driversLicense, List<string> tags = null)
        {
            var hash = this._vault.Hash(driversLicense);
            var url = $"/vault/static/{_vault.VaultId}/driverslicense?hash={Uri.EscapeDataString(hash)}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags.Select(item => Uri.EscapeDataString(item)))}";
            }

            var responses = await _vault.Client.Get<List<DriversLicenseResponse>>(url);

            foreach (var response in responses)
            {
                response.DriversLicense = _vault.Decrypt(response.Iv, response.AuthTag, response.DriversLicense);
            }

            return responses;
        }

        /// <summary>
        /// Delete the DriversLicense alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/driverslicense/{aliasId}");
        }
    }
}
