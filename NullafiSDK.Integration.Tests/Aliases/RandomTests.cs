using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.Random;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class RandomTests
    {

        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Random Vault Example", null);

            RandomResponse created = await Create(staticVault);
            RandomResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.Data);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.Data, retrieved.Data);
            Assert.AreEqual(created.Alias, retrieved.Alias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<RandomResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.Random.Create(name);
        }

        private async Task<RandomResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.Random.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String data)
        {
            await vault.Random.RetrieveFromRealData(data);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.Random.Delete(id);
        }
    }
}
