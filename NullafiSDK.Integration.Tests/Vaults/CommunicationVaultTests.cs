using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Nullafi;
using Nullafi.Domains.CommunicationVault;

namespace NullafiSDK.Integration.Tests.Vaults
{
    [TestClass]
    [TestCategory("Integration")]
    public class CommunicationVaultTests
    {
        [TestMethod]
        public async Task GivenApiKey_WhenCreatingNewVault_ShouldReturnVaultEncryptionKey()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var vaultCreated = CreateCommunicationVault(client, "test vault").Result;

            var vaultReturned = RetrieveCommunicationVault(client, vaultCreated.VaultId, vaultCreated.MasterKey).Result;

            await DeleteCommunicationVault(client, vaultCreated.VaultId);

            Assert.AreEqual(vaultCreated.VaultId, vaultReturned.VaultId);
            Assert.AreEqual(vaultCreated.VaultName, vaultReturned.VaultName);
            Assert.IsNotNull(vaultCreated.MasterKey);
        }

        private async Task<CommunicationVault> CreateCommunicationVault(Client client, string name)
        {
            var staticVault = await client.CreateCommunicationVault(name, null);
            return staticVault;
        }

        private async Task<CommunicationVault> RetrieveCommunicationVault(Client client, string id, string masterKey)
        {
            var staticVault = await client.RetrieveCommunicationVault(id, masterKey);
            return staticVault;
        }

        private async Task DeleteCommunicationVault(Client client, string id)
        {
            await client.DeleteCommunicationVault(id);
        }
    }
}
