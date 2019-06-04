using NullafiSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NullafiSDK.Domains.StaticVault
{
    public class StaticVault
    {
        public readonly Client client;
        readonly Security security;

        public string VaultId { get; set; }
        public string VaultName { get; set; }
        public string MasterKey { get; set; }

        private StaticVault(Client client, string vaultId, string vaultName, string masterKey)
        {
            this.client = client;
            this.VaultId = vaultId;
            this.VaultName = vaultName;
            this.MasterKey = masterKey;
            this.security = new Security();
        }

        public AesEncryptedData Encrypt(string value)
        {
            var iv = this.security.AesGenerateInitializationVector();
            return this.security.AesEncrypt(this.MasterKey, iv, value);
        }

        public string Decrypt(string iv, string authTag, string value)
        {
            return this.security.AesDecrypt(this.MasterKey, iv, authTag, value);
        }

        public async static Task<StaticVault> CreateStaticVault(Client client, string name, List<string> tags)
        {
            var security = new Security();

            var payload = new StaticVaultPayload()
            {
                Name = name,
                Tags = tags
            };

            var response = await client.Post<StaticVaultPayload, StaticVaultResponse>("/vault/static", payload);

            return new StaticVault(client, response.Id, response.Name, security.AesGenerateMasterKey());
        }

        public async static Task<StaticVault> RetrieveStaticVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<StaticVaultResponse>($"/vault/static/${vaultId}");
            return new StaticVault(client, vaultId, response.Name, masterKey);
        }
    }
}
