using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.StaticVault.Managers.Ssn
{
    public class SsnManager
    {
        private readonly StaticVault _vault;

        public SsnManager(StaticVault vault)
        {
            _vault = vault;
        }

        public async Task<SsnResponse> Create(string ssn, List<string> tags, string state)
        {
            var result = _vault.Encrypt(ssn);
            var payload = new SsnRequest
            {
                Ssn = result.EncryptedData,
                SsnHash = _vault.Hash(ssn),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var url = $"/vault/static/{_vault.VaultId}/ssn";
            if (state != null) url += $"/{state}";

            var response = await _vault.Client.Post<SsnRequest, SsnResponse>(url, payload);
            response.Ssn = _vault.Decrypt(response.Iv, response.AuthTag, response.Ssn);
            return response;
        }

        public async Task<SsnResponse> Retrieve(string aliasId)
        {
            var response = await _vault.Client.Get<SsnResponse>($"/vault/static/{_vault.VaultId}/ssn/{aliasId}");
            response.Ssn = _vault.Decrypt(response.Iv, response.AuthTag, response.Ssn);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await _vault.Client.Delete($"/vault/static/{_vault.VaultId}/ssn/{aliasId}");
        }
    }
}
