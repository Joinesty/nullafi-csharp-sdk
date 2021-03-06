﻿using System;
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
            Assert.IsNotNull(vaultCreated.MasterKey);
        }

        private async Task<StaticVault> CreateStaticVault(Client client, string name)
        {
            var staticVault = await client.CreateStaticVault(name, null);
            return staticVault;
        }

        private async Task<StaticVault> RetrieveStaticVault(Client client, string id, string masterKey)
        {
            var staticVault = await client.RetrieveStaticVault(id, masterKey);
            return staticVault;
        }

        private async Task DeleteStaticVault(Client client, string id)
        {
            await client.DeleteStaticVault(id);
        }
    }
}
