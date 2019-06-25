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
     }
}
