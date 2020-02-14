using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.StaticVault;
using Nullafi.Domains.StaticVault.Managers.Race;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Static.Managers
{
    [TestClass]
    public class RaceTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var staticVault = await client.CreateStaticVault("Address Vault Example", null);

            RaceResponse created = await Create(staticVault);
            RaceResponse retrieved = await Retrieve(staticVault, created.Id);

            await RetrieveFromRealData(staticVault, created.Race);
            await Delete(staticVault, retrieved.Id);

            await client.DeleteStaticVault(staticVault.VaultId);
        }

        private async Task<RaceResponse> Create(StaticVault vault)
        {
            var name = "example";
            return await vault.Race.Create(name);
        }

        private async Task<RaceResponse> Retrieve(StaticVault vault, String id)
        {
            return await vault.Race.Retrieve(id);
        }

        private async Task RetrieveFromRealData(StaticVault vault, String race)
        {
            await vault.Race.RetrieveFromRealData(race);
        }

        private async Task Delete(StaticVault vault, String id)
        {
            await vault.Race.Delete(id);
        }
    }
}
