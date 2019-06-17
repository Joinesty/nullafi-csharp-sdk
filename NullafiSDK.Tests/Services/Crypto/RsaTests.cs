using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Services.Crypto;
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
            const string key = "key";
            const string expectedBase64Hash = "emolzboiO6eDawDSI+5eRZI4z4jLgeYZeLDDD4yI9b4=";

            Func<string, string, string> encryptWithPubKey = (string plainText, string pubKey) =>
            {
                var encryptEngine = new OaepEncoding(new RsaEngine());
                var bytesToDecrypt = Encoding.UTF8.GetBytes(plainText);

                var strReader = new StringReader(pubKey);
                PemReader reader = new PemReader(strReader);
                var keypair = (RsaKeyParameters)reader.ReadObject();
                encryptEngine.Init(true, keypair);
                return Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length));
            };

            var rsa = new RSA();

            var manager = rsa.RSAGenerateManager();
            var encryptedData = encryptWithPubKey(data, manager.PublicKey);
            var decryptedData = manager.Decrypt(encryptedData);

            Assert.AreEqual(data, decryptedData);
        }
    }
}
