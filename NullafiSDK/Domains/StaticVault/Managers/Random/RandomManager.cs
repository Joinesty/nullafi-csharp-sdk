using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.Random
{
  public class RandomManager
{
  StaticVault vault;

  public RandomManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<RandomModel> Create(string data, List<string> tags)
  {
    var result = this.vault.Encrypt(data);
    var payload = new RandomModel
    {
      Data = result.EncryptedData,
      DataHash = this.vault.Hash(data),
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<RandomModel, RandomModel>($"/vault/static/${this.vault.VaultId}/random", payload);
    response.Data = this.vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task<RandomModel> Retrieve(string tokenId)
  {
    var response = await this.vault.client.Get<RandomModel>($"/vault/static/{this.vault.VaultId}/random/{tokenId}");
    response.Data = this.vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/random/{tokenId}");
  }
}
}
