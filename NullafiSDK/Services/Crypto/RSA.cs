using System;
using System.IO;
using System.Text;
using System.Security;
using Org.BouncyCastle.Utilities.IO.Pem;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;

namespace Nullafi.Services.Crypto
{

    /// <summary>
    /// RSAManager
    /// </summary>
    public class RSAManager
    {
        /// <summary>
        /// Decrypt object to a base64 string using a private key
        /// </summary>
        public Func<string, string> Decrypt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PublicKey { get; set; }
    }

    /// <summary>
    /// RSA
    /// </summary>
    public class RSA
    {
        /// <summary>
        /// Create instance of RSAManager
        /// </summary>
        public RSAManager RSAGenerateManager()
        {
            var keyGen = new RsaKeyPairGenerator();
            keyGen.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            var keyPair = keyGen.GenerateKeyPair();

            Func<string, string> decrypt = (string encryptedValue) =>
            {
                var decryptEngine = new OaepEncoding(new RsaEngine());
                var bytesToDecrypt = Convert.FromBase64String(encryptedValue);
                decryptEngine.Init(false, keyPair.Private as RsaPrivateCrtKeyParameters);
                return Encoding.UTF8.GetString(decryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length));
            };

            var info = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public);

            var stringWriter = new StringWriter();
            var pemWriter = new PemWriter(stringWriter);
            var pemObject = new PemObject("PUBLIC KEY", info.GetEncoded());
            pemWriter.WriteObject(pemObject);

            return new RSAManager
            {
                Decrypt = decrypt,
                PublicKey = stringWriter.ToString(),
            };
        }
    }
}
