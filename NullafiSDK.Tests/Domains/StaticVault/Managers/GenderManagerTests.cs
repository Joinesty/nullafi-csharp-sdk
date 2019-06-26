using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.Gender;
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
    public class GenderManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string genderId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string genderAlias = "gender example";
        string gender = "real gender example";
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
        public async Task GivenRequestToCreateAGenderAliasWithTags_WhenCreatingAlias_ShouldReturnAGenderAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/gender").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new GenderResponse
                 {
                     Id = genderId,
                     Gender = request.Value<string>("gender"),
                     GenderAlias = genderAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var genderResponse = await StaticVault.Gender.Create(gender, tags);

            Assert.AreEqual(genderResponse.Id, genderId);
            Assert.AreEqual(genderResponse.Gender, gender);
            Assert.AreEqual(genderResponse.GenderAlias, genderAlias);
            CollectionAssert.AreEqual(genderResponse.Tags, tags);
            Assert.AreEqual(genderResponse.UpdatedAt, now);
            Assert.AreEqual(genderResponse.CreatedAt, now);
            Assert.IsNotNull(genderResponse.AuthTag);
            Assert.IsNotNull(genderResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateAGenderAlias_WhenCreatingAlias_ShouldReturnAGenderAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/gender").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new GenderResponse
                 {
                     Id = genderId,
                     Gender = request.Value<string>("gender"),
                     GenderAlias = genderAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var genderResponse = await StaticVault.Gender.Create(gender);

            Assert.AreEqual(genderResponse.Id, genderId);
            Assert.AreEqual(genderResponse.Gender, gender);
            Assert.AreEqual(genderResponse.GenderAlias, genderAlias);
            Assert.AreEqual(genderResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(genderResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(genderResponse.AuthTag);
            Assert.IsNotNull(genderResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAGenderAlias_WhenRetrievingAlias_ShouldReturnAGenderAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/gender/{genderId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, gender);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new GenderResponse
                 {
                     Id = genderId,
                     Gender = encryptedData.EncryptedData,
                     GenderAlias = genderAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var genderResponse = await StaticVault.Gender.Retrieve(genderId);

            Assert.AreEqual(genderResponse.Id, genderId);
            Assert.AreEqual(genderResponse.Gender, gender);
            Assert.AreEqual(genderResponse.GenderAlias, genderAlias);
            Assert.AreEqual(genderResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(genderResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(genderResponse.AuthTag);
            Assert.IsNotNull(genderResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAGenderAliasFromRealData_WhenRetrievingAlias_ShouldReturnAGenderAlias()
        {
            var hash = StaticVault.Hash(gender);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/gender")
                .WithParam("hash")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, gender);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<GenderResponse> { new GenderResponse
                 {
                     Id = genderId,
                     Gender = encryptedData.EncryptedData,
                     GenderAlias = genderAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var genderResponses = await StaticVault.Gender.RetrieveFromRealData(gender);


            genderResponses.ForEach(genderResponse =>
            {
                Assert.AreEqual(genderResponse.Id, genderId);
                Assert.AreEqual(genderResponse.Gender, gender);
                Assert.AreEqual(genderResponse.GenderAlias, genderAlias);
                Assert.AreEqual(genderResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(genderResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(genderResponse.AuthTag);
                Assert.IsNotNull(genderResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteAGenderAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/gender/{genderId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.Gender.Delete(genderId);
        }
    }
}


