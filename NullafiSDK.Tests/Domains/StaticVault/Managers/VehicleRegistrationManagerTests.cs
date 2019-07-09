using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.VehicleRegistration;
using Nullafi.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Nullafi.Tests.Domains.Static.Managers
{
    [TestClass]
    public class VehicleRegistrationManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string vehicleregistrationId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string vehicleregistrationAlias = "vehicleregistration example";
        string vehicleregistration = "real vehicleregistration example";
        List<string> tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };
        DateTime now = DateTime.Now;

        [ClassInitialize]
        public static async Task InstantiateVault(TestContext context)
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
            StaticVault = await client.CreateStaticVault(vaultName, tags);
        }

        [TestMethod]
        public async Task GivenRequestToCreateAVehicleRegistrationAliasWithTags_WhenCreatingAlias_ShouldReturnAVehicleRegistrationAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/vehicleregistration").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new VehicleRegistrationResponse
                 {
                     Id = vehicleregistrationId,
                     VehicleRegistration = request.Value<string>("vehicleregistration"),
                     VehicleRegistrationAlias = vehicleregistrationAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var vehicleregistrationResponse = await StaticVault.VehicleRegistration.Create(vehicleregistration, tags);

            Assert.AreEqual(vehicleregistrationResponse.Id, vehicleregistrationId);
            Assert.AreEqual(vehicleregistrationResponse.VehicleRegistration, vehicleregistration);
            Assert.AreEqual(vehicleregistrationResponse.VehicleRegistrationAlias, vehicleregistrationAlias);
            CollectionAssert.AreEqual(vehicleregistrationResponse.Tags, tags);
            Assert.AreEqual(vehicleregistrationResponse.UpdatedAt, now);
            Assert.AreEqual(vehicleregistrationResponse.CreatedAt, now);
            Assert.IsNotNull(vehicleregistrationResponse.AuthTag);
            Assert.IsNotNull(vehicleregistrationResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateAVehicleRegistrationAlias_WhenCreatingAlias_ShouldReturnAVehicleRegistrationAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/vehicleregistration").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new VehicleRegistrationResponse
                 {
                     Id = vehicleregistrationId,
                     VehicleRegistration = request.Value<string>("vehicleregistration"),
                     VehicleRegistrationAlias = vehicleregistrationAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var vehicleregistrationResponse = await StaticVault.VehicleRegistration.Create(vehicleregistration);

            Assert.AreEqual(vehicleregistrationResponse.Id, vehicleregistrationId);
            Assert.AreEqual(vehicleregistrationResponse.VehicleRegistration, vehicleregistration);
            Assert.AreEqual(vehicleregistrationResponse.VehicleRegistrationAlias, vehicleregistrationAlias);
            Assert.AreEqual(vehicleregistrationResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(vehicleregistrationResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(vehicleregistrationResponse.AuthTag);
            Assert.IsNotNull(vehicleregistrationResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAVehicleRegistrationAlias_WhenRetrievingAlias_ShouldReturnAVehicleRegistrationAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/vehicleregistration/{vehicleregistrationId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, vehicleregistration);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new VehicleRegistrationResponse
                 {
                     Id = vehicleregistrationId,
                     VehicleRegistration = encryptedData.EncryptedData,
                     VehicleRegistrationAlias = vehicleregistrationAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var vehicleregistrationResponse = await StaticVault.VehicleRegistration.Retrieve(vehicleregistrationId);

            Assert.AreEqual(vehicleregistrationResponse.Id, vehicleregistrationId);
            Assert.AreEqual(vehicleregistrationResponse.VehicleRegistration, vehicleregistration);
            Assert.AreEqual(vehicleregistrationResponse.VehicleRegistrationAlias, vehicleregistrationAlias);
            Assert.AreEqual(vehicleregistrationResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(vehicleregistrationResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(vehicleregistrationResponse.AuthTag);
            Assert.IsNotNull(vehicleregistrationResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAVehicleRegistrationAliasFromRealData_WhenRetrievingAlias_ShouldReturnAVehicleRegistrationAlias()
        {
            var hash = StaticVault.Hash(vehicleregistration);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/vehicleregistration")
                .WithParam("hash").WithParam("tags")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, vehicleregistration);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<VehicleRegistrationResponse> { new VehicleRegistrationResponse
                 {
                     Id = vehicleregistrationId,
                     VehicleRegistration = encryptedData.EncryptedData,
                     VehicleRegistrationAlias = vehicleregistrationAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var vehicleregistrationResponses = await StaticVault.VehicleRegistration.RetrieveFromRealData(vehicleregistration, tags);


            vehicleregistrationResponses.ForEach(vehicleregistrationResponse =>
            {
                Assert.AreEqual(vehicleregistrationResponse.Id, vehicleregistrationId);
                Assert.AreEqual(vehicleregistrationResponse.VehicleRegistration, vehicleregistration);
                Assert.AreEqual(vehicleregistrationResponse.VehicleRegistrationAlias, vehicleregistrationAlias);
                Assert.AreEqual(vehicleregistrationResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(vehicleregistrationResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(vehicleregistrationResponse.AuthTag);
                Assert.IsNotNull(vehicleregistrationResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteAVehicleRegistrationAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/vehicleregistration/{vehicleregistrationId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.VehicleRegistration.Delete(vehicleregistrationId);
        }
    }
}


