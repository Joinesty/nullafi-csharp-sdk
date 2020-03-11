using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.Address;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    [TestCategory("Integration")]
    public class AddressExample
    {

        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Address Vault Example", null);
            
            AddressResponse created = await Create(staticVault);
            AddressResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.Address);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.Address, retrieved.Address);
            Assert.AreEqual(created.AddressAlias, retrieved.AddressAlias);

            AddressResponse createdWithState = await CreateWithState(staticVault);
            AddressResponse retrievedWithState = await Retrieve(staticVault, createdWithState.Id);

            await RetrieveFromRealData(staticVault, createdWithState.Address);
            await Delete(staticVault, retrievedWithState.Id);

            Assert.AreEqual(createdWithState.Id, retrievedWithState.Id);
            Assert.AreEqual(createdWithState.Address, retrievedWithState.Address);
            Assert.AreEqual(createdWithState.AddressAlias, retrievedWithState.AddressAlias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<AddressResponse> Create(StaticVault vault)
        {
            var name = "Address Example";
            return await vault.Address.Create(name, null);
        }

        private async Task<AddressResponse> CreateWithState(StaticVault vault)
        {
            var name = "Address With State Example";
            var state = "IL";
            return await vault.Address.Create(name, state);
        }

        private async Task<AddressResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.Address.Retrieve(id);
        }

        private async Task<List<AddressResponse>> RetrieveFromRealData(StaticVault vault, String address)
        {
           return await vault.Address.RetrieveFromRealData(address);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.Address.Delete(id);
        }
    }
}
