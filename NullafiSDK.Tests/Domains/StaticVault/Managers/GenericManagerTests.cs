using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.Generic;
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
    public class GenericManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string genericId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string genericAlias = "generic example";
        string generic = "real generic example";
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
        public async Task GivenRequestToCreateAGenericAliasWithTags_WhenCreatingAlias_ShouldReturnAGenericAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/generic").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new GenericResponse
                 {
                     Id = genericId,
                     Data = request.Value<string>("data"),
                     Alias = genericAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var genericResponse = await StaticVault.Generic.Create(generic, @"\d{3}", tags);

            Assert.AreEqual(genericResponse.Id, genericId);
            Assert.AreEqual(genericResponse.Data, generic);
            Assert.AreEqual(genericResponse.Alias, genericAlias);
            CollectionAssert.AreEqual(genericResponse.Tags, tags);
            Assert.AreEqual(genericResponse.UpdatedAt, now);
            Assert.AreEqual(genericResponse.CreatedAt, now);
            Assert.IsNotNull(genericResponse.AuthTag);
            Assert.IsNotNull(genericResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateAGenericAlias_WhenCreatingAlias_ShouldReturnAGenericAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/generic").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new GenericResponse
                 {
                     Id = genericId,
                     Data = request.Value<string>("data"),
                     Alias = genericAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var genericResponse = await StaticVault.Generic.Create(generic, @"\d{3}");

            Assert.AreEqual(genericResponse.Id, genericId);
            Assert.AreEqual(genericResponse.Data, generic);
            Assert.AreEqual(genericResponse.Alias, genericAlias);
            Assert.AreEqual(genericResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(genericResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(genericResponse.AuthTag);
            Assert.IsNotNull(genericResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAGenericAlias_WhenRetrievingAlias_ShouldReturnAGenericAlias()
        {


            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/generic/{genericId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, generic);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new GenericResponse
                 {
                     Id = genericId,
                     Data = encryptedData.EncryptedData,
                     Alias = genericAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var genericResponse = await StaticVault.Generic.Retrieve(genericId);

            Assert.AreEqual(genericResponse.Id, genericId);
            Assert.AreEqual(genericResponse.Data, generic);
            Assert.AreEqual(genericResponse.Alias, genericAlias);
            Assert.AreEqual(genericResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(genericResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(genericResponse.AuthTag);
            Assert.IsNotNull(genericResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAGenericAliasFromRealData_WhenRetrievingAlias_ShouldReturnAGenericAlias()
        {
            var hash = StaticVault.Hash(generic);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/generic")
                .WithParam("hash")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, generic);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<GenericResponse> { new GenericResponse
                 {
                     Id = genericId,
                     Data = encryptedData.EncryptedData,
                     Alias = genericAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var genericResponses = await StaticVault.Generic.RetrieveFromRealData(generic);


            genericResponses.ForEach(genericResponse =>
            {
                Assert.AreEqual(genericResponse.Id, genericId);
                Assert.AreEqual(genericResponse.Data, generic);
                Assert.AreEqual(genericResponse.Alias, genericAlias);
                Assert.AreEqual(genericResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(genericResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(genericResponse.AuthTag);
                Assert.IsNotNull(genericResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteAGenericAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            var genericId = "42719977-66da-4b48-89e7-ea53e0b0db32";
            var now = DateTime.Now;

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/generic/{genericId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.Generic.Delete(genericId);
        }
    }
}
