using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.DriversLicense;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class DriversLicenseTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Drivers License Vault Example", null);

            DriversLicenseResponse created = await Create(staticVault);
            DriversLicenseResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.DriversLicense);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.DriversLicense, retrieved.DriversLicense);
            Assert.AreEqual(created.DriversLicenseAlias, retrieved.DriversLicenseAlias);

            DriversLicenseResponse createdWithState = await CreateWithState(staticVault);
            DriversLicenseResponse retrievedWithState = await Retrieve(staticVault, createdWithState.Id);

            Assert.AreEqual(createdWithState.Id, retrievedWithState.Id);
            Assert.AreEqual(createdWithState.DriversLicense, retrievedWithState.DriversLicense);
            Assert.AreEqual(createdWithState.DriversLicenseAlias, retrievedWithState.DriversLicenseAlias);

            await RetrieveFromRealData(staticVault, createdWithState.DriversLicense);
            await Delete(staticVault, retrievedWithState.Id);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<DriversLicenseResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.DriversLicense.Create(name);
        }

        private async Task<DriversLicenseResponse> CreateWithState(StaticVault vault)
        {
            var name = "DriversLicense With State Example";
            var state = "IL";
            
            return await vault.DriversLicense.Create(name, state);
        }

        private async Task<DriversLicenseResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.DriversLicense.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String driversLicense)
        {
            await vault.DriversLicense.RetrieveFromRealData(driversLicense);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.DriversLicense.Delete(id);
        }
    }
}
