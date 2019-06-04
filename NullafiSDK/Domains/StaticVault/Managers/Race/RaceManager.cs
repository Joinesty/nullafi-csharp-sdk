using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.Race
{
  public class RaceManager
{
  StaticVault vault;

  public RaceManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<RaceModel> Create(string race, List<string> tags)
  {
    var result = this.vault.Encrypt(race);
    var payload = new RaceModel
    {
      Race = result.EncryptedData,
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<RaceModel, RaceModel>($"/vault/static/${this.vault.VaultId}/race", payload);
    return response;
  }

  public async Task<RaceModel> Retrieve(string tokenId)
  {
    return await this.vault.client.Get<RaceModel>($"/vault/static/{this.vault.VaultId}/race/{tokenId}");
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/race/{tokenId}");
  }
}
}
