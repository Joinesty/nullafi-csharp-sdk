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
        internal readonly Client Client;
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

        /// <summary>
        /// Generate a hash for the real data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Hash(string value)
        {
            return _security.Hmac.Hash(value, Client.HashKey);
        }

        /// <summary>
        /// Encrypt static aliases (before sending info to the API)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public AesEncryptedData Encrypt(string value)
        {
            var iv = _security.Aes.GenerateStringIv();
            return _security.Aes.Encrypt(MasterKey, iv, value);
        }

        /// <summary>
        /// Decrypt static aliases
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Decrypt(string iv, string authTag, string value)
        {
            return _security.Aes.Decrypt(MasterKey, iv, authTag, value);
        }

        /// <summary>
        /// Create the API to create a new communication vault
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static async Task<CommunicationVault> CreateCommunicationVault(Client client, string name, List<string> tags = null)
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
            
            var masterKey = security.Aes.Decrypt(aesEncryptedMasterKey.Replace("\"", ""), response.Iv, response.AuthTag, response.MasterKey, true);

            return new CommunicationVault(client, response.Id, response.Name, masterKey);
        }

        /// <summary>
        /// Retrieve the communication vault from id
        /// </summary>
        /// <param name="client"></param>
        /// <param name="vaultId"></param>
        /// <param name="masterKey"></param>
        /// <returns></returns>
        public static async Task<CommunicationVault> RetrieveCommunicationVault(Client client, string vaultId, string masterKey)
        {
            var response = await client.Get<CommunicationVaultResponse>($"/vault/communication/{vaultId}");
            return new CommunicationVault(client, vaultId, response.Name, masterKey);
        }
    }
}
