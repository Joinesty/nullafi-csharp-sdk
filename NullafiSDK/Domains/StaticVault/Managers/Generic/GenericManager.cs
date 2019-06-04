using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.Generic
{
  public class GenericManager
{
  StaticVault vault;

  public GenericManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<GenericModel> Create(string data, List<string> tags)
  {
    var result = this.vault.Encrypt(data);
    var payload = new GenericModel
    {
        Data = result.EncryptedData,
        DataHash = this.vault.Hash(data),
        Iv = result.Iv,
        AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<GenericModel, GenericModel>($"/vault/static/${this.vault.VaultId}/generic", payload);
    response.Data = this.vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async Task<GenericModel> Retrieve(string tokenId)
  {
    var response = await this.vault.client.Get<GenericModel>($"/vault/static/{this.vault.VaultId}/generic/{tokenId}");
    response.Data = this.vault.Decrypt(response.Iv, response.AuthTag, response.Data);
    return response;
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/gender/{tokenId}");
  }
}
}
