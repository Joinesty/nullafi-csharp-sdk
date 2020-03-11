using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.FirstName;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class FirstNameTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("FirstName Vault Example", null);

            FirstNameResponse created = await Create(staticVault);
            FirstNameResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.FirstName);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.FirstName, retrieved.FirstName);
            Assert.AreEqual(created.FirstNameAlias, retrieved.FirstNameAlias);

            FirstNameResponse createdWithGender = await CreateWithGender(staticVault);
            FirstNameResponse retrievedWithGender = await Retrieve(staticVault, createdWithGender.Id);

            await RetrieveFromRealData(staticVault, createdWithGender.FirstName);
            await Delete(staticVault, retrievedWithGender.Id);

            Assert.AreEqual(createdWithGender.Id, retrievedWithGender.Id);
            Assert.AreEqual(createdWithGender.FirstName, retrievedWithGender.FirstName);
            Assert.AreEqual(createdWithGender.FirstNameAlias, retrievedWithGender.FirstNameAlias);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<FirstNameResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.FirstName.Create(name);
        }

        private async Task<FirstNameResponse> CreateWithGender(StaticVault vault)
        {
            var name = "example";
            var gender = "male";

            return await vault.FirstName.Create(name, gender);
        }

        private async Task<FirstNameResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.FirstName.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String firstName)
        {
            await vault.FirstName.RetrieveFromRealData(firstName);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.FirstName.Delete(id);
        }
    }
}
