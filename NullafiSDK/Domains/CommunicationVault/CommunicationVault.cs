using NullafiSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NullafiSDK.Domains.CommunicationVault
{
    public class CommunicationVault
    {
        public readonly Client client;
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

        public string Hash(string value)
        {
            return this.security.hmac.Hash(value, this.client.HashKey);
        }

        public AesEncryptedData Encrypt(string value)
        {
            var iv = this.security.aes.GenerateIv();
            byte[] byteMasterKey = Encoding.UTF8.GetBytes(this.MasterKey);
            return this.security.aes.Encrypt(byteMasterKey, iv, value);
        }

        public string Decrypt(string iv, string authTag, string value)
        {
            byte[] byteIv = Encoding.UTF8.GetBytes(iv);
            byte[] byteAuthTag = Encoding.UTF8.GetBytes(authTag);
            byte[] byteMasterKey = Encoding.UTF8.GetBytes(this.MasterKey);

            return this.security.aes.Decrypt(byteMasterKey, byteIv, byteAuthTag, value);
        }

        public async static Task<CommunicationVault> PostCommunicationVault(Client client, string name, List<string> tags)
        {
            var security = new Security();
            var rsaEphemeral = security.rsa.RSAGenerateManager(security.aes.GenerateStringMasterKey());

            var payload = new CommunicationVaultPayload()
            {
                Name = name,
                PublicKey = rsaEphemeral.PublicKey,
                Tags = tags
            };

            var response = await client.Post<CommunicationVaultPayload, CommunicationVaultResponse>("/vault/communication", payload);

            string aesEncryptedMasterKey = rsaEphemeral.Decrypt(response.SessionKey);
            byte[] byteAesEncryptedMasterKey = Encoding.UTF8.GetBytes(aesEncryptedMasterKey);
            byte[] byteIv = Encoding.UTF8.GetBytes(response.Iv);
            byte[] byteAuthTag = Encoding.UTF8.GetBytes(response.AuthTag);
            
            var masterKey = security.aes.Decrypt(byteAesEncryptedMasterKey, byteIv, byteAuthTag, response.VaultMasterKey);

            return new CommunicationVault(client, response.Id, response.Name, masterKey);
        }

        public async static Task<CommunicationVault> GetCommunicationVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<CommunicationVaultResponse>($"/vault/communication/${vaultId}");
            return new CommunicationVault(client, vaultId, response.Name, masterKey);
        }
    }
}
