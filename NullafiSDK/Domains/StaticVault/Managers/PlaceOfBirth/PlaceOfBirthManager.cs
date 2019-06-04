using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.PlaceOfBirth
{
  public class PlaceOfBirthManager
{
  StaticVault vault;

  public PlaceOfBirthManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<PlaceOfBirthModel> Create(string placeofbirth, List<string> tags)
  {
    var result = this.vault.Encrypt(placeofbirth);
    var payload = new PlaceOfBirthModel
    {
      PlaceOfBirth = result.EncryptedData,
      PlaceOfBirthHash = this.vault.Hash(placeofbirth),
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<PlaceOfBirthModel, PlaceOfBirthModel>($"/vault/static/${this.vault.VaultId}/placeofbirth", payload);
    response.PlaceOfBirth = this.vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
    return response;
  }

  public async Task<PlaceOfBirthModel> Retrieve(string tokenId)
  {
    var response = await this.vault.client.Get<PlaceOfBirthModel>($"/vault/static/{this.vault.VaultId}/placeofbirth/{tokenId}");
    response.PlaceOfBirth = this.vault.Decrypt(response.Iv, response.AuthTag, response.PlaceOfBirth);
    return response;
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/placeofbirth/{tokenId}");
  }
}
}
