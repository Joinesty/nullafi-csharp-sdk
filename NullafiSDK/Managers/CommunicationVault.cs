using NullafiSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NullafiSDK.Managers
{
    public class CommunicationVault
    {
        readonly Client client;
        readonly Security security;

        public string VaultId { get; set; }
        public string VaultName { get; set; }
        public string MasterKey { get; set; }

        private CommunicationVault(Client client, string vaultId, string vaultName, string masterKey)
        {
            this.client = client;
            this.VaultId = vaultId;
            this.VaultName = vaultName;
            this.MasterKey = masterKey;
            this.security = new Security();
        }

        public string Encrypt(string value)
        {
            var iv = this.security.AesGenerateInitializationVector();
            return this.security.AesEncrypt(this.MasterKey, iv, value);
        }

        public string Decrypt(string iv, string authTag, string value)
        {
            return this.security.AesDecrypt(this.MasterKey, iv, authTag, value);
        }

        public async static Task<CommunicationVault> PostCommunicationVault(Client client, string name, List<string> tags)
        {
            var security = new Security();
            var rsaEphemeral = security.RSAGenerateEphemeral("test");

            var payload = new CommunicationVaultPayload()
            {
                Name = name,
                PublicKey = rsaEphemeral.PublicKey,
                Tags = tags
            };

            var response = await client.Post<CommunicationVaultPayload, CommunicationVaultResponse>("/vault/communication", payload);

            var aesEncryptedMasterKey = rsaEphemeral.Decrypt(response.SessionKey);
            var masterKey = security.AesDecrypt(aesEncryptedMasterKey, response.Iv, response.AuthTag, response.VaultMasterKey);

            return new CommunicationVault(client, response.Id, response.Name, masterKey);
        }

        public async static Task<CommunicationVault> GetCommunicationVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<CommunicationVaultResponse>($"/vault/communication/${vaultId}");
            return new CommunicationVault(client, vaultId, response.Name, masterKey);
        }
    }
}
