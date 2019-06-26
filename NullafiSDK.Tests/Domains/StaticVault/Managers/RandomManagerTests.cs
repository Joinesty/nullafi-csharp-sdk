using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.Random;
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
    public class RandomManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string randomId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string randomAlias = "random example";
        string random = "real random example";
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
        public async Task GivenRequestToCreateARandomAliasWithTags_WhenCreatingAlias_ShouldReturnARandomAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/random").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new RandomResponse
                 {
                     Id = randomId,
                     Data = request.Value<string>("data"),
                     Alias = randomAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var randomResponse = await StaticVault.Random.Create(random, tags);

            Assert.AreEqual(randomResponse.Id, randomId);
            Assert.AreEqual(randomResponse.Data, random);
            Assert.AreEqual(randomResponse.Alias, randomAlias);
            CollectionAssert.AreEqual(randomResponse.Tags, tags);
            Assert.AreEqual(randomResponse.UpdatedAt, now);
            Assert.AreEqual(randomResponse.CreatedAt, now);
            Assert.IsNotNull(randomResponse.AuthTag);
            Assert.IsNotNull(randomResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateARandomAlias_WhenCreatingAlias_ShouldReturnARandomAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/random").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new RandomResponse
                 {
                     Id = randomId,
                     Data = request.Value<string>("data"),
                     Alias = randomAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var randomResponse = await StaticVault.Random.Create(random);

            Assert.AreEqual(randomResponse.Id, randomId);
            Assert.AreEqual(randomResponse.Data, random);
            Assert.AreEqual(randomResponse.Alias, randomAlias);
            Assert.AreEqual(randomResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(randomResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(randomResponse.AuthTag);
            Assert.IsNotNull(randomResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveARandomAlias_WhenRetrievingAlias_ShouldReturnARandomAlias()
        {


            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/random/{randomId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, random);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new RandomResponse
                 {
                     Id = randomId,
                     Data = encryptedData.EncryptedData,
                     Alias = randomAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var randomResponse = await StaticVault.Random.Retrieve(randomId);

            Assert.AreEqual(randomResponse.Id, randomId);
            Assert.AreEqual(randomResponse.Data, random);
            Assert.AreEqual(randomResponse.Alias, randomAlias);
            Assert.AreEqual(randomResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(randomResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(randomResponse.AuthTag);
            Assert.IsNotNull(randomResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveARandomAliasFromRealData_WhenRetrievingAlias_ShouldReturnARandomAlias()
        {
            var hash = StaticVault.Hash(random);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/random")
                .WithParam("hash")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, random);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<RandomResponse> { new RandomResponse
                 {
                     Id = randomId,
                     Data = encryptedData.EncryptedData,
                     Alias = randomAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var randomResponses = await StaticVault.Random.RetrieveFromRealData(random);


            randomResponses.ForEach(randomResponse =>
            {
                Assert.AreEqual(randomResponse.Id, randomId);
                Assert.AreEqual(randomResponse.Data, random);
                Assert.AreEqual(randomResponse.Alias, randomAlias);
                Assert.AreEqual(randomResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(randomResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(randomResponse.AuthTag);
                Assert.IsNotNull(randomResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteARandomAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            var randomId = "42719977-66da-4b48-89e7-ea53e0b0db32";
            var now = DateTime.Now;

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/random/{randomId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.Random.Delete(randomId);
        }
    }
}
