using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Nullafi.Tests
{
    [TestClass]
    public class Mock
    {
        public static FluentMockServer Server;
        public static string HASH_KEY = "some-hash";
        public static string API_KEY = "some-api-key";

        [AssemblyInitialize]
        public static void InitializeServer(TestContext context)
        {
            Server = FluentMockServer.Start(5000);

            Server.Given(Request.Create().WithPath("/authentication/token").UsingPost())
                        .RespondWith(
                            Response.Create()
                            .WithStatusCode(HttpStatusCode.OK)
                            .WithBody(@"{ ""token"": ""some-token"", ""hashKey"": """ + HASH_KEY + @""" }")
                         );
        }


        [AssemblyCleanup]
        public static void ShutdownServer()
        {
            Server.Stop();
        }
    }
}
