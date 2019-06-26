using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.PlaceOfBirth;
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
    public class PlaceOfBirthManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string placeofbirthId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string placeofbirthAlias = "placeofbirth example";
        string placeofbirth = "real placeofbirth example";
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
        public async Task GivenRequestToCreateAPlaceOfBirthAliasWithTags_WhenCreatingAlias_ShouldReturnAPlaceOfBirthAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/placeofbirth").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new PlaceOfBirthResponse
                 {
                     Id = placeofbirthId,
                     PlaceOfBirth = request.Value<string>("placeofbirth"),
                     PlaceOfBirthAlias = placeofbirthAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var placeofbirthResponse = await StaticVault.PlaceOfBirth.Create(placeofbirth, tags);

            Assert.AreEqual(placeofbirthResponse.Id, placeofbirthId);
            Assert.AreEqual(placeofbirthResponse.PlaceOfBirth, placeofbirth);
            Assert.AreEqual(placeofbirthResponse.PlaceOfBirthAlias, placeofbirthAlias);
            CollectionAssert.AreEqual(placeofbirthResponse.Tags, tags);
            Assert.AreEqual(placeofbirthResponse.UpdatedAt, now);
            Assert.AreEqual(placeofbirthResponse.CreatedAt, now);
            Assert.IsNotNull(placeofbirthResponse.AuthTag);
            Assert.IsNotNull(placeofbirthResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateAPlaceOfBirthAlias_WhenCreatingAlias_ShouldReturnAPlaceOfBirthAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/placeofbirth").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new PlaceOfBirthResponse
                 {
                     Id = placeofbirthId,
                     PlaceOfBirth = request.Value<string>("placeofbirth"),
                     PlaceOfBirthAlias = placeofbirthAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var placeofbirthResponse = await StaticVault.PlaceOfBirth.Create(placeofbirth);

            Assert.AreEqual(placeofbirthResponse.Id, placeofbirthId);
            Assert.AreEqual(placeofbirthResponse.PlaceOfBirth, placeofbirth);
            Assert.AreEqual(placeofbirthResponse.PlaceOfBirthAlias, placeofbirthAlias);
            Assert.AreEqual(placeofbirthResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(placeofbirthResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(placeofbirthResponse.AuthTag);
            Assert.IsNotNull(placeofbirthResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAPlaceOfBirthAlias_WhenRetrievingAlias_ShouldReturnAPlaceOfBirthAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/placeofbirth/{placeofbirthId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, placeofbirth);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new PlaceOfBirthResponse
                 {
                     Id = placeofbirthId,
                     PlaceOfBirth = encryptedData.EncryptedData,
                     PlaceOfBirthAlias = placeofbirthAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var placeofbirthResponse = await StaticVault.PlaceOfBirth.Retrieve(placeofbirthId);

            Assert.AreEqual(placeofbirthResponse.Id, placeofbirthId);
            Assert.AreEqual(placeofbirthResponse.PlaceOfBirth, placeofbirth);
            Assert.AreEqual(placeofbirthResponse.PlaceOfBirthAlias, placeofbirthAlias);
            Assert.AreEqual(placeofbirthResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(placeofbirthResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(placeofbirthResponse.AuthTag);
            Assert.IsNotNull(placeofbirthResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAPlaceOfBirthAliasFromRealData_WhenRetrievingAlias_ShouldReturnAPlaceOfBirthAlias()
        {
            var hash = StaticVault.Hash(placeofbirth);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/placeofbirth")
                .WithParam("hash")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, placeofbirth);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<PlaceOfBirthResponse> { new PlaceOfBirthResponse
                 {
                     Id = placeofbirthId,
                     PlaceOfBirth = encryptedData.EncryptedData,
                     PlaceOfBirthAlias = placeofbirthAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var placeofbirthResponses = await StaticVault.PlaceOfBirth.RetrieveFromRealData(placeofbirth);


            placeofbirthResponses.ForEach(placeofbirthResponse =>
            {
                Assert.AreEqual(placeofbirthResponse.Id, placeofbirthId);
                Assert.AreEqual(placeofbirthResponse.PlaceOfBirth, placeofbirth);
                Assert.AreEqual(placeofbirthResponse.PlaceOfBirthAlias, placeofbirthAlias);
                Assert.AreEqual(placeofbirthResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(placeofbirthResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(placeofbirthResponse.AuthTag);
                Assert.IsNotNull(placeofbirthResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteAPlaceOfBirthAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/placeofbirth/{placeofbirthId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.PlaceOfBirth.Delete(placeofbirthId);
        }
    }
}

