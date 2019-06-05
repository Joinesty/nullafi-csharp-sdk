using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.Race
{
  public class RaceManager
{
  StaticVault vault;

  public RaceManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<RaceResponse> Create(string race, List<string> tags)
  {
    var result = this.vault.Encrypt(race);
    var payload = new RaceRequest
    {
      Race = result.EncryptedData,
      RaceHash = this.vault.Hash(race),
      Tags = tags,
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<RaceRequest, RaceResponse>($"/vault/static/${this.vault.VaultId}/race", payload);
    response.Race = this.vault.Decrypt(response.Iv, response.AuthTag, response.Race);
    return response;
  }

  public async Task<RaceResponse> Retrieve(string aliasId)
  {
    var response = await this.vault.client.Get<RaceResponse>($"/vault/static/{this.vault.VaultId}/race/{aliasId}");
    response.Race = this.vault.Decrypt(response.Iv, response.AuthTag, response.Race);
    return response;
  }

  public async Task Delete(string aliasId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/race/{aliasId}");
  }
}
}
