using System;
using System.IO;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using Org.BouncyCastle.Utilities.IO.Pem;

using NullafiSDK.Models;

namespace NullafiSDK.Services.Crypto
{

    public class RSAManager
    {
        public Func<string, string> Encrypt { get; set; }
        public Func<string, string> Decrypt { get; set; }
        public string PublicKey { get; set; }
    }

    public class RSA
    {
        public RSAManager RSAGenerateManager(string passphrase)
        {
            CspParameters param = new CspParameters
            {
                Flags = CspProviderFlags.CreateEphemeralKey
            };

            SecureString secureString = new SecureString();

            foreach (char c in passphrase.ToCharArray())
            {
                secureString.AppendChar(c);
            }

            param.KeyPassword = secureString;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param);

            Func<string, string> encrypt = (string value) =>
            {
                return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(value), RSAEncryptionPadding.OaepSHA1));
            };

            Func<string, string> decrypt = (string encryptedValue) =>
            {
                return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(encryptedValue), RSAEncryptionPadding.OaepSHA1));
            };

            StringWriter stringWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(stringWriter);
            PemObject pemObject = new PemObject("PUBLIC KEY", rsa.ExportCspBlob(false));
            pemWriter.WriteObject(pemObject);

            return new RSAManager
            {
                Encrypt = encrypt,
                Decrypt = decrypt,
                PublicKey = stringWriter.ToString(),
            };
        }
        
    }
}
 