using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.Ssn;
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
    public class SsnManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string ssnId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string ssnAlias = "ssn example";
        string ssn = "real ssn example";
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
        public async Task GivenRequestToCreateASsnAliasWithTags_WhenCreatingAlias_ShouldReturnASsnAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/ssn").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new SsnResponse
                 {
                     Id = ssnId,
                     Ssn = request.Value<string>("ssn"),
                     SsnAlias = ssnAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var ssnResponse = await StaticVault.Ssn.Create(ssn, tags);

            Assert.AreEqual(ssnResponse.Id, ssnId);
            Assert.AreEqual(ssnResponse.Ssn, ssn);
            Assert.AreEqual(ssnResponse.SsnAlias, ssnAlias);
            CollectionAssert.AreEqual(ssnResponse.Tags, tags);
            Assert.AreEqual(ssnResponse.UpdatedAt, now);
            Assert.AreEqual(ssnResponse.CreatedAt, now);
            Assert.IsNotNull(ssnResponse.AuthTag);
            Assert.IsNotNull(ssnResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateASsnAlias_WhenCreatingAlias_ShouldReturnASsnAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/ssn").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new SsnResponse
                 {
                     Id = ssnId,
                     Ssn = request.Value<string>("ssn"),
                     SsnAlias = ssnAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var ssnResponse = await StaticVault.Ssn.Create(ssn);

            Assert.AreEqual(ssnResponse.Id, ssnId);
            Assert.AreEqual(ssnResponse.Ssn, ssn);
            Assert.AreEqual(ssnResponse.SsnAlias, ssnAlias);
            Assert.AreEqual(ssnResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(ssnResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(ssnResponse.AuthTag);
            Assert.IsNotNull(ssnResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveASsnAlias_WhenRetrievingAlias_ShouldReturnASsnAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/ssn/{ssnId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, ssn);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new SsnResponse
                 {
                     Id = ssnId,
                     Ssn = encryptedData.EncryptedData,
                     SsnAlias = ssnAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var ssnResponse = await StaticVault.Ssn.Retrieve(ssnId);

            Assert.AreEqual(ssnResponse.Id, ssnId);
            Assert.AreEqual(ssnResponse.Ssn, ssn);
            Assert.AreEqual(ssnResponse.SsnAlias, ssnAlias);
            Assert.AreEqual(ssnResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(ssnResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(ssnResponse.AuthTag);
            Assert.IsNotNull(ssnResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveASsnAliasFromRealData_WhenRetrievingAlias_ShouldReturnASsnAlias()
        {
            var hash = StaticVault.Hash(ssn);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/ssn")
                .WithParam("hash").WithParam("tags")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, ssn);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<SsnResponse> { new SsnResponse
                 {
                     Id = ssnId,
                     Ssn = encryptedData.EncryptedData,
                     SsnAlias = ssnAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var ssnResponses = await StaticVault.Ssn.RetrieveFromRealData(ssn, tags);


            ssnResponses.ForEach(ssnResponse =>
            {
                Assert.AreEqual(ssnResponse.Id, ssnId);
                Assert.AreEqual(ssnResponse.Ssn, ssn);
                Assert.AreEqual(ssnResponse.SsnAlias, ssnAlias);
                Assert.AreEqual(ssnResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(ssnResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(ssnResponse.AuthTag);
                Assert.IsNotNull(ssnResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteASsnAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/ssn/{ssnId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.Ssn.Delete(ssnId);
        }
    }
}

