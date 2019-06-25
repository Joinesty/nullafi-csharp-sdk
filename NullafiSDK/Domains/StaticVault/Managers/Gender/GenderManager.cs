using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Gender
{
    /// <summary>
    /// FirstNameManager
    /// </summary>
    public class GenderManager
{
    private readonly StaticVault _vault;

        /// <summary>
        /// Create an instance of GenderManager
        /// </summary>
        /// <param name="vault"></param>
        public GenderManager(StaticVault vault)
        {
          _vault = vault;
        }

        /// <summary>
        /// Create a new Gender string to be aliased within static vault
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="tags"></param>
        /// <returns>id, gender, genderAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<GenderResponse> Create(string gender, List<string> tags = null)
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

        /// <summary>
        /// Retrieve the Gender string alias from a static vault.
        /// </summary>
        /// <remarks>
        /// <para>Returns an array of matching values.</para>
        /// <para>Array will be sorted by date created.</para>
        /// </remarks>
        /// <param name="aliasId"></param>
        /// <returns>id, gender, genderAlias, tags, iv, authTag, tags, createdAt</returns>
        public async Task<GenderResponse> Retrieve(string aliasId)
      {
        var response = await _vault.Client.Get<GenderResponse>($"/vault/static/{_vault.VaultId}/gender/{aliasId}");
        response.Gender = _vault.Decrypt(response.Iv, response.AuthTag, response.Gender);
        return response;
      }

        /// <summary>
        /// Delete the Gender alias from static vault
        /// </summary>
        /// <param name="aliasId"></param>
        /// <returns>ok</returns>
        public async Task Delete(string aliasId)
      {
        await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/gender/{aliasId}");
      }
    }
}
