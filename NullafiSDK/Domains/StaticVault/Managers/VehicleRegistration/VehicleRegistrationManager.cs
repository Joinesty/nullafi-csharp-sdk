using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.VehicleRegistration
{
  public class VehicleRegistrationManager
{
  StaticVault vault;

  public VehicleRegistrationManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<VehicleRegistrationResponse> Create(string vehicleregistration, List<string> tags)
  {
    var result = this.vault.Encrypt(vehicleregistration);
    var payload = new VehicleRegistrationRequest
    {
      VehicleRegistration = result.EncryptedData,
      VehicleRegistrationHash = this.vault.Hash(vehicleregistration),
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<VehicleRegistrationRequest, VehicleRegistrationResponse>($"/vault/static/{this.vault.VaultId}/vehicleregistration", payload);
    response.VehicleRegistration = this.vault.Decrypt(response.Iv, response.AuthTag, response.VehicleRegistration);
    return response;
  }

  public async Task<VehicleRegistrationResponse> Retrieve(string aliasId)
  {
    return await this.vault.client.Get<VehicleRegistrationResponse>($"/vault/static/{this.vault.VaultId}/vehicleregistration/{aliasId}");
  }

  public async Task Delete(string aliasId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/vehicleregistration/{aliasId}");
  }
}
}
