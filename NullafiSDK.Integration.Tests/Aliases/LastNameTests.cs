using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.LastName;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class LastNameTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Last Name Vault Example", null);

            LastNameResponse created = await Create(staticVault);
            LastNameResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, retrieved.LastName);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.LastName, retrieved.LastName);
            Assert.AreEqual(created.LastNameAlias, retrieved.LastNameAlias);

            LastNameResponse createdWithGender = await CreateWithGender(staticVault);
            LastNameResponse retrievedWithGender = await Retrieve(staticVault, createdWithGender.Id);

            await RetrieveFromRealData(staticVault, retrievedWithGender.LastName);
            await Delete(staticVault, retrievedWithGender.Id);

            Assert.AreEqual(createdWithGender.Id, retrievedWithGender.Id);
            Assert.AreEqual(createdWithGender.LastName, retrievedWithGender.LastName);
            Assert.AreEqual(createdWithGender.LastNameAlias, retrievedWithGender.LastNameAlias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<LastNameResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.LastName.Create(name);
        }

        private async Task<LastNameResponse> CreateWithGender(StaticVault vault)
        {
            var name = "example";
            var gender = "male";

            return await vault.LastName.Create(name, gender);
        }

        private async Task<LastNameResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.LastName.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String lastName)
        {
            await vault.LastName.RetrieveFromRealData(lastName);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.LastName.Delete(id);
        }
    }
}
