using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Nullafi.Tests
{
    [TestClass]
    public class NullafiSDKTests
    {
        [TestMethod]
        public async void GivenRequestToUseTheSDK_WhenCreatingClient_ShouldReturnAuthenticatedClientInstance()
        {
            FluentMockServer.Start("https://dashboard-api.nullafi.com")
            .Given(Request.Create().WithPath("/authentication/token").UsingPost())
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
