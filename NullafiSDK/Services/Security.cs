using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace NullafiSDK
{

    public class RSAEphemeral {
        public Func<string, string> Encrypt {get;set;}
        public Func<string, string> Decrypt {get;set;}
        public string PublicKey {get;set;}
        public string PrivateKey {get;set;}
    }

    public class Security
    {
        public RSAEphemeral RSAGenerateEphemeral(string passphrase)
        {
            var param = new CspParameters();
            param.Flags = CspProviderFlags.CreateEphemeralKey;

            var secureString = new SecureString();

            foreach (var c in passphrase.ToCharArray())
                secureString.AppendChar(c);

            param.KeyPassword = secureString;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param);

            Func<string, string> encrypt = (string value) => {
               return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(value), false));
            };

            Func<string, string> decrypt = (string encryptedValue) =>
            {
              return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(encryptedValue), false));
            };

            return new RSAEphemeral
            {
                Encrypt = encrypt,
                Decrypt = decrypt,
                PublicKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(rsa.ToXmlString(false))),
                PrivateKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(rsa.ToXmlString(true)))
            };
        }

        public string AesGenerateMasterKey() {
            var aes = new RijndaelManaged();
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }

        public string AesGenerateMasterKey()
        {
            var aes = new RijndaelManaged();
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }

        public string AesEncrypt(string plainText, string Key, string IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Convert.FromBase64String(Key);
                rijAlg.IV = Convert.FromBase64String(IV);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public string AesDecrypt(string base64encryptedString, string Key, string IV)
        {
            // Check arguments. 
            if (base64encryptedString == null || base64encryptedString.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Convert.FromBase64String(Key);
                rijAlg.IV = Convert.FromBase64String(IV);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(base64encryptedString)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
    }
}
