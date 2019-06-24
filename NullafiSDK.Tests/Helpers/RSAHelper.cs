using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nullafi.Tests.Helpers
{
    public class RSAHelper
    {
        public static string EncryptWithPubKey(string plainText, string pubKey)
        {
            var encryptEngine = new OaepEncoding(new RsaEngine());
            var bytesToDecrypt = Encoding.UTF8.GetBytes(plainText);

            var strReader = new StringReader(pubKey);
            PemReader reader = new PemReader(strReader);
            var keypair = (RsaKeyParameters)reader.ReadObject();
            encryptEngine.Init(true, keypair);
            return Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length));
        }
    }
}
