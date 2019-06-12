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

    public class RSAManager
    {
        public Func<string, string> Decrypt { get; set; }
        public string PublicKey { get; set; }
    }

    public class RSA
    {
        public RSAManager RSAGenerateManager()
        {
            var keyGen = new RsaKeyPairGenerator();
            keyGen.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            var keyPair = keyGen.GenerateKeyPair();

            var secureString = new SecureString();


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
