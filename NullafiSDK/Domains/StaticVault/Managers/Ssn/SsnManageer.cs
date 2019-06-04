using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NullafiSDK.Domains.StaticVault;

namespace NullafiSDK.Domains.StaticVault.Managers.Ssn
{
  public class SsnManager
{
  StaticVault vault;

  public SsnManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<SsnModel> Create(string ssn, List<string> tags)
  {
    var result = this.vault.Encrypt(ssn);
    var payload = new SsnModel
    {
      Ssn = result.EncryptedData,
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await this.vault.client.Post<SsnModel, SsnModel>($"/vault/static/${this.vault.VaultId}/ssn", payload);
    return response;
  }

  public async Task<SsnModel> Retrieve(string tokenId)
  {
    return await this.vault.client.Get<SsnModel>($"/vault/static/{this.vault.VaultId}/ssn/{tokenId}");
  }

  public async void Delete(string tokenId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/ssn/{tokenId}");
  }
}
}
