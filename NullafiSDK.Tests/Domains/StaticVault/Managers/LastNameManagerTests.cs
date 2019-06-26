    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Nullafi.Domains.StaticVault.Managers.LastName;
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
        public class LastNameManagerTests
        {
            static Nullafi.Domains.StaticVault.StaticVault StaticVault;

            string lastnameId = "42719977-66da-4b48-89e7-ea53e0b0db32";
            string lastnameAlias = "lastname example";
            string lastname = "real lastname example";
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
            public async Task GivenRequestToCreateALastNameAliasWithTags_WhenCreatingAlias_ShouldReturnALastNameAlias()
            {
                Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/lastname").UsingPost())
                    .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                    {
                        var request = JObject.Parse(requestMessage.Body);

                        return Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                     .WithBody(JsonConvert.SerializeObject(new LastNameResponse
                     {
                         Id = lastnameId,
                         LastName = request.Value<string>("lastname"),
                         LastNameAlias = lastnameAlias,
                         AuthTag = request.Value<string>("authTag"),
                         Iv = request.Value<string>("iv"),
                         Tags = tags,
                         UpdatedAt = now,
                         CreatedAt = now
                     }));
                    }));


                var lastnameResponse = await StaticVault.LastName.Create(lastname, tags);

                Assert.AreEqual(lastnameResponse.Id, lastnameId);
                Assert.AreEqual(lastnameResponse.LastName, lastname);
                Assert.AreEqual(lastnameResponse.LastNameAlias, lastnameAlias);
                CollectionAssert.AreEqual(lastnameResponse.Tags, tags);
                Assert.AreEqual(lastnameResponse.UpdatedAt, now);
                Assert.AreEqual(lastnameResponse.CreatedAt, now);
                Assert.IsNotNull(lastnameResponse.AuthTag);
                Assert.IsNotNull(lastnameResponse.Iv);
            }


            [TestMethod]
            public async Task GivenRequestToCreateALastNameAlias_WhenCreatingAlias_ShouldReturnALastNameAlias()
            {
                Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/lastname").UsingPost())
                    .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                    {
                        var request = JObject.Parse(requestMessage.Body);

                        return Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                     .WithBody(JsonConvert.SerializeObject(new LastNameResponse
                     {
                         Id = lastnameId,
                         LastName = request.Value<string>("lastname"),
                         LastNameAlias = lastnameAlias,
                         AuthTag = request.Value<string>("authTag"),
                         Iv = request.Value<string>("iv"),
                         UpdatedAt = now,
                         CreatedAt = now
                     }));
                    }));


                var lastnameResponse = await StaticVault.LastName.Create(lastname);

                Assert.AreEqual(lastnameResponse.Id, lastnameId);
                Assert.AreEqual(lastnameResponse.LastName, lastname);
                Assert.AreEqual(lastnameResponse.LastNameAlias, lastnameAlias);
                Assert.AreEqual(lastnameResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(lastnameResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(lastnameResponse.AuthTag);
                Assert.IsNotNull(lastnameResponse.Iv);
            }

            [TestMethod]
            public async Task GivenRequestToRetrieveALastNameAlias_WhenRetrievingAlias_ShouldReturnALastNameAlias()
            {
                Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/lastname/{lastnameId}").UsingGet())
                    .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                    {
                        var security = new Security();
                        var iv = security.Aes.GenerateStringIv();
                        var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, lastname);

                        return Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                     .WithBody(JsonConvert.SerializeObject(new LastNameResponse
                     {
                         Id = lastnameId,
                         LastName = encryptedData.EncryptedData,
                         LastNameAlias = lastnameAlias,
                         AuthTag = encryptedData.AuthTag,
                         Iv = encryptedData.Iv,
                         UpdatedAt = now,
                         CreatedAt = now
                     }));
                    }));


                var lastnameResponse = await StaticVault.LastName.Retrieve(lastnameId);

                Assert.AreEqual(lastnameResponse.Id, lastnameId);
                Assert.AreEqual(lastnameResponse.LastName, lastname);
                Assert.AreEqual(lastnameResponse.LastNameAlias, lastnameAlias);
                Assert.AreEqual(lastnameResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.AreEqual(lastnameResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                Assert.IsNotNull(lastnameResponse.AuthTag);
                Assert.IsNotNull(lastnameResponse.Iv);
            }

            [TestMethod]
            public async Task GivenRequestToRetrieveALastNameAliasFromRealData_WhenRetrievingAlias_ShouldReturnALastNameAlias()
            {
                var hash = StaticVault.Hash(lastname);

                Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/lastname")
                    .WithParam("hash")
                    .UsingGet())
                    .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                    {

                        var security = new Security();
                        var iv = security.Aes.GenerateStringIv();
                        var encryptedData = security.Aes.Encrypt(StaticVault.MasterKey, iv, lastname);

                        return Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                     .WithBody(JsonConvert.SerializeObject(new List<LastNameResponse> { new LastNameResponse
                     {
                         Id = lastnameId,
                         LastName = encryptedData.EncryptedData,
                         LastNameAlias = lastnameAlias,
                         AuthTag = encryptedData.AuthTag,
                         Iv = encryptedData.Iv,
                         UpdatedAt = now,
                         CreatedAt = now
                     }}));
                    }));

                var lastnameResponses = await StaticVault.LastName.RetrieveFromRealData(lastname);


                lastnameResponses.ForEach(lastnameResponse =>
                {
                    Assert.AreEqual(lastnameResponse.Id, lastnameId);
                    Assert.AreEqual(lastnameResponse.LastName, lastname);
                    Assert.AreEqual(lastnameResponse.LastNameAlias, lastnameAlias);
                    Assert.AreEqual(lastnameResponse.UpdatedAt.ToLongDateString(), now.ToLongDateString());
                    Assert.AreEqual(lastnameResponse.CreatedAt.ToLongDateString(), now.ToLongDateString());
                    Assert.IsNotNull(lastnameResponse.AuthTag);
                    Assert.IsNotNull(lastnameResponse.Iv);
                });
            }

            [TestMethod]
            public async Task GivenRequestToDeleteALastNameAlias_WhenDeletingAlias_ShouldReturnAOkResponse()
            {
                Mock.Server.Given(Request.Create().WithPath($"/vault/static/{StaticVault.VaultId}/lastname/{lastnameId}").UsingDelete())
                    .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                     .WithBody(JsonConvert.SerializeObject(new
                     {
                         Ok = true
                     })));

                await StaticVault.LastName.Delete(lastnameId);
            }
        }
    }

