using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Services.Crypto;

namespace Nullafi.Tests.Services.Crypto
{
    [TestClass]
    public class HmacTests
    {
        //Please follow the pattern "GivenX_WhenY_ShouldZ
        [TestMethod]
        public void WhenDoingEncryption_ShouldHashTheValue()
        {
            const string plainString = "stringtohash";
            const string key = "key";
            const string expectedBase64Hash = "emolzboiO6eDawDSI+5eRZI4z4jLgeYZeLDDD4yI9b4=";

            var hmac = new Hmac();
            var hashString = hmac.Hash(plainString, key);

            Assert.AreEqual(hashString, expectedBase64Hash);
        }
    }
}
