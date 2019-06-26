using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.TaxPayer;
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
    public class TaxPayerManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string taxpayerId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string taxpayerAlias = "taxpayer example";
        string taxpayer = "real taxpayer example";
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
        public async Task GivenRequestToCreateATaxPayerAliasWithTags_WhenCreatingAlias_ShouldReturnATaxPayerAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/taxpayer").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new TaxPayerResponse
                 {
                     Id = taxpayerId,
                     TaxPayer = request.Value<string>("taxpayer"),
                     TaxPayerAlias = taxpayerAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var taxpayerResponse = await StaticVault.TaxPayer.Create(taxpayer, tags);

            Assert.AreEqual(taxpayerResponse.Id, taxpayerId);
            Assert.AreEqual(taxpayerResponse.TaxPayer, taxpayer);
            Assert.AreEqual(taxpayerResponse.TaxPayerAlias, taxpayerAlias);
            CollectionAssert.AreEqual(taxpayerResponse.Tags, tags);
            Assert.AreEqual(taxpayerResponse.UpdatedAt, now);
            Assert.AreEqual(taxpayerResponse.CreatedAt, now);
            Assert.IsNotNull(taxpayerResponse.AuthTag);
            Assert.IsNotNull(taxpayerResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateATaxPayerAlias_WhenCreatingAlias_ShouldReturnATaxPayerAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/taxpayer").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new TaxPayerResponse
                 {
                     Id = taxpayerId,
                     TaxPayer = request.Value<string>("taxpayer"),
                     TaxPayerAlias = taxpayerAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var taxpayerResponse = await StaticVault.TaxPayer.Create(taxpayer);

            Assert.AreEqual(taxpayerResponse.Id, taxpayerId);
            Assert.AreEqual(taxpayerResponse.TaxPayer, taxpayer);
            Assert.AreEqual(taxpayerResponse.TaxPayerAlias, taxpayerAlias);
            Assert.AreEqual(taxpayerResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(taxpayerResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(taxpayerResponse.AuthTag);
            Assert.IsNotNull(taxpayerResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveATaxPayerAlias_WhenRetrievingAlias_ShouldReturnATaxPayerAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/taxpayer/{taxpayerId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, taxpayer);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new TaxPayerResponse
                 {
                     Id = taxpayerId,
                     TaxPayer = encryptedData.EncryptedData,
                     TaxPayerAlias = taxpayerAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var taxpayerResponse = await StaticVault.TaxPayer.Retrieve(taxpayerId);

            Assert.AreEqual(taxpayerResponse.Id, taxpayerId);
            Assert.AreEqual(taxpayerResponse.TaxPayer, taxpayer);
            Assert.AreEqual(taxpayerResponse.TaxPayerAlias, taxpayerAlias);
            Assert.AreEqual(taxpayerResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(taxpayerResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(taxpayerResponse.AuthTag);
            Assert.IsNotNull(taxpayerResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveATaxPayerAliasFromRealData_WhenRetrievingAlias_ShouldReturnATaxPayerAlias()
        {
            var hash = StaticVault.Hash(taxpayer);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/taxpayer")
                .WithParam("hash").WithParam("tags")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, taxpayer);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<TaxPayerResponse> { new TaxPayerResponse
                 {
                     Id = taxpayerId,
                     TaxPayer = encryptedData.EncryptedData,
                     TaxPayerAlias = taxpayerAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var taxpayerResponses = await StaticVault.TaxPayer.RetrieveFromRealData(taxpayer, tags);


            taxpayerResponses.ForEach(taxpayerResponse =>
            {
                Assert.AreEqual(taxpayerResponse.Id, taxpayerId);
                Assert.AreEqual(taxpayerResponse.TaxPayer, taxpayer);
                Assert.AreEqual(taxpayerResponse.TaxPayerAlias, taxpayerAlias);
                Assert.AreEqual(taxpayerResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(taxpayerResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(taxpayerResponse.AuthTag);
                Assert.IsNotNull(taxpayerResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteATaxPayerAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/taxpayer/{taxpayerId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.TaxPayer.Delete(taxpayerId);
        }
    }
}

