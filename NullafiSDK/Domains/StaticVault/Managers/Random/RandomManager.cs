using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.Random
{
  public class RandomManager
{
  StaticVault vault;

  public RandomManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<RandomResponse> Create(string data, List<string> tags)
  {
    var result = this.vault.Encrypt(data);
    var payload = new RandomRequest
    {
      Data = result.EncryptedData,
      DataHash = this.vault.Hash(data),
      Tags = tags,
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<RandomRequest, RandomResponse>($"/vault/static/{this.vault.VaultId}/random", payload);
    response.Data = this.vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task<RandomResponse> Retrieve(string aliasId)
  {
    var response = await this.vault.client.Get<RandomResponse>($"/vault/static/{this.vault.VaultId}/random/{aliasId}");
    response.Data = this.vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task Delete(string aliasId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/random/{aliasId}");
  }
}
}
