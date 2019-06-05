
using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;

using NullafiSDK.Models;

namespace NullafiSDK.Services.Crypto
{

    public class AESGCM
    {
        private readonly SecureRandom Random = new SecureRandom();

        public readonly int AUTH_TAG_BIT_LENGTH = 128;
        public readonly int SECRET_KEY_BIT_LENGTH = 256;
        public readonly int IV_BIT_LENGTH = 128;
        
        public byte[] GenerateMasterKey()
        {
            var key = new byte[SECRET_KEY_BIT_LENGTH / 8];
            Random.NextBytes(key);
            return key;
        } 

        public string GenerateStringMasterKey()
        {
            return Convert.ToBase64String(this.GenerateMasterKey());
        }

        public byte[] GenerateIv()
        {
            var key = new byte[IV_BIT_LENGTH / 8];
            Random.NextBytes(key);
            return key;
        }

        public string GenerateStringIv()
        {
            return Convert.ToBase64String(this.GenerateIv());
        }

        public AesEncryptedData Encrypt(byte[] masterKey, byte[] iv, string plainText)
        {
            byte[] bytePlainText = Encoding.UTF8.GetBytes(plainText);
            return Encrypt(masterKey, iv, bytePlainText);
        }

        public AesEncryptedData Encrypt(byte[] masterKey, byte[] iv, byte[] plainText)
        {
            var cipher = new GcmBlockCipher(new AesFastEngine());
            var parameters = new AeadParameters(new KeyParameter(masterKey), AUTH_TAG_BIT_LENGTH, iv, null);
            cipher.Init(true, parameters);

            //Generate Cipher Text With Auth Tag
            var cipherText = new byte[cipher.GetOutputSize(plainText.Length)];
            var len = cipher.ProcessBytes(plainText, 0, plainText.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            return new AesEncryptedData() {
                EncryptedData = Convert.ToBase64String(cipherText),
                AuthTag = Convert.ToBase64String(cipher.GetMac()),
                Iv = Convert.ToBase64String(iv)
            };
        }

        public string Decrypt(byte[] masterKey, byte[] iv, byte[] authTag, string cipherText)
        {
            byte[] byteCipherText = Encoding.UTF8.GetBytes(cipherText);
            byte[] bytePlainText = Decrypt(masterKey, iv, authTag, byteCipherText);
            return bytePlainText == null ? null : Encoding.UTF8.GetString(bytePlainText);
        }

        public byte[] Decrypt(byte[] masterKey, byte[] iv, byte[] authKey, byte[] cipherText)
        {
            using (var hmac = new HMACSHA256(authKey))
            {
                var sentTag = new byte[hmac.HashSize / 8];
                var calcTag = hmac.ComputeHash(cipherText, 0, cipherText.Length - sentTag.Length);
                var ivLength = (IV_BIT_LENGTH / 8);

                if (cipherText.Length < sentTag.Length + ivLength)
                    return null;

                Array.Copy(cipherText, cipherText.Length - sentTag.Length, sentTag, 0, sentTag.Length);

                var compare = 0;
                for (var i = 0; i < sentTag.Length; i++)
                    compare |= sentTag[i] ^ calcTag[i]; 

                if (compare != 0)
                    return null;

                using (var aes = new AesManaged
                {
                    KeySize = SECRET_KEY_BIT_LENGTH,
                    BlockSize = IV_BIT_LENGTH,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                })
                {
                    Array.Copy(cipherText, 0, iv, 0, iv.Length);

                    using (var decrypter = aes.CreateDecryptor(masterKey, iv))
                    using (var plainTextStream = new MemoryStream())
                    {
                        using (var decrypterStream = new CryptoStream(plainTextStream, decrypter, CryptoStreamMode.Write))
                        using (var binaryWriter = new BinaryWriter(decrypterStream))
                        {
                            //Decrypt Cipher Text from Message
                            binaryWriter.Write(
                                cipherText,
                                iv.Length,
                                cipherText.Length - iv.Length - sentTag.Length
                            );
                        }
                        //Return Plain Text
                        return plainTextStream.ToArray();
                    }
                }
            }
        }
    }
}
 