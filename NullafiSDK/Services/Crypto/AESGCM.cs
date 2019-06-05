
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

using Nullafi.Models;

namespace Nullafi.Services.Crypto
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
            var parameters = new AeadParameters(new KeyParameter(masterKey), AUTH_TAG_BIT_LENGTH, iv);
            cipher.Init(true, parameters);

            int outputLength = cipher.GetOutputSize(plainText.Length);
            byte[] output = new byte[outputLength];

            // Produce cipher text
            int outputOffset = cipher.ProcessBytes(plainText, 0, plainText.Length, output, 0);

            // Produce authentication tag
            outputOffset += cipher.DoFinal(output, outputOffset);

            // Split output into cipher text and authentication tag
            int authTagLength = AUTH_TAG_BIT_LENGTH / 8;

            byte[] cipherText = new byte[outputOffset - authTagLength];
            byte[] authTag = new byte[authTagLength];

            Array.Copy(output, 0, cipherText, 0, cipherText.Length);
            Array.Copy(output, outputOffset - authTagLength, authTag, 0, authTag.Length);

            return new AesEncryptedData() {
                EncryptedData = Convert.ToBase64String(cipherText),
                AuthTag = Convert.ToBase64String(authTag),
                Iv = Convert.ToBase64String(iv)
            };
        }

        public string Decrypt(byte[] masterKey, byte[] iv, byte[] authTag, string cipherText)
        {
            byte[] byteCipherText = Convert.FromBase64String(cipherText);
            byte[] bytePlainText = Decrypt(masterKey, iv, authTag, byteCipherText);
            return bytePlainText == null ? null : Encoding.UTF8.GetString(bytePlainText).TrimEnd("\r\n\0".ToCharArray());
        }

        public byte[] Decrypt(byte[] masterKey, byte[] iv, byte[] authKey, byte[] cipherText)
        {
            var input = new byte[cipherText.Length + authKey.Length];

            Array.Copy(cipherText, 0, input, 0, cipherText.Length);
            Array.Copy(authKey, 0, input, cipherText.Length, authKey.Length);


            GcmBlockCipher cipher = new GcmBlockCipher(new AesFastEngine());
            AeadParameters parameters =
                      new AeadParameters(new KeyParameter(masterKey), 128, iv);

            cipher.Init(false, parameters);

            byte[] output = new byte[cipher.GetOutputSize(input.Length)];

            Int32 outputOffset = cipher.ProcessBytes
                           (input, 0, input.Length, output, 0);

            cipher.DoFinal(output, outputOffset);

            return output;
        }
    }
}
