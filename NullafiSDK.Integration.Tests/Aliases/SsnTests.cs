using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.Ssn;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class SsnTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("SSN Vault Example", null);

            SsnResponse created = await Create(staticVault);
            SsnResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.Ssn);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.Ssn, retrieved.Ssn);
            Assert.AreEqual(created.SsnAlias, retrieved.SsnAlias);

            SsnResponse createdWithState = await CreateWithState(staticVault);
            SsnResponse retrievedWithState = await Retrieve(staticVault, createdWithState.Id);

            await RetrieveFromRealData(staticVault, createdWithState.Ssn);
            await Delete(staticVault, retrievedWithState.Id);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<SsnResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.Ssn.Create(name);
        }

        private async Task<SsnResponse> CreateWithState(StaticVault vault)
        {
            var name = "Ssn With State Example";
            var state = "IL";

            return await vault.Ssn.Create(name, state, null);
        }

        private async Task<SsnResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.Ssn.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String ssn)
        {
            await vault.Ssn.RetrieveFromRealData(ssn);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.Ssn.Delete(id);
        }
    }
}
