using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.FirstName;
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
    public class FirstNameManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string firstnameId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string firstnameAlias = "firstname example";
        string firstname = "real firstname example";
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
        public async Task GivenRequestToCreateAFirstNameAliasWithTags_WhenCreatingAlias_ShouldReturnAFirstNameAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/firstname").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new FirstNameResponse
                 {
                     Id = firstnameId,
                     FirstName = request.Value<string>("firstname"),
                     FirstNameAlias = firstnameAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var firstnameResponse = await StaticVault.FirstName.Create(firstname, tags);

            Assert.AreEqual(firstnameResponse.Id, firstnameId);
            Assert.AreEqual(firstnameResponse.FirstName, firstname);
            Assert.AreEqual(firstnameResponse.FirstNameAlias, firstnameAlias);
            CollectionAssert.AreEqual(firstnameResponse.Tags, tags);
            Assert.AreEqual(firstnameResponse.UpdatedAt, now);
            Assert.AreEqual(firstnameResponse.CreatedAt, now);
            Assert.IsNotNull(firstnameResponse.AuthTag);
            Assert.IsNotNull(firstnameResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateAFirstNameAlias_WhenCreatingAlias_ShouldReturnAFirstNameAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/firstname").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);
                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new FirstNameResponse
                 {
                     Id = firstnameId,
                     FirstName = request.Value<string>("firstname"),
                     FirstNameAlias = firstnameAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var firstnameResponse = await StaticVault.FirstName.Create(firstname);

            Assert.AreEqual(firstnameResponse.Id, firstnameId);
            Assert.AreEqual(firstnameResponse.FirstName, firstname);
            Assert.AreEqual(firstnameResponse.FirstNameAlias, firstnameAlias);
            Assert.AreEqual(firstnameResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(firstnameResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(firstnameResponse.AuthTag);
            Assert.IsNotNull(firstnameResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAFirstNameAlias_WhenRetrievingAlias_ShouldReturnAFirstNameAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/firstname/{firstnameId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, firstname);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new FirstNameResponse
                 {
                     Id = firstnameId,
                     FirstName = encryptedData.EncryptedData,
                     FirstNameAlias = firstnameAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var firstnameResponse = await StaticVault.FirstName.Retrieve(firstnameId);

            Assert.AreEqual(firstnameResponse.Id, firstnameId);
            Assert.AreEqual(firstnameResponse.FirstName, firstname);
            Assert.AreEqual(firstnameResponse.FirstNameAlias, firstnameAlias);
            Assert.AreEqual(firstnameResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(firstnameResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(firstnameResponse.AuthTag);
            Assert.IsNotNull(firstnameResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAFirstNameAliasFromRealData_WhenRetrievingAlias_ShouldReturnAFirstNameAlias()
        {
            var hash = StaticVault.Hash(firstname);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/firstname")
                .WithParam("hash")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, firstname);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<FirstNameResponse> { new FirstNameResponse
                 {
                     Id = firstnameId,
                     FirstName = encryptedData.EncryptedData,
                     FirstNameAlias = firstnameAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var firstnameResponses = await StaticVault.FirstName.RetrieveFromRealData(firstname);


            firstnameResponses.ForEach(firstnameResponse =>
            {
                Assert.AreEqual(firstnameResponse.Id, firstnameId);
                Assert.AreEqual(firstnameResponse.FirstName, firstname);
                Assert.AreEqual(firstnameResponse.FirstNameAlias, firstnameAlias);
                Assert.AreEqual(firstnameResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(firstnameResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(firstnameResponse.AuthTag);
                Assert.IsNotNull(firstnameResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteAFirstNameAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/firstname/{firstnameId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.FirstName.Delete(firstnameId);
        }
    }
}

