using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Domains.CommunicationVault;
using Nullafi.Domains.CommunicationVault.Managers.Email;
using System;
using System.Threading.Tasks;

namespace NullafiSDKExamples.Examples.Communication.Managers
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public async Task Run()
        {
            var sdk = new Nullafi.NullafiSDK(Environment.GetEnvironmentVariable("API_KEY"));
            var client = await sdk.CreateClient();

            var communicationVault = await client.CreateCommunicationVault("Email Vault Example", null);

            EmailResponse created = await Create(communicationVault);
            EmailResponse retrieved = await Retrieve(communicationVault, created.Id);

            await RetrieveFromRealData(communicationVault, created.Email);
            await Delete(communicationVault, retrieved.Id);

            Assert.AreEqual(created.Id, retrieved.Id);
            Assert.AreEqual(created.Email, retrieved.Email);
            Assert.AreEqual(created.EmailAlias, retrieved.EmailAlias);

            await client.DeleteStaticVault(communicationVault.VaultId);
        }

        private async Task<EmailResponse> Create(CommunicationVault vault)
        {
            var name = "emai@example.com";
            return await vault.Email.Create(name);
        }

        private async Task<EmailResponse> Retrieve(CommunicationVault vault, String id)
        {
            return await vault.Email.Retrieve(id);
        }

        private async Task RetrieveFromRealData(CommunicationVault vault, String email)
        {
            await vault.Email.RetrieveFromRealData(email);
        }

        private async Task Delete(CommunicationVault vault, String id)
        {
            await vault.Email.Delete(id);
        }
    }
}
