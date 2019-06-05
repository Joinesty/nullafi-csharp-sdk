using Nullafi.Domains.CommunicationVault.Managers.Email;
using Nullafi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nullafi.Domains.CommunicationVault
{
    public class CommunicationVault
    {
        public readonly Client client;
        readonly Security security;

        public string VaultId { get; set; }
        public string VaultName { get; set; }
        public string MasterKey { get; set; }
        public EmailManager Email { get; private set; }

        private CommunicationVault(Client client, string vaultId, string vaultName, string masterKey)
        {
            this.client = client;
            this.VaultId = vaultId;
            this.VaultName = vaultName;
            this.MasterKey = masterKey;
            this.security = new Security();

            this.Email = new EmailManager(this);
        }

        public string Hash(string value)
        {
            return this.security.hmac.Hash(value, this.client.HashKey);
        }

        public AesEncryptedData Encrypt(string value)
        {
            var iv = this.security.aes.GenerateIv();
            byte[] byteMasterKey = Convert.FromBase64String(this.MasterKey);
            return this.security.aes.Encrypt(byteMasterKey, iv, value);
        }

        public string Decrypt(string iv, string authTag, string value)
        {
            byte[] byteIv = Convert.FromBase64String(iv);
            byte[] byteAuthTag = Convert.FromBase64String(authTag);
            byte[] byteMasterKey = Convert.FromBase64String(this.MasterKey);

            return this.security.aes.Decrypt(byteMasterKey, byteIv, byteAuthTag, value);
        }

        public async static Task<CommunicationVault> CreateCommunicationVault(Client client, string name, List<string> tags)
        {
            var security = new Security();
            var rsaEphemeral = security.rsa.RSAGenerateManager();

            var payload = new CommunicationVaultPayload()
            {
                Name = name,
                PublicKey = rsaEphemeral.PublicKey,
                Tags = tags
            };

            var response = await client.Post<CommunicationVaultPayload, CommunicationVaultResponse>("/vault/communication", payload);

            string aesEncryptedMasterKey = rsaEphemeral.Decrypt(response.SessionKey);
            byte[] byteAesEncryptedMasterKey = Convert.FromBase64String(aesEncryptedMasterKey.Replace("\"", ""));
            byte[] byteIv = Convert.FromBase64String(response.Iv);
            byte[] byteAuthTag = Convert.FromBase64String(response.AuthTag);
            
            var masterKey = Convert.ToBase64String(security.aes.Decrypt(byteAesEncryptedMasterKey, byteIv, byteAuthTag, Convert.FromBase64String(response.MasterKey)));

            return new CommunicationVault(client, response.Id, response.Name, masterKey);
        }

        public async static Task<CommunicationVault> RetrieveCommunicationVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<CommunicationVaultResponse>($"/vault/communication/{vaultId}");
            return new CommunicationVault(client, vaultId, response.Name, masterKey);
        }
    }
}
