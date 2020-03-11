using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.Gender;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class GenderTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Address Vault Example", null);

            GenderResponse created = await Create(staticVault);
            GenderResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.Gender);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.Gender, retrieved.Gender);
            Assert.AreEqual(created.GenderAlias, retrieved.GenderAlias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<GenderResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.Gender.Create(name);
        }

        private async Task<GenderResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.Gender.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String gender)
        {
            await vault.Gender.RetrieveFromRealData(gender);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.Gender.Delete(id);
        }
    }
}
