using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.Gender
{
  public class GenderManager
{
  StaticVault vault;

  public GenderManager(StaticVault vault)
{
  this.vault = vault;
}

  public async Task<GenderResponse> Create(string gender, List<string> tags, string gender)
  {
    var result = this.vault.Encrypt(gender);
    var payload = new GenderRequest
    {
        Gender = result.EncryptedData,
        GenderHash = this.vault.Hash(gender),
        Tags = tags,
        Iv = result.Iv,
        AuthTag = result.AuthTag
    };

    String url = $"/vault/static/{this.vault.VaultId}/gender";
    if (gender != null) url += $"/{gender}";

    var response = await this.vault.client.Post<GenderRequest, GenderResponse>(url, payload);
    response.Gender = this.vault.Decrypt(response.Iv, response.AuthTag, response.Gender);
    return response;
  }

  public async Task<GenderResponse> Retrieve(string aliasId)
  {
    var response = await this.vault.client.Get<GenderResponse>($"/vault/static/{this.vault.VaultId}/gender/{aliasId}");
    response.Gender = this.vault.Decrypt(response.Iv, response.AuthTag, response.Gender);
    return response;
  }

  public async Task Delete(string aliasId)
  {
    await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/gender/{aliasId}");
  }
}
}
