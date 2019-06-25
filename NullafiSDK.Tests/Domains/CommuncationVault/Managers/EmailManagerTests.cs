using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Domains.CommunicationVault.Managers.Email;
using Nullafi.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Nullafi.Tests.Domains.CommuncationVault.Managers
{
    [TestClass]
    public class EmailManagerTests
    {
        static Nullafi.Domains.CommunicationVault.CommunicationVault CommunicationVault;

        [ClassInitialize]
        public static async Task InstantiateVault(TestContext context)
        {
            var vaultId = "some-vault-id";
            var vaultName = "some-vault-name";
            var tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };

            var security = new Security();
            var vaultMasterkey = security.Aes.GenerateStringMasterKey();

            Mock.Server.Given(Request.Create().WithPath("/vault/communication").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var secLevelMasterkey = security.Aes.GenerateStringMasterKey();
                    var secLevelIv = security.Aes.GenerateStringIv();
                    var encryptedMasterKey = security.Aes.Encrypt(secLevelMasterkey, secLevelIv, vaultMasterkey);

                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new
                 {
                     Id = vaultId,
                     Name = vaultName,
                     MasterKey = encryptedMasterKey.EncryptedData,
                     encryptedMasterKey.AuthTag,
                     encryptedMasterKey.Iv,
                     SessionKey = RSAHelper.EncryptWithPubKey(secLevelMasterkey, request.Value<string>("publicKey")),
                     Tags = tags
                 }));
                }));

            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            CommunicationVault = await client.CreateCommunicationVault(vaultName, tags);
        }

        [TestMethod]
        public async Task GivenRequestToCreateAEmailAliasWithTags_WhenCreatingAlias_ShouldReturnAEmailAlias()
        {
            var emailId = "42719977-66da-4b48-89e7-ea53e0b0db32";
            var emailAlias = "alias@email.com";
            var email = "example@email.com";
            var tags = new List<string> { "some-vault-tag-1", "some-vault-tag-2" };
            var now = DateTime.Now;

            Mock.Server.Given(Request.Create().WithPath($"/vault/communication/{CommunicationVault.VaultId}/email").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new EmailResponse
                 {
                     Id = emailId,
                     Email = request.Value<string>("email"),
                     EmailAlias = emailAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     Tags = tags,
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var emailResponse = await CommunicationVault.Email.Create(email, tags);

            Assert.AreEqual(emailResponse.Id, emailId);
            Assert.AreEqual(emailResponse.Email, email);
            Assert.AreEqual(emailResponse.EmailAlias, emailAlias);
            CollectionAssert.AreEqual(emailResponse.Tags, tags);
            Assert.AreEqual(emailResponse.UpdatedAt, now);
            Assert.AreEqual(emailResponse.CreatedAt, now);
            Assert.IsNotNull(emailResponse.AuthTag);
            Assert.IsNotNull(emailResponse.Iv);
        }


        [TestMethod]
        public async Task GivenRequestToCreateAEmailAlias_WhenCreatingAlias_ShouldReturnAEmailAlias()
        {
            var emailId = "42719977-66da-4b48-89e7-ea53e0b0db32";
            var emailAlias = "alias@email.com";
            var email = "example@email.com";
            var now = DateTime.Now;

            Mock.Server.Given(Request.Create().WithPath($"/vault/communication/{CommunicationVault.VaultId}/email").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new EmailResponse
                 {
                     Id = emailId,
                     Email = request.Value<string>("email"),
                     EmailAlias = emailAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var emailResponse = await CommunicationVault.Email.Create(email);

            Assert.AreEqual(emailResponse.Id, emailId);
            Assert.AreEqual(emailResponse.Email, email);
            Assert.AreEqual(emailResponse.EmailAlias, emailAlias);
            Assert.AreEqual(emailResponse.UpdatedAt, now);
            Assert.AreEqual(emailResponse.CreatedAt, now);
            Assert.IsNotNull(emailResponse.AuthTag);
            Assert.IsNotNull(emailResponse.Iv);
        }

        [TestMethod]
        public async Task GivenRequestToRetrieveAEmailAlias_WhenRetrievingAlias_ShouldReturnAEmailAlias()
        {
            var emailId = "42719977-66da-4b48-89e7-ea53e0b0db32";
            var emailAlias = "alias@email.com";
            var email = "example@email.com";
            var now = DateTime.Now;

            Mock.Server.Given(Request.Create().WithPath($"/vault/communication/{CommunicationVault.VaultId}/email").UsingPost())
                .RespondWith(new ResponseProviderInterceptor((RequestMessage requestMessage) =>
                {
                    var request = JObject.Parse(requestMessage.Body);

                    return Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                 .WithBody(JsonConvert.SerializeObject(new EmailResponse
                 {
                     Id = emailId,
                     Email = request.Value<string>("email"),
                     EmailAlias = emailAlias,
                     AuthTag = request.Value<string>("authTag"),
                     Iv = request.Value<string>("iv"),
                     UpdatedAt = now,
                     CreatedAt = now
                 }));
                }));


            var emailResponse = await CommunicationVault.Email.Create(email);

            Assert.AreEqual(emailResponse.Id, emailId);
            Assert.AreEqual(emailResponse.Email, email);
            Assert.AreEqual(emailResponse.EmailAlias, emailAlias);
            Assert.AreEqual(emailResponse.UpdatedAt, now);
            Assert.AreEqual(emailResponse.CreatedAt, now);
            Assert.IsNotNull(emailResponse.AuthTag);
            Assert.IsNotNull(emailResponse.Iv);
        }
    }
}
