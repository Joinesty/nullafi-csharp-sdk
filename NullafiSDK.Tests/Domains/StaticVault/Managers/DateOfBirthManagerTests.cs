using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.DateOfBirth;
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
    public class DateOfBirthManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string dateofbirthId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string dateofbirthAlias = "dateofbirth example";
        string dateofbirth = "real dateofbirth example";
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
        public async Task GivenRequestToCreateADateOfBirthAliasWithTags_WhenCreatingAlias_ShouldReturnADateOfBirthAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/dateofbirth").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new DateOfBirthResponse
                 {
                     Id = dateofbirthId,
                     DateOfBirth = request.Value<string>("dateofbirth"),
                     DateOfBirthAlias = dateofbirthAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var dateofbirthResponse = await StaticVault.DateOfBirth.Create(dateofbirth, tags);

            Assert.AreEqual(dateofbirthResponse.Id, dateofbirthId);
            Assert.AreEqual(dateofbirthResponse.DateOfBirth, dateofbirth);
            Assert.AreEqual(dateofbirthResponse.DateOfBirthAlias, dateofbirthAlias);
            CollectionAssert.AreEqual(dateofbirthResponse.Tags, tags);
            Assert.AreEqual(dateofbirthResponse.UpdatedAt, now);
            Assert.AreEqual(dateofbirthResponse.CreatedAt, now);
            Assert.IsNotNull(dateofbirthResponse.AuthTag);
            Assert.IsNotNull(dateofbirthResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateADateOfBirthAlias_WhenCreatingAlias_ShouldReturnADateOfBirthAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/dateofbirth").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new DateOfBirthResponse
                 {
                     Id = dateofbirthId,
                     DateOfBirth = request.Value<string>("dateofbirth"),
                     DateOfBirthAlias = dateofbirthAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var dateofbirthResponse = await StaticVault.DateOfBirth.Create(dateofbirth);

            Assert.AreEqual(dateofbirthResponse.Id, dateofbirthId);
            Assert.AreEqual(dateofbirthResponse.DateOfBirth, dateofbirth);
            Assert.AreEqual(dateofbirthResponse.DateOfBirthAlias, dateofbirthAlias);
            Assert.AreEqual(dateofbirthResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(dateofbirthResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(dateofbirthResponse.AuthTag);
            Assert.IsNotNull(dateofbirthResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveADateOfBirthAlias_WhenRetrievingAlias_ShouldReturnADateOfBirthAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/dateofbirth/{dateofbirthId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, dateofbirth);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new DateOfBirthResponse
                 {
                     Id = dateofbirthId,
                     DateOfBirth = encryptedData.EncryptedData,
                     DateOfBirthAlias = dateofbirthAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var dateofbirthResponse = await StaticVault.DateOfBirth.Retrieve(dateofbirthId);

            Assert.AreEqual(dateofbirthResponse.Id, dateofbirthId);
            Assert.AreEqual(dateofbirthResponse.DateOfBirth, dateofbirth);
            Assert.AreEqual(dateofbirthResponse.DateOfBirthAlias, dateofbirthAlias);
            Assert.AreEqual(dateofbirthResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(dateofbirthResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(dateofbirthResponse.AuthTag);
            Assert.IsNotNull(dateofbirthResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveADateOfBirthAliasFromRealData_WhenRetrievingAlias_ShouldReturnADateOfBirthAlias()
        {
            var hash = StaticVault.Hash(dateofbirth);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/dateofbirth")
                .WithParam("hash").WithParam("tags")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, dateofbirth);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<DateOfBirthResponse> { new DateOfBirthResponse
                 {
                     Id = dateofbirthId,
                     DateOfBirth = encryptedData.EncryptedData,
                     DateOfBirthAlias = dateofbirthAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var dateofbirthResponses = await StaticVault.DateOfBirth.RetrieveFromRealData(dateofbirth, tags);


            dateofbirthResponses.ForEach(dateofbirthResponse =>
            {
                Assert.AreEqual(dateofbirthResponse.Id, dateofbirthId);
                Assert.AreEqual(dateofbirthResponse.DateOfBirth, dateofbirth);
                Assert.AreEqual(dateofbirthResponse.DateOfBirthAlias, dateofbirthAlias);
                Assert.AreEqual(dateofbirthResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(dateofbirthResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(dateofbirthResponse.AuthTag);
                Assert.IsNotNull(dateofbirthResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteADateOfBirthAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/dateofbirth/{dateofbirthId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.DateOfBirth.Delete(dateofbirthId);
        }
    }
}


