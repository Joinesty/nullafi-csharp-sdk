using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.VehicleRegistration;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class VehicleRegistrationTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Vehicle Registration Vault Example", null);

            VehicleRegistrationResponse created = await Create(staticVault);
            VehicleRegistrationResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.VehicleRegistration);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.VehicleRegistration, retrieved.VehicleRegistration);
            Assert.AreEqual(created.VehicleRegistrationAlias, retrieved.VehicleRegistrationAlias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<VehicleRegistrationResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.VehicleRegistration.Create(name);
        }

        private async Task<VehicleRegistrationResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.VehicleRegistration.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String vehicleRegistration)
        {
            await vault.VehicleRegistration.RetrieveFromRealData(vehicleRegistration);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.VehicleRegistration.Delete(id);
        }
    }
}
