using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                .WithBody("{ \"token\": \"some-token\", \"hashKey\": \"some-hash-key\" }")
            );

            var sdk = new NullafiSDK("API_KEY");
            var client = await sdk.CreateClient();

            Assert.IsNotNull(client);
        }
    }
}
