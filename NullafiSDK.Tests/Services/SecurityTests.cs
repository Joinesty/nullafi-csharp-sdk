using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nullafi.Tests.Services
{
    [TestClass]
    public class SecurityTests
    {
        [TestMethod]
        public void GivenNeedToUseEncryption_WhenSecurityClassIsInstantiated_ShouldHaveAllCryptoPropsInstantiaded()
        {
            var security = new Security();
            Assert.IsNotNull(security.Aes);
            Assert.IsNotNull(security.Hmac);
            Assert.IsNotNull(security.RSA);
        }
    }
}
