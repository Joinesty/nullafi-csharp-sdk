using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.Gender
{
  public class GenderManager
{
  StaticVault vault;

  public GenderManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<GenderModel> Create(string gender, List<string> tags)
  {
    var result = this.vault.Encrypt(gender);
    var payload = new GenderModel
    {
      Gender = result.EncryptedData,
        Iv = result.Iv,
        AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<GenderModel, GenderModel>($"/vault/static/${this.vault.VaultId}/gender", payload);
    return response;
  }

  public async Task<GenderModel> Retrieve(string tokenId)
  {
    return await this.vault.client.Get<GenderModel>($"/vault/static/{this.vault.VaultId}/gender/{tokenId}");
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/gender/{tokenId}");
  }
}
}
