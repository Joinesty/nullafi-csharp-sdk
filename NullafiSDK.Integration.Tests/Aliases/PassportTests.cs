using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.Passport;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class PassportTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Passport Vault Example", null);

            PassportResponse created = await Create(staticVault);
            PassportResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.Passport);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.Passport, retrieved.Passport);
            Assert.AreEqual(created.PassportAlias, retrieved.PassportAlias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<PassportResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.Passport.Create(name);
        }

        private async Task<PassportResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.Passport.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String passport)
        {
           await vault.Passport.RetrieveFromRealData(passport);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.Passport.Delete(id);
        }
    }
}
