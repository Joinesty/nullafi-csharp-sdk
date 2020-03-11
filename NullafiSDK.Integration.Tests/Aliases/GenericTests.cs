using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.Generic;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class GenericTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Address Vault Example", null);

            GenericResponse created = await Create(staticVault);
            GenericResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.Data);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.Data, retrieved.Data);
            Assert.AreEqual(created.Alias, retrieved.Alias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<GenericResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.Generic.Create(name, @"\d{4}");
        }

        private async Task<GenericResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.Generic.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String data)
        {
            await vault.Generic.RetrieveFromRealData(data);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.Generic.Delete(id);
        }
    }
}
