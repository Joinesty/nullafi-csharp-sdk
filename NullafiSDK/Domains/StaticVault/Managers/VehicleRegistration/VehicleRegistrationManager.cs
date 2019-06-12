using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.VehicleRegistration
{
  public class VehicleRegistrationManager
{
    private readonly StaticVault _vault;

  public VehicleRegistrationManager(StaticVault vault)
{
  _vault = vault;
}

  public async Task<VehicleRegistrationResponse> Create(string vehicleregistration, List<string> tags)
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

  public async Task<VehicleRegistrationResponse> Retrieve(string aliasId)
  {
    return await _vault.Client.Get<VehicleRegistrationResponse>($"/vault/static/{_vault.VaultId}/vehicleregistration/{aliasId}");
  }

  public async Task Delete(string aliasId)
  {
    await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/vehicleregistration/{aliasId}");
  }
}
}
