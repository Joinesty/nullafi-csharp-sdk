using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nullafi.Domains.StaticVault;

namespace Nullafi.Domains.StaticVault.Managers.Passport
{
    public class PassportManager
    {
        StaticVault vault;

        public PassportManager(StaticVault vault)
        {
            this.vault = vault;
        }

        public async Task<PassportResponse> Create(string passport, List<string> tags)
        {
            var result = this.vault.Encrypt(passport);
            var payload = new PassportRequest
            {
                Passport = result.EncryptedData,
                PassportHash = this.vault.Hash(passport),
                Tags = tags,
                Iv = result.Iv,
                AuthTag = result.AuthTag
            };

            var response = await this.vault.client.Post<PassportRequest, PassportResponse>($"/vault/static/${this.vault.VaultId}/passport", payload);
            response.Passport = this.vault.Decrypt(response.Iv, response.AuthTag, response.Passport);
            return response;
        }

        public async Task<PassportResponse> Retrieve(string aliasId)
        {
            var response = await this.vault.client.Get<PassportResponse>($"/vault/static/{this.vault.VaultId}/passport/{aliasId}");
            response.Passport = this.vault.Decrypt(response.Iv, response.AuthTag, response.Passport);
            return response;
        }

        public async Task Delete(string aliasId)
        {
            await this.vault.client.Delete($"/vault/static/{this.vault.VaultId}/passport/{aliasId}");
        }
    }
}
