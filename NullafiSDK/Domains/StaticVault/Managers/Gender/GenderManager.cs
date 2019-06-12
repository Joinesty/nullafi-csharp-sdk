using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Gender
{
  public class GenderManager
{
    private readonly StaticVault _vault;

  public GenderManager(StaticVault vault)
{
  _vault = vault;
}

  public async Task<GenderResponse> Create(string gender, List<string> tags)
  {
    var result = _vault.Encrypt(gender);
    var payload = new GenderRequest
    {
        Gender = result.EncryptedData,
        GenderHash = _vault.Hash(gender),
        Tags = tags,
        Iv = result.Iv,
        AuthTag = result.AuthTag
    };

    var response = await _vault.Client.Post<GenderRequest, GenderResponse>($"/vault/static/{_vault.VaultId}/gender", payload);
    response.Gender = _vault.Decrypt(response.Iv, response.AuthTag, response.Gender);
    return response;
  }

  public async Task<GenderResponse> Retrieve(string aliasId)
  {
    var response = await _vault.Client.Get<GenderResponse>($"/vault/static/{_vault.VaultId}/gender/{aliasId}");
    response.Gender = _vault.Decrypt(response.Iv, response.AuthTag, response.Gender);
    return response;
  }

  public async Task Delete(string aliasId)
  {
    await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/gender/{aliasId}");
  }
}
}
