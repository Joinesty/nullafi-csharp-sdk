using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Services.Crypto;
using Nullafi.Tests.Helpers;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Text;

namespace Nullafi.Tests.Services.Crypto
{
    [TestClass]
    public class RsaTests
    {
        [TestMethod]
        public void GivenACommunicationVaultCreationRequest_WhenSendingTheMasterkey_ThenEncryptItUsingRSA()
        {
            const string data = "plainText";
           
            var rsa = new RSA();

            var manager = rsa.RSAGenerateManager();
            var encryptedData = RSAHelper.EncryptWithPubKey(data, manager.PublicKey);
            var decryptedData = manager.Decrypt(encryptedData);

            Assert.AreEqual(data, decryptedData);
        }
    }
}
