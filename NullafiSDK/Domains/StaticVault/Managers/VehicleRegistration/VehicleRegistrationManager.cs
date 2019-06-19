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
        /// <returns></returns>
        public VehicleRegistrationManager(StaticVault vault)
        {
            _vault = vault;
        }

        /// <summary>
        /// Create a new VehicleRegistration string to be aliased within static vault
        /// </summary>
        /// <param name="vehicleregistration"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
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
        /// Retrieve the VehicleRegistration string alias from a static vault. Returns an array of matching values. Array will be sorted by date created.
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task<VehicleRegistrationResponse> Retrieve(string aliasId)
        {
            return await _vault.Client.Get<VehicleRegistrationResponse>($"/vault/static/{_vault.VaultId}/vehicleregistration/{aliasId}");
        }

        /// <summary>
        /// Delete the VehicleRegistration alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns></returns>
        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/vehicleregistration/{aliasId}");
        }
    }
}
