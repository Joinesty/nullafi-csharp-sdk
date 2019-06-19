using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using System.Threading.Tasks;

namespace Nullafi.Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public async Task GivenTheNeedToCommunicateWithAPI_WhenUsingTheSDK_ShouldAuthenticate()
        {
            var client = new Client();
            await client.Authenticate(Mock.API_KEY);

            Assert.AreEqual(client.HashKey, Mock.HASH_KEY);
        }

        [TestMethod]
        public async Task GivenRequestToCreateStaticVault_WhenCreatingAStaticVault_ReturnAStaticVaultInstance()
        {
            var vaultId = "some-vault-id";
            var vaultName = "some-vault-name";
            var tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };

            Mock.Server.Given(Request.Create().WithPath("/vault/static").UsingPost())
            .RespondWith(
                Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody("{ \"id\": \"" + vaultId + "\", \"name\": \"" + vaultName + "\", \"tags\": [" + string.Join(",", tags.Select(x => $"\"{x}\"")) + "] }")
             );

            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            var vault = await client.CreateStaticVault(vaultName, tags);

            Assert.AreEqual(vault.VaultId, vaultId);
            Assert.AreEqual(vault.VaultName, vaultName);

            Assert.IsNotNull(vault.MasterKey);

            Assert.IsNotNull(vault.Address);
            Assert.IsNotNull(vault.DateOfBirth);
            Assert.IsNotNull(vault.DriversLicense);
            Assert.IsNotNull(vault.FirstName);
            Assert.IsNotNull(vault.Gender);
            Assert.IsNotNull(vault.Generic);
            Assert.IsNotNull(vault.LastName);
            Assert.IsNotNull(vault.Passport);
            Assert.IsNotNull(vault.PlaceOfBirth);
            Assert.IsNotNull(vault.Race);
            Assert.IsNotNull(vault.Random);
            Assert.IsNotNull(vault.Ssn);
            Assert.IsNotNull(vault.TaxPayer);
            Assert.IsNotNull(vault.VehicleRegistration);
        }

        [TestMethod]
        public async Task GivenRequestToCreateCommunicationVault_WhenCreatingACommunicationVault_ReturnACommunicationVaultInstance()
        {
            var vaultId = "some-vault-id";
            var vaultName = "some-vault-name";
            var tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };

            Mock.Server.Given(Request.Create().WithPath("/vault/communication").UsingPost())
            .RespondWith(
                Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody("{ \"id\": \"" + vaultId + "\", " +
                            "\"name\": \"" + vaultName + "\", " +
                            "\"tags\": [" + string.Join(",", tags.Select(x => $"\"{x}\"")) + "] " +
                          "}")
             );

            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            var vault = await client.CreateCommunicationVault(vaultName, tags);

            Assert.AreEqual(vault.VaultId, vaultId);
            Assert.AreEqual(vault.VaultName, vaultName);

            Assert.IsNotNull(vault.MasterKey);

            Assert.IsNotNull(vault.Email);
        }
    }
}
