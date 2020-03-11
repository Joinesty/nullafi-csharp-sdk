using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.DateOfBirth;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{

    [TestClass]
    [TestCategory("Integration")]
    public class DateOfBirthTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Date of Birth Vault Example", null);

            DateOfBirthResponse created = await Create(staticVault);
            DateOfBirthResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.DateOfBirth);
            await Delete(staticVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.DateOfBirth, retrieved.DateOfBirth);
            Assert.AreEqual(created.DateOfBirthAlias, retrieved.DateOfBirthAlias);

            DateOfBirthResponse createdWithYear = await CreateWithYear(staticVault);
            DateOfBirthResponse retrievedWithYear = await Retrieve(staticVault, createdWithYear.Id);

            await RetrieveFromRealData(staticVault, createdWithYear.DateOfBirth);
            await Delete(staticVault, retrievedWithYear.Id);

            Assert.AreEqual(createdWithYear.Id, retrievedWithYear.Id);
            Assert.AreEqual(createdWithYear.DateOfBirth, retrievedWithYear.DateOfBirth);
            Assert.AreEqual(createdWithYear.DateOfBirthAlias, retrievedWithYear.DateOfBirthAlias);

            DateOfBirthResponse createdWithMonth = await CreateWithMonth(staticVault);
            DateOfBirthResponse retrievedWithMonth = await Retrieve(staticVault, createdWithMonth.Id);

            await RetrieveFromRealData(staticVault, created.DateOfBirth);
            await Delete(staticVault, retrievedWithMonth.Id);

            Assert.AreEqual(createdWithMonth.Id, retrievedWithMonth.Id);
            Assert.AreEqual(createdWithMonth.DateOfBirth, retrievedWithMonth.DateOfBirth);
            Assert.AreEqual(createdWithMonth.DateOfBirthAlias, retrievedWithMonth.DateOfBirthAlias);

            DateOfBirthResponse createdWithYearMonth = await CreateWithYearMonth(staticVault);
            DateOfBirthResponse retrievedWithYearMonth = await Retrieve(staticVault, createdWithYearMonth.Id);

            await RetrieveFromRealData(staticVault, createdWithYearMonth.DateOfBirth);
            await Delete(staticVault, retrievedWithYearMonth.Id);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<DateOfBirthResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.DateOfBirth.Create(name);
        }

        private async Task<DateOfBirthResponse> CreateWithMonth(StaticVault vault)
        {
            var name = "example";
            int month = 1;

            return await vault.DateOfBirth.Create(name, null, month);
        }

        private async Task<DateOfBirthResponse> CreateWithYear(StaticVault vault)
        {
            var name = "example";
            int year = 1991;

            return await vault.DateOfBirth.Create(name, year);
        }

        private async Task<DateOfBirthResponse> CreateWithYearMonth(StaticVault vault)
        {
            var name = "example";
            int month = 1;
            int year = 1991;

            return await vault.DateOfBirth.Create(name, year, month);
        }

        private async Task<DateOfBirthResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.DateOfBirth.Retrieve(id);

        }

        private async Task RetrieveFromRealData(StaticVault vault, String dateOfBirth)
        {
            await vault.DateOfBirth.RetrieveFromRealData(dateOfBirth);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.DateOfBirth.Delete(id);
        }
    }
}
