using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Nullafi;
using Nullafi.Domains.StaticVault;

namespace NullafiSDK.Integration.Tests.Vaults
{
    [TestClass]
    [TestCategory("Integration")]
    public class StaticVaultTests
    {
        [ClassInitialize]
        public static void ApiAuthentication(TestContext context)
        {
            Environment.SetEnvironmentVariable("NULLAFI_API_URL", "**NUllAFI_API**");
            Environment.SetEnvironmentVariable("API_KEY", "**YOUR_API_KEY**");
        }

        [TestMethod]
        public async Task GivenApiKey_WhenCreatingNewVault_ShouldReturnVaultEncryptionKey()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var vaultCreated = CreateStaticVault(client, "test vault").Result;

            var vaultReturned = RetrieveStaticVault(client, vaultCreated.VaultId, vaultCreated.MasterKey).Result;

            await DeleteStaticVault(client, vaultCreated.VaultId);

            Assert.AreEqual(vaultCreated.VaultId, vaultReturned.VaultId);
            Assert.AreEqual(vaultCreated.VaultName, vaultReturned.VaultName);
        }

        private async Task<StaticVault> CreateStaticVault(Client client, string name)
        {
            StaticVault staticVault = await client.CreateStaticVault(name, null);
            Console.WriteLine("**** StaticVaultExample.createStaticVault:");
            Console.WriteLine("-> Id: " + staticVault.VaultId);
            Console.WriteLine("-> Name: " + staticVault.VaultName);
            Console.WriteLine("-> MasterKey: " + staticVault.MasterKey);
            Console.WriteLine("\n");

            return staticVault;
        }

        private async Task<StaticVault> RetrieveStaticVault(Client client, string id, string masterKey)
        {
            StaticVault staticVault = await client.RetrieveStaticVault(id, masterKey);
            Console.WriteLine("**** StaticVaultExample.retrieveStaticVault:");
            Console.WriteLine("-> Id: " + staticVault.VaultId);
            Console.WriteLine("-> Name: " + staticVault.VaultName);
            Console.WriteLine("-> MasterKey: " + staticVault.MasterKey);
            Console.WriteLine("\n");

            return staticVault;
        }

        private async Task DeleteStaticVault(Client client, string id)
        {
            await client.DeleteStaticVault(id);
            Console.WriteLine("**** StaticVaultExample.deleteStaticVault:");
            Console.WriteLine("-> Id: " + id);
            Console.WriteLine("-> ok: true");
            Console.WriteLine("\n");
        }
    }
}
