using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Nullafi.Tests
{
    [TestClass]
    public class Mock
    {
        [AssemblyInitialize]
        public static void InitializeServer(TestContext context)
        {
            System.Environment.SetEnvironmentVariable("NULLAFI_API_URL", "**NUllAFI_API**");
            System.Environment.SetEnvironmentVariable("API_KEY", "**YOUR_API_KEY**");
        }

    }
}
