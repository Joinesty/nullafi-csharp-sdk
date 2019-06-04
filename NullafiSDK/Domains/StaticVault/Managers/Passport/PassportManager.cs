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
      PassportHash = this.vault.Hash(passport),
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<PassportModel, PassportModel>($"/vault/static/${this.vault.VaultId}/passport", payload);
    response.Passport = this.vault.Decrypt(response.Iv, response.AuthTag, response.Passport);
    return response;
  }

  public async Task<PassportModel> Retrieve(string tokenId)
  {
    var response = await this.vault.client.Get<PassportModel>($"/vault/static/{this.vault.VaultId}/passport/{tokenId}");
    response.Passport = this.vault.Decrypt(response.Iv, response.AuthTag, response.Passport);
    return response;
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/passport/{tokenId}");
  }
}
}
