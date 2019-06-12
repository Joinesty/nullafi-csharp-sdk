using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Race
{
  public class RaceManager
{
    private readonly StaticVault _vault;

  public RaceManager(StaticVault vault)
{
  _vault = vault;
}

  public async Task<RaceResponse> Create(string race, List<string> tags)
  {
    var result = _vault.Encrypt(race);
    var payload = new RaceRequest
    {
      Race = result.EncryptedData,
      RaceHash = _vault.Hash(race),
      Tags = tags,
      Iv = result.Iv,
      AuthTag = result.AuthTag
    };

    var response = await _vault.Client.Post<RaceRequest, RaceResponse>($"/vault/static/{_vault.VaultId}/race", payload);
    response.Race = _vault.Decrypt(response.Iv, response.AuthTag, response.Race);
    return response;
  }

  public async Task<RaceResponse> Retrieve(string aliasId)
  {
    var response = await _vault.Client.Get<RaceResponse>($"/vault/static/{_vault.VaultId}/race/{aliasId}");
    response.Race = _vault.Decrypt(response.Iv, response.AuthTag, response.Race);
    return response;
  }

  public async Task Delete(string aliasId)
  {
    await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/race/{aliasId}");
  }
}
}
