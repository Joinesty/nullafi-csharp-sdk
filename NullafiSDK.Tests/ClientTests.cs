using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Nullafi.Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public async void GivenTheNeedToCommunicateWithAPI_WhenUsingTheSDK_ShouldAuthenticate()
        {
            var hashKey = "some-hash-key";
            FluentMockServer.Start("https://dashboard-api.nullafi.com")
                .Given(Request.Create().WithPath("/authentication/token").UsingPost())
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(@"{ ""token"": ""some-token"", ""hashKey"": """ + hashKey + @""" }")
                 );

            var client = new Client();
            await client.Authenticate("API_KEY");

            Assert.AreEqual(client.HashKey, "some-hash-key");
        }

        [TestMethod]
        public async void GivenRequestToCreateStaticVault_WhenCreatingAStaticVault_ReturnAStaticVaultInstance()
        {
            var vaultId = "some-vault-id";
            var vaultName = "some-vault-name";
            var tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };

            FluentMockServer.Start("https://dashboard-api.nullafi.com")
                .Given(Request.Create().WithPath("/vault/static").UsingPost())
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody("{ \"id\": " + vaultId + "\"\", \"name\": \"" + vaultName + "\", \"tags\": [" + string.Join(",", tags.Select(x => $"\"{x}\"")) + "] }")
                 );

            var client = new Client();
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
    }
}
