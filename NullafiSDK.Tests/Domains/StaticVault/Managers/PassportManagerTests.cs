using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.Passport;
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
    public class PassportManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string passportId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string passportAlias = "passport example";
        string passport = "real passport example";
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
        public async Task GivenRequestToCreateAPassportAliasWithTags_WhenCreatingAlias_ShouldReturnAPassportAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/passport").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new PassportResponse
                 {
                     Id = passportId,
                     Passport = request.Value<string>("passport"),
                     PassportAlias = passportAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var passportResponse = await StaticVault.Passport.Create(passport, tags);

            Assert.AreEqual(passportResponse.Id, passportId);
            Assert.AreEqual(passportResponse.Passport, passport);
            Assert.AreEqual(passportResponse.PassportAlias, passportAlias);
            CollectionAssert.AreEqual(passportResponse.Tags, tags);
            Assert.AreEqual(passportResponse.UpdatedAt, now);
            Assert.AreEqual(passportResponse.CreatedAt, now);
            Assert.IsNotNull(passportResponse.AuthTag);
            Assert.IsNotNull(passportResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateAPassportAlias_WhenCreatingAlias_ShouldReturnAPassportAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/passport").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new PassportResponse
                 {
                     Id = passportId,
                     Passport = request.Value<string>("passport"),
                     PassportAlias = passportAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var passportResponse = await StaticVault.Passport.Create(passport);

            Assert.AreEqual(passportResponse.Id, passportId);
            Assert.AreEqual(passportResponse.Passport, passport);
            Assert.AreEqual(passportResponse.PassportAlias, passportAlias);
            Assert.AreEqual(passportResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(passportResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(passportResponse.AuthTag);
            Assert.IsNotNull(passportResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAPassportAlias_WhenRetrievingAlias_ShouldReturnAPassportAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/passport/{passportId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, passport);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new PassportResponse
                 {
                     Id = passportId,
                     Passport = encryptedData.EncryptedData,
                     PassportAlias = passportAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var passportResponse = await StaticVault.Passport.Retrieve(passportId);

            Assert.AreEqual(passportResponse.Id, passportId);
            Assert.AreEqual(passportResponse.Passport, passport);
            Assert.AreEqual(passportResponse.PassportAlias, passportAlias);
            Assert.AreEqual(passportResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(passportResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(passportResponse.AuthTag);
            Assert.IsNotNull(passportResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAPassportAliasFromRealData_WhenRetrievingAlias_ShouldReturnAPassportAlias()
        {
            var hash = StaticVault.Hash(passport);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/passport")
                .WithParam("hash").WithParam("tags")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, passport);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<PassportResponse> { new PassportResponse
                 {
                     Id = passportId,
                     Passport = encryptedData.EncryptedData,
                     PassportAlias = passportAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var passportResponses = await StaticVault.Passport.RetrieveFromRealData(passport, tags);


            passportResponses.ForEach(passportResponse =>
            {
                Assert.AreEqual(passportResponse.Id, passportId);
                Assert.AreEqual(passportResponse.Passport, passport);
                Assert.AreEqual(passportResponse.PassportAlias, passportAlias);
                Assert.AreEqual(passportResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(passportResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(passportResponse.AuthTag);
                Assert.IsNotNull(passportResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteAPassportAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/passport/{passportId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.Passport.Delete(passportId);
        }
    }
}

