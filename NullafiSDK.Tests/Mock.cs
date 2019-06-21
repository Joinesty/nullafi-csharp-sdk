using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
            System.Environment.SetEnvironmentVariable("NULLAFI_API_URL", "http://localhost:5000");
            Server = FluentMockServer.Start(5000);

            Server.Given(Request.Create().WithPath("/authentication/token").UsingPost())
                        .RespondWith(
                            Response.Create()
                            .WithStatusCode(HttpStatusCode.OK)
                            .WithBody(JsonConvert.SerializeObject(new
                            {
                                Token = "some-token",
                                HashKey = HASH_KEY
                            }))
                         );
        }


        [AssemblyCleanup]
        public static void ShutdownServer()
        {
            Server.Stop();
        }
    }
}
