using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.Race;
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
    public class RaceManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string raceId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string raceAlias = "race example";
        string race = "real race example";
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
        public async Task GivenRequestToCreateARaceAliasWithTags_WhenCreatingAlias_ShouldReturnARaceAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/race").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new RaceResponse
                 {
                     Id = raceId,
                     Race = request.Value<string>("race"),
                     RaceAlias = raceAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var raceResponse = await StaticVault.Race.Create(race, tags);

            Assert.AreEqual(raceResponse.Id, raceId);
            Assert.AreEqual(raceResponse.Race, race);
            Assert.AreEqual(raceResponse.RaceAlias, raceAlias);
            CollectionAssert.AreEqual(raceResponse.Tags, tags);
            Assert.AreEqual(raceResponse.UpdatedAt, now);
            Assert.AreEqual(raceResponse.CreatedAt, now);
            Assert.IsNotNull(raceResponse.AuthTag);
            Assert.IsNotNull(raceResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateARaceAlias_WhenCreatingAlias_ShouldReturnARaceAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/race").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new RaceResponse
                 {
                     Id = raceId,
                     Race = request.Value<string>("race"),
                     RaceAlias = raceAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var raceResponse = await StaticVault.Race.Create(race);

            Assert.AreEqual(raceResponse.Id, raceId);
            Assert.AreEqual(raceResponse.Race, race);
            Assert.AreEqual(raceResponse.RaceAlias, raceAlias);
            Assert.AreEqual(raceResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(raceResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(raceResponse.AuthTag);
            Assert.IsNotNull(raceResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveARaceAlias_WhenRetrievingAlias_ShouldReturnARaceAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/race/{raceId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, race);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new RaceResponse
                 {
                     Id = raceId,
                     Race = encryptedData.EncryptedData,
                     RaceAlias = raceAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var raceResponse = await StaticVault.Race.Retrieve(raceId);

            Assert.AreEqual(raceResponse.Id, raceId);
            Assert.AreEqual(raceResponse.Race, race);
            Assert.AreEqual(raceResponse.RaceAlias, raceAlias);
            Assert.AreEqual(raceResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(raceResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(raceResponse.AuthTag);
            Assert.IsNotNull(raceResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveARaceAliasFromRealData_WhenRetrievingAlias_ShouldReturnARaceAlias()
        {
            var hash = StaticVault.Hash(race);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/race")
                .WithParam("hash").WithParam("tags")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, race);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<RaceResponse> { new RaceResponse
                 {
                     Id = raceId,
                     Race = encryptedData.EncryptedData,
                     RaceAlias = raceAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var raceResponses = await StaticVault.Race.RetrieveFromRealData(race, tags);


            raceResponses.ForEach(raceResponse =>
            {
                Assert.AreEqual(raceResponse.Id, raceId);
                Assert.AreEqual(raceResponse.Race, race);
                Assert.AreEqual(raceResponse.RaceAlias, raceAlias);
                Assert.AreEqual(raceResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(raceResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(raceResponse.AuthTag);
                Assert.IsNotNull(raceResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteARaceAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/race/{raceId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.Race.Delete(raceId);
        }
    }
}

