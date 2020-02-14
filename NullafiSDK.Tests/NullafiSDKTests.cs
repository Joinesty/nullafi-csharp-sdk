using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Nullafi.Tests
{
    [TestClass]
    public class NullafiSDKTests
    {
        [TestMethod]
        public async Task GivenRequestToUseTheSDK_WhenCreatingClient_ShouldReturnAuthenticatedClientInstance()
        {
            Mock.Server.Given(Request.Create().WithPath("/authentication/token").UsingPost())
            .RespondWith(
                Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(JsonConvert.SerializeObject(new
                {
                    Token = "some-token",
                    HashKey = "some-hash-key",
                }))
            );

            var sdk = new NullafiSDK("dlCoYgYqvTQPHhIt9VB3Yo+BI5QhJ3hgZi5t/jkxThE=");
            var client = await sdk.CreateClient();

            Assert.IsNotNull(client);
        }
    }
}
