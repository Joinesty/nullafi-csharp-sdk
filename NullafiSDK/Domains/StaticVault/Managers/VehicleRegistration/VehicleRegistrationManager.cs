using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.VehicleRegistration
{
    /// <summary>
    /// Create an instance of VehicleRegistration Manager
    /// </summary>
    public class VehicleRegistrationManager
    {   
        private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of VehicleRegistration Manager
        /// </summary>
        /// <param name="vault"></param>
        public VehicleRegistrationManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new VehicleRegistration string to be aliased within static vault
        /// </summary>
        /// <param name="vehicleregistration"></param>
        /// <param name="tags"></param>
        /// <returns>Returns a promise containing: id, vehicleRegistration, vehicleRegistrationAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<VehicleRegistrationResponse> Create(string vehicleregistration, List<string> tags = null)
        {
            var result = _vault.Encrypt(vehicleregistration);
            var payload = new VehicleRegistrationRequest
            {
              VehicleRegistration = result.EncryptedData,
              VehicleRegistrationHash = _vault.Hash(vehicleregistration),
              Iv = result.Iv,
              AuthTag = result.AuthTag
            };

            var response = await _vault.Client.Post<VehicleRegistrationRequest, VehicleRegistrationResponse>($"/vault/static/{_vault.VaultId}/vehicleregistration", payload);
            response.VehicleRegistration = _vault.Decrypt(response.Iv, response.AuthTag, response.VehicleRegistration);
            return response;
        }

        /// <summary>
        /// Retrieve the VehicleRegistration string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: id, vehicleRegistration, vehicleRegistrationAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<VehicleRegistrationResponse> Retrieve(string aliasId)
        {
            return await _vault.Client.Get<VehicleRegistrationResponse>($"/vault/static/{_vault.VaultId}/vehicleregistration/{aliasId}");
        }

        /// <summary>
        /// Retrieve the VehicleRegistration alias from real VehicleRegistration.
        /// Real value must be an exact match and will also be case sensitive.
        /// Returns an array of matching values.Array will be sorted by date created.
        /// </summary>
        /// <param name="taxpayer"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<List<VehicleRegistrationResponse>> RetrieveFromRealData(string vehicleregistration, List<string> tags = null)
        {
            var hash = this._vault.Hash(vehicleregistration);
            var url = $"/vault/static/vehicleregistration?hash={hash}";

            if (tags != null)
            {
                url += $"&tags={string.Join("&tags=", tags)}";
            }

            var responses = await _vault.Client.Get<List<VehicleRegistrationResponse>>(url);

            foreach (var response in responses)
            {
                response.VehicleRegistration = _vault.Decrypt(response.Iv, response.AuthTag, response.VehicleRegistration);
            }

            return responses;
        }

        /// <summary>
        /// Delete the VehicleRegistration alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>Returns a promise containing: ok</returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/vehicleregistration/{aliasId}");
        }
    }
}
