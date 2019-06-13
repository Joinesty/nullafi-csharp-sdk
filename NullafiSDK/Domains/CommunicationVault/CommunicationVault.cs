using Nullafi.Domains.CommunicationVault.Managers.Email;
using Nullafi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.CommunicationVault
{
    /// <summary>
    /// Communication Vault
    /// </summary>
    public class CommunicationVault
    {
        public readonly Client Client;
        private readonly Security _security;

        public string VaultId { get; set; }
        public string VaultName { get; set; }
        public string MasterKey { get; set; }
        public EmailManager Email { get; private set; }

        /// <summary>
        /// Create an instance of CommunicationVault
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vaultId"></param>
        /// <param name="vaultName"></param>
        /// <param name="masterKey"></param>
        /// <returns></returns>
        private CommunicationVault(Client client, string vaultId, string vaultName, string masterKey)
        {
            Client = client;
            VaultId = vaultId;
            VaultName = vaultName;
            MasterKey = masterKey;
            _security = new Security();

            Email = new EmailManager(this);
        }

        public string Hash(string value)
        {
            return _security.Hmac.Hash(value, Client.HashKey);
        }

        public AesEncryptedData Encrypt(string value)
        {
            var iv = _security.Aes.GenerateIv();
            var byteMasterKey = Convert.FromBase64String(MasterKey);
            return _security.Aes.Encrypt(byteMasterKey, iv, value);
        }

        public string Decrypt(string iv, string authTag, string value)
        {
            var byteIv = Convert.FromBase64String(iv);
            var byteAuthTag = Convert.FromBase64String(authTag);
            var byteMasterKey = Convert.FromBase64String(MasterKey);

            return _security.Aes.Decrypt(byteMasterKey, byteIv, byteAuthTag, value);
        }

        public static async Task<CommunicationVault> CreateCommunicationVault(Client client, string name, List<string> tags)
        {
            var security = new Security();
            var rsaEphemeral = security.RSA.RSAGenerateManager();

            var payload = new CommunicationVaultPayload()
            {
                Name = name,
                PublicKey = rsaEphemeral.PublicKey,
                Tags = tags
            };

            var response = await client.Post<CommunicationVaultPayload, CommunicationVaultResponse>("/vault/communication", payload);

            var aesEncryptedMasterKey = rsaEphemeral.Decrypt(response.SessionKey);
            var byteAesEncryptedMasterKey = Convert.FromBase64String(aesEncryptedMasterKey.Replace("\"", ""));
            var byteIv = Convert.FromBase64String(response.Iv);
            var byteAuthTag = Convert.FromBase64String(response.AuthTag);
            
            var masterKey = Convert.ToBase64String(security.Aes.Decrypt(byteAesEncryptedMasterKey, byteIv, byteAuthTag, Convert.FromBase64String(response.MasterKey)));

            return new CommunicationVault(client, response.Id, response.Name, masterKey);
        }

        public static async Task<CommunicationVault> RetrieveCommunicationVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<CommunicationVaultResponse>($"/vault/communication/{vaultId}");
            return new CommunicationVault(client, vaultId, response.Name, masterKey);
        }
    }
}
