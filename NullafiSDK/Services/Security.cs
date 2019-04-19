using NullafiSDK.Models;
using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace NullafiSDK
{

    public class RSAEphemeral
    {
        public Func<string, string> Encrypt { get; set; }
        public Func<string, string> Decrypt { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }

    public class Security
    {
        public RSAEphemeral RSAGenerateEphemeral(string passphrase)
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

        public string AesGenerateMasterKey()
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }

        public string AesGenerateInitializationVector()
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }

        public AesEncryptedData AesEncrypt(string Key, string IV, string plainText)
        {
            return new AesEncryptedData
            {
                EncryptedData = plainText,
                Iv = "",
                AuthTag = "",
            };
        }

        public string AesDecrypt(string Key, string IV, string authTag, string base64encryptedString)
        {
            // Check arguments. 
            if (base64encryptedString == null || base64encryptedString.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }

            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException("Key");
            }

            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("IV");
            }

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
