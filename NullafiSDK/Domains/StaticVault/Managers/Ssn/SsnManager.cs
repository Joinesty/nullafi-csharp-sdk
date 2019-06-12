using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.Ssn
{
    public class SsnManager
    {
        StaticVault vault;

        public SsnManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<SsnResponse> Create(string ssn, List<string> tags)
        {
            return await this.Create(ssn, null, tags);
        }

        public async Task<SsnResponse> Create(string ssn, string state = null, List<string> tags = null)
        {
            var result = this.vault.Encrypt(ssn);
            var payload = new SsnRequest
            {
                Ssn = result.EncryptedData,
                SsnHash = this.vault.Hash(ssn),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            String url = $"/vault/static/{this.vault.VaultId}/ssn";
            if (state != null) url += $"/{state}";

            var response = await this.vault.client.Post<SsnRequest, SsnResponse>(url, payload);
            response.Ssn = this.vault.Decrypt(response.Iv, response.AuthTag, response.Ssn);
            return response;
        }

        public async Task<SsnResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<SsnResponse>($"/vault/static/{this.vault.VaultId}/ssn/{aliasId}");
            response.Ssn = this.vault.Decrypt(response.Iv, response.AuthTag, response.Ssn);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/ssn/{aliasId}");
        }
    }
}
