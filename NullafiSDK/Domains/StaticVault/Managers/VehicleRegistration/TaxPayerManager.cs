using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.VehicleRegistration
{
  public class VehicleRegistrationManager
{
  StaticVault vault;

  public VehicleRegistrationManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<VehicleRegistrationModel> Create(string vehicleregistration, List<string> tags)
  {
    var result = this.vault.Encrypt(vehicleregistration);
    var payload = new VehicleRegistrationModel
    {
      VehicleRegistration = result.EncryptedData,
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<VehicleRegistrationModel, VehicleRegistrationModel>($"/vault/static/${this.vault.VaultId}/vehicleregistration", payload);
    return response;
  }

  public async Task<VehicleRegistrationModel> Retrieve(string tokenId)
  {
    return await this.vault.client.Get<VehicleRegistrationModel>($"/vault/static/{this.vault.VaultId}/vehicleregistration/{tokenId}");
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/vehicleregistration/{tokenId}");
  }
}
}
