using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Nullafi.Tests.Domains.StaticVault
{
    [TestClass]
    public class StaticVaultTests
    {
        static Nullafi.Domains.StaticVault.StaticVault StaticVault;

        [ClassInitialize]
        public static async Task InstantiateVault(TestContext context)
        {
            Mock.Server.Given(Request.Create().WithPath("/vault/static").UsingPost())
            .RespondWith(
              Response.Create()
              .WithStatusCode(HttpStatusCode.OK)
              .WithBody(JsonConvert.SerializeObject(new
              {
                  Id = "some-vault-id",
                  Name = "some-vault-name"
              }))
              );

            var client = new Client();
            await client.Authenticate(Mock.API_KEY);
            StaticVault = await client.CreateStaticVault("some-vault-name");
        }

        [TestMethod]
        public void GivenRequestToSaveUnencryptedData_WhenAddingTokenToStaticVault_ShouldEncryptValue()
        {
            //var data = "some data";
            //var encryptedData = staticVault.Encrypt(data);

            //Assert.AreNotEqual(data, encryptedData.EncryptedData);
            //Assert.IsNotNull(encryptedData.EncryptedData);
            //Assert.IsNotNull(encryptedData.Iv);
            //Assert.IsNotNull(encryptedData.AuthTag);
        }

        [TestMethod]
        public void GivenRequestToRetrieveAEncryptedData_WhenRetrievingTokenFromStaticVault_ShouldDecryptValue()
        {
            //var data = "some data";
            //var encryptedData = staticVault.Encrypt(data);
            //var decryptedData = staticVault.Decrypt(encryptedData.Iv, encryptedData.AuthTag, encryptedData.EncryptedData);

            //Assert.AreEqual(data, decryptedData);
        }
    }
}
