using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.DriversLicense;
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
    public class DriversLicenseManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string driverslicenseId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string driverslicenseAlias = "driverslicense example";
        string driverslicense = "real driverslicense example";
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
        public async Task GivenRequestToCreateADriversLicenseAliasWithTags_WhenCreatingAlias_ShouldReturnADriversLicenseAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/driverslicense").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new DriversLicenseResponse
                 {
                     Id = driverslicenseId,
                     DriversLicense = request.Value<string>("driverslicense"),
                     DriversLicenseAlias = driverslicenseAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var driverslicenseResponse = await StaticVault.DriversLicense.Create(driverslicense, tags);

            Assert.AreEqual(driverslicenseResponse.Id, driverslicenseId);
            Assert.AreEqual(driverslicenseResponse.DriversLicense, driverslicense);
            Assert.AreEqual(driverslicenseResponse.DriversLicenseAlias, driverslicenseAlias);
            CollectionAssert.AreEqual(driverslicenseResponse.Tags, tags);
            Assert.AreEqual(driverslicenseResponse.UpdatedAt, now);
            Assert.AreEqual(driverslicenseResponse.CreatedAt, now);
            Assert.IsNotNull(driverslicenseResponse.AuthTag);
            Assert.IsNotNull(driverslicenseResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateADriversLicenseAlias_WhenCreatingAlias_ShouldReturnADriversLicenseAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/driverslicense").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new DriversLicenseResponse
                 {
                     Id = driverslicenseId,
                     DriversLicense = request.Value<string>("driverslicense"),
                     DriversLicenseAlias = driverslicenseAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var driverslicenseResponse = await StaticVault.DriversLicense.Create(driverslicense);

            Assert.AreEqual(driverslicenseResponse.Id, driverslicenseId);
            Assert.AreEqual(driverslicenseResponse.DriversLicense, driverslicense);
            Assert.AreEqual(driverslicenseResponse.DriversLicenseAlias, driverslicenseAlias);
            Assert.AreEqual(driverslicenseResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(driverslicenseResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(driverslicenseResponse.AuthTag);
            Assert.IsNotNull(driverslicenseResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveADriversLicenseAlias_WhenRetrievingAlias_ShouldReturnADriversLicenseAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/driverslicense/{driverslicenseId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, driverslicense);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new DriversLicenseResponse
                 {
                     Id = driverslicenseId,
                     DriversLicense = encryptedData.EncryptedData,
                     DriversLicenseAlias = driverslicenseAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var driverslicenseResponse = await StaticVault.DriversLicense.Retrieve(driverslicenseId);

            Assert.AreEqual(driverslicenseResponse.Id, driverslicenseId);
            Assert.AreEqual(driverslicenseResponse.DriversLicense, driverslicense);
            Assert.AreEqual(driverslicenseResponse.DriversLicenseAlias, driverslicenseAlias);
            Assert.AreEqual(driverslicenseResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(driverslicenseResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(driverslicenseResponse.AuthTag);
            Assert.IsNotNull(driverslicenseResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveADriversLicenseAliasFromRealData_WhenRetrievingAlias_ShouldReturnADriversLicenseAlias()
        {
            var hash = StaticVault.Hash(driverslicense);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/driverslicense")
                .WithParam("hash").WithParam("tags")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, driverslicense);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<DriversLicenseResponse> { new DriversLicenseResponse
                 {
                     Id = driverslicenseId,
                     DriversLicense = encryptedData.EncryptedData,
                     DriversLicenseAlias = driverslicenseAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var driverslicenseResponses = await StaticVault.DriversLicense.RetrieveFromRealData(driverslicense, tags);


            driverslicenseResponses.ForEach(driverslicenseResponse =>
            {
                Assert.AreEqual(driverslicenseResponse.Id, driverslicenseId);
                Assert.AreEqual(driverslicenseResponse.DriversLicense, driverslicense);
                Assert.AreEqual(driverslicenseResponse.DriversLicenseAlias, driverslicenseAlias);
                Assert.AreEqual(driverslicenseResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(driverslicenseResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(driverslicenseResponse.AuthTag);
                Assert.IsNotNull(driverslicenseResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteADriversLicenseAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/driverslicense/{driverslicenseId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.DriversLicense.Delete(driverslicenseId);
        }
    }
}

