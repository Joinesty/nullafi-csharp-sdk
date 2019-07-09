using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.StaticVault.Managers.Address;
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
    public class AddressManagerTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        string addressId = "42719977-66da-4b48-89e7-ea53e0b0db32";
        string addressAlias = "address example";
        string address = "real address example";
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
        public async Task GivenRequestToCreateAAddressAliasWithTags_WhenCreatingAlias_ShouldReturnAAddressAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/address").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new AddressResponse
                 {
                     Id = addressId,
                     Address = request.Value<string>("address"),
                     AddressAlias = addressAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var addressResponse = await StaticVault.Address.Create(address, tags);

            Assert.AreEqual(addressResponse.Id, addressId);
            Assert.AreEqual(addressResponse.Address, address);
            Assert.AreEqual(addressResponse.AddressAlias, addressAlias);
            CollectionAssert.AreEqual(addressResponse.Tags, tags);
            Assert.AreEqual(addressResponse.UpdatedAt, now);
            Assert.AreEqual(addressResponse.CreatedAt, now);
            Assert.IsNotNull(addressResponse.AuthTag);
            Assert.IsNotNull(addressResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateAAddressAlias_WhenCreatingAlias_ShouldReturnAAddressAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/address").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new AddressResponse
                 {
                     Id = addressId,
                     Address = request.Value<string>("address"),
                     AddressAlias = addressAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var addressResponse = await StaticVault.Address.Create(address);

            Assert.AreEqual(addressResponse.Id, addressId);
            Assert.AreEqual(addressResponse.Address, address);
            Assert.AreEqual(addressResponse.AddressAlias, addressAlias);
            Assert.AreEqual(addressResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(addressResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(addressResponse.AuthTag);
            Assert.IsNotNull(addressResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAAddressAlias_WhenRetrievingAlias_ShouldReturnAAddressAlias()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/address/{addressId}").UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, address);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new AddressResponse
                 {
                     Id = addressId,
                     Address = encryptedData.EncryptedData,
                     AddressAlias = addressAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var addressResponse = await StaticVault.Address.Retrieve(addressId);

            Assert.AreEqual(addressResponse.Id, addressId);
            Assert.AreEqual(addressResponse.Address, address);
            Assert.AreEqual(addressResponse.AddressAlias, addressAlias);
            Assert.AreEqual(addressResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.AreEqual(addressResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
            Assert.IsNotNull(addressResponse.AuthTag);
            Assert.IsNotNull(addressResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAAddressAliasFromRealData_WhenRetrievingAlias_ShouldReturnAAddressAlias()
        {
            var hash = StaticVault.Hash(address);

            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/address")
                .WithParam("hash").WithParam("tags")
                .UsingGet())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {

                    var security = new Security();
                    var iv = security.Aes.GenerateStringIv();
                    var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, address);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new List<AddressResponse> { new AddressResponse
                 {
                     Id = addressId,
                     Address = encryptedData.EncryptedData,
                     AddressAlias = addressAlias,
                     AuthTag = encryptedData.AuthTag,
                     Iv = encryptedData.Iv,
                     UpdatedAt = now,
                     CreatedAt = now
                 }}));
                }));

            var addressResponses = await StaticVault.Address.RetrieveFromRealData(address, tags);


            addressResponses.ForEach(addressResponse =>
            {
                Assert.AreEqual(addressResponse.Id, addressId);
                Assert.AreEqual(addressResponse.Address, address);
                Assert.AreEqual(addressResponse.AddressAlias, addressAlias);
                Assert.AreEqual(addressResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(addressResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(addressResponse.AuthTag);
                Assert.IsNotNull(addressResponse.Iv);
            });
        }

        [TestMethod]
        public async Task GivenRequestToDeleteAAddressAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
        {
            Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/address/{addressId}").UsingDelete())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Ok = true
                 })));

            await StaticVault.Address.Delete(addressId);
        }
    }
}

