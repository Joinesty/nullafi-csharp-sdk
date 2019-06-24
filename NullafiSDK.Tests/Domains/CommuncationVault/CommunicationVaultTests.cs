using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nullafi.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Nullafi.Tests.Domains.CommuncationVault
{
    class CommunicationVaultTests
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
    }
}
