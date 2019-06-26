using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using System.Threading.Tasks;
using Nullafi.Tests.Helpers;
using WireMock;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Dynamic;

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
              .WithBody(JsonConvert.SerializeObject(new
              {
                  Id = vaultId,
                  Name = vaultName,
                  Tags = tags
              }))
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
        public async Task GivenRequestToRetrieveStaticVault_WhenRetrievingAStaticVault_ReturnAStaticVaultInstance()
        {
            var vaultId = "some-vault-id";
            var vaultName = "some-vault-name";
            var tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{vaultId}").UsingGet())
          .RespondWith(
              Response.Create()
              .WithStatusCode(HttpStatusCode.OK)
              .WithBody(JsonConvert.SerializeObject(new
              {
                  Id = vaultId,
                  Name = vaultName,
                  Tags = tags
              }))
              );

            var security = new Security();
            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            var vault = await client.RetrieveStaticVault(vaultId, security.Aes.GenerateStringMasterKey());

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
        public async Task GivenRequestToDeleteStaticVault_WhenDeletingACommunicationVault_ReturnAOkResponse()
        {
            var vaultId = "some-vault-id";

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{vaultId}").UsingDelete())
          .RespondWith(
              Response.Create()
              .WithStatusCode(HttpStatusCode.OK)
              .WithBody(JsonConvert.SerializeObject(new
              {
                  Ok = true
              }))
              );

            var security = new Security();
            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            await client.DeleteStaticVault(vaultId);
        }

        [TestMethod]
        public async Task GivenRequestToCreateCommunicationVault_WhenCreatingACommunicationVault_ReturnACommunicationVaultInstance()
        {
            var vaultId = "some-vault-id";
            var vaultName = "some-vault-name";
            var tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };

            var security = new Security();
            var vaultMasterkey = security.Aes.GenerateStringMasterKey();

            Mock.Server.Given(Request.Create().WithPath("/vault/communication").UsingPost())
            .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
            {
                var secLevelMasterkey = security.Aes.GenerateStringMasterKey();
                var secLevelIv = security.Aes.GenerateStringIv();
                var encryptedMasterKey = security.Aes.Encrypt(secLevelMasterkey, secLevelIv, vaultMasterkey);

                var request = JObject.Parse(requestMessage.Body);

                return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Id = vaultId,
                     Name = vaultName,
                     MasterKey = encryptedMasterKey.EncryptedData,
                     encryptedMasterKey.AuthTag,
                     encryptedMasterKey.Iv,
                     SessionKey = RSAHelper.EncryptWithPubKey(secLevelMasterkey, request.Value<string>("publicKey")),
                     Tags = tags
                 }));
            }));

            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            var vault = await client.CreateCommunicationVault(vaultName, tags);

            Assert.AreEqual(vault.VaultId, vaultId);
            Assert.AreEqual(vault.VaultName, vaultName);
            Assert.AreEqual(vault.MasterKey, vaultMasterkey);

            Assert.IsNotNull(vault.Email);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveCommunicationVault_WhenRetrievingACommunicationVault_ReturnACommunicationVaultInstance()
        {
            var vaultId = "some-vault-id";
            var vaultName = "some-vault-name";
            var tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };

            Mock.Server.Given(Request.Create().WithPath($"/vault/communication/{vaultId}").UsingGet())
          .RespondWith(
              Response.Create()
              .WithStatusCode(HttpStatusCode.OK)
              .WithBody(JsonConvert.SerializeObject(new
              {
                  Id = vaultId,
                  Name = vaultName,
                  Tags = tags
              }))
              );

            var security = new Security();
            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            var vault = await client.RetrieveCommunicationVault(vaultId, security.Aes.GenerateStringMasterKey());

            Assert.AreEqual(vault.VaultId, vaultId);
            Assert.AreEqual(vault.VaultName, vaultName);

            Assert.IsNotNull(vault.MasterKey);

            Assert.IsNotNull(vault.Email);
        }

        [TestMethod]
        public async Task GivenRequestToDeleteCommunicationVault_WhenDeletingACommunicationVault_ReturnAOkResponse()
        {
            var vaultId = "some-vault-id";

            Mock.Server.Given(Request.Create().WithPath($"/vault/communication/{vaultId}").UsingDelete())
          .RespondWith(
              Response.Create()
              .WithStatusCode(HttpStatusCode.OK)
              .WithBody(JsonConvert.SerializeObject(new
              {
                  Ok = true
              }))
              );

            var security = new Security();
            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            await client.DeleteCommunicationVault(vaultId);
        }
    }
}
