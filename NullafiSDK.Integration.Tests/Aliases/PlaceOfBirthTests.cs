using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.PlaceOfBirth;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class PlaceOfBirthTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Place of Birth Vault Example", null);

            PlaceOfBirthResponse created = await Create(staticVault);
            PlaceOfBirthResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.PlaceOfBirth);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.PlaceOfBirth, retrieved.PlaceOfBirth);
            Assert.AreEqual(created.PlaceOfBirthAlias, retrieved.PlaceOfBirthAlias);

            PlaceOfBirthResponse createdWithState = await CreateWithState(staticVault);
            PlaceOfBirthResponse retrievedWithState = await Retrieve(staticVault, createdWithState.Id);

            await RetrieveFromRealData(staticVault, createdWithState.PlaceOfBirth);
            await Delete(staticVault, retrievedWithState.Id);

            Assert.AreEqual(createdWithState.Id, retrievedWithState.Id);
            Assert.AreEqual(createdWithState.PlaceOfBirth, retrievedWithState.PlaceOfBirth);
            Assert.AreEqual(createdWithState.PlaceOfBirthAlias, retrievedWithState.PlaceOfBirthAlias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<PlaceOfBirthResponse> Create(StaticVault vault)
        {
            var name = "Example With Space";
            return await vault.PlaceOfBirth.Create(name);
        }

        private async Task<PlaceOfBirthResponse> CreateWithState(StaticVault vault)
        {
            var name = "PlaceOfBirth With State Example";
            var state = "IL";

            return await vault.PlaceOfBirth.Create(name, state, null);
        }

        private async Task<PlaceOfBirthResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.PlaceOfBirth.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String placeOfBirth)
        {
            await vault.PlaceOfBirth.RetrieveFromRealData(placeOfBirth);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.PlaceOfBirth.Delete(id);
        }
    }
}
