using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nullafi.Services.Crypto;
using System;

namespace Nullafi.Tests.Services.Crypto
{
    [TestClass]
    public class AESGCMTests
    {
        [TestMethod]
        public void GivenAVaultCreationRequest_WhenCreatingAVault_ThenGenerateA32bytesMasterKey()
        {
            var aesGCM = new Aesgcm();
            var masterKey = aesGCM.GenerateStringMasterKey();

            Assert.AreEqual(Convert.FromBase64String(masterKey).Length, 32);
        }

        [TestMethod]
        public void GivenATokenCreationRequest_WhenCreatingAToken_ThenGenerateA16BitIv()
        {
            var aesGCM = new Aesgcm();
            var iv = aesGCM.GenerateStringIv();

            Assert.AreEqual(Convert.FromBase64String(iv).Length, 16);
        }

        [TestMethod]
        public void GivenATokenCreationRequest_WhenCreatingAToken_ThenEncryptTokenValue()
        {
            var aesGCM = new Aesgcm();
            var masterKey = "Hz0kSjlF4D6wbtlPzMkPUR3eYcbtNA3tHfGjwhcUVAs=";
            var iv = "tGpT5qcDZi3pFrUb0F70mA==";
            var authTag = "GO1vDnMXnLLGSL3ix47/2Q==";
            var encryptedData = "lmc0d1dCd1El";

            var data = "plaintext";

            var encryptedObj = aesGCM.Encrypt(masterKey, iv, data);

            Assert.AreEqual(encryptedObj.AuthTag, authTag);
            Assert.AreEqual(encryptedObj.Iv, iv);
            Assert.AreEqual(encryptedObj.EncryptedData, encryptedData);
        }

        [TestMethod]
        public void GivenATokenRetrievalRequest_WhenRetrievingAToken_ThenDecryptTheTokenValue()
        {
            var aesGCM = new Aesgcm();
            var masterKey = "Hz0kSjlF4D6wbtlPzMkPUR3eYcbtNA3tHfGjwhcUVAs=";
            var iv = "tGpT5qcDZi3pFrUb0F70mA==";
            var authTag = "GO1vDnMXnLLGSL3ix47/2Q==";
            var encryptedData = "lmc0d1dCd1El";

            var data = "plaintext";

            var decryptedData = aesGCM.Decrypt(masterKey, iv, authTag, encryptedData);

            Assert.AreEqual(data, decryptedData);
        }
    }
}
