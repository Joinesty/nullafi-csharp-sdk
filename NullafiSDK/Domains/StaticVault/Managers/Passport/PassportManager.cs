using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Passport
{
    public class PassportManager
    {
        private readonly StaticVault _vault;

        public PassportManager(StaticVault vault)
        {
            _vault = vault;
        }

        public async Task<PassportResponse> Create(string passport, List<string> tags)
        {
            var result = _vault.Encrypt(passport);
            var payload = new PassportRequest
            {
                Passport = result.EncryptedData,
                PassportHash = _vault.Hash(passport),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await _vault.Client.Post<PassportRequest, PassportResponse>($"/vault/static/{_vault.VaultId}/passport", payload);
            response.Passport = _vault.Decrypt(response.Iv, response.AuthTag, response.Passport);
            return response;
        }

        public async Task<PassportResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<PassportResponse>($"/vault/static/{_vault.VaultId}/passport/{aliasId}");
            response.Passport = _vault.Decrypt(response.Iv, response.AuthTag, response.Passport);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/passport/{aliasId}");
        }
    }
}
