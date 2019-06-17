using Nullafi.Domains.CommunicationVault.Managers.Email;
using Nullafi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nullafi.Domains.CommunicationVault
{
    /// <summary>
    /// 
    /// </summary>
    public class CommunicationVault
    {
        internal readonly Client Client;
        private readonly Security _security;

        /// <summary>
        /// 
        /// </summary>
        public string VaultId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VaultName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MasterKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EmailManager Email { get; private set; }

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

        internal AesEncryptedData Encrypt(string value)
        {
            var iv = _security.Aes.GenerateStringIv();
            return _security.Aes.Encrypt(MasterKey, iv, value);
        }

        internal string Decrypt(string iv, string authTag, string value)
        {
            return _security.Aes.Decrypt(MasterKey, iv, authTag, value);
        }

        /// <summary>
        /// 
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
        /// 
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
