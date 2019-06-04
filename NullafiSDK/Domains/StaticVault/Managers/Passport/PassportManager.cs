using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.Passport
{
  public class PassportManager
{
  StaticVault vault;

  public PassportManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<PassportModel> Create(string passport, List<string> tags)
  {
    var result = this.vault.Encrypt(passport);
    var payload = new PassportModel
    {
      Passport = result.EncryptedData,
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<PassportModel, PassportModel>($"/vault/static/${this.vault.VaultId}/passport", payload);
    return response;
  }

  public async Task<PassportModel> Retrieve(string tokenId)
  {
    return await this.vault.client.Get<PassportModel>($"/vault/static/{this.vault.VaultId}/passport/{tokenId}");
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/passport/{tokenId}");
  }
}
}
