using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.TaxPayer;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class TaxPayerTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Address Vault Example", null);

            TaxPayerResponse created = await Create(staticVault);
            TaxPayerResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.TaxPayer);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.TaxPayer, retrieved.TaxPayer);
            Assert.AreEqual(created.TaxPayerAlias, retrieved.TaxPayerAlias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<TaxPayerResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.TaxPayer.Create(name);
        }

        private async Task<TaxPayerResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.TaxPayer.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String taxpayer)
        {
           await vault.TaxPayer.RetrieveFromRealData(taxpayer);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.TaxPayer.Delete(id);
        }
    }
}
