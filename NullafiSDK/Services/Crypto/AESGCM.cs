
using System;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Nullafi.Models;

namespace Nullafi.Services.Crypto
{

    /// <summary>
    /// Aesgcm
    /// </summary>
    public class Aesgcm
    {
        private readonly SecureRandom _random = new SecureRandom();

        public readonly int AuthTagBitLength = 128;
        public readonly int SecretKeyBitLength = 256;
        public readonly int IvBitLength = 128;
        
        private byte[] GenerateMasterKey()
        {
            var key = new byte[SecretKeyBitLength / 8];
            _random.NextBytes(key);
            return key;
        }

        /// <summary>
        /// Generate masterkey to be used on AES encrypt/decrypt
        /// </summary>
        /// <returns></returns>
        public string GenerateStringMasterKey()
        {
            return Convert.ToBase64String(GenerateMasterKey());
        }

        private byte[] GenerateIv()
        {
            var key = new byte[IvBitLength / 8];
            _random.NextBytes(key);
            return key;
        }

        /// <summary>
        /// Generate initialization vector to be used on AES encrypt/decrypt
        /// </summary>
        /// <returns></returns>
        public string GenerateStringIv()
        {
            return Convert.ToBase64String(GenerateIv());
        }

        /// <summary>
        /// Encrypt the data using AES GCM 256bit
        /// </summary>
        /// <param name="masterKey"></param>
        /// <param name="iv"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public AesEncryptedData Encrypt(string masterKey, string iv, string plainText)
        {
            var bytePlainText = Encoding.UTF8.GetBytes(plainText);
            return Encrypt(Convert.FromBase64String(masterKey), Convert.FromBase64String(iv), bytePlainText);
        }

        private AesEncryptedData Encrypt(byte[] masterKey, byte[] iv, byte[] plainText)
        {
            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(masterKey), AuthTagBitLength, iv);
            cipher.Init(true, parameters);

            var outputLength = cipher.GetOutputSize(plainText.Length);
            var output = new byte[outputLength];

            // Produce cipher text
            var outputOffset = cipher.ProcessBytes(plainText, 0, plainText.Length, output, 0);

            // Produce authentication tag
            outputOffset += cipher.DoFinal(output, outputOffset);

            // Split output into cipher text and authentication tag
            var authTagLength = AuthTagBitLength / 8;

            var cipherText = new byte[outputOffset - authTagLength];
            var authTag = new byte[authTagLength];

            Array.Copy(output, 0, cipherText, 0, cipherText.Length);
            Array.Copy(output, outputOffset - authTagLength, authTag, 0, authTag.Length);

            return new AesEncryptedData() {
                EncryptedData = Convert.ToBase64String(cipherText),
                AuthTag = Convert.ToBase64String(authTag),
                Iv = Convert.ToBase64String(iv)
            };
        }

        /// <summary>
        /// Decrypt the data using AES GCM 256bit
        /// </summary>
        /// <param name="masterKey"></param>
        /// <param name="iv"></param>
        /// <param name="authTag"></param>
        /// <param name="cipherText"></param>
        /// <param name="returnBase64"></param>
        /// <returns></returns>
        public string Decrypt(string masterKey, string iv, string authTag, string cipherText)
        {
            var byteCipherText = Convert.FromBase64String(cipherText);
            var bytePlainText = Decrypt(Convert.FromBase64String(masterKey), Convert.FromBase64String(iv), Convert.FromBase64String(authTag), byteCipherText);

            if (bytePlainText == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(bytePlainText).TrimEnd("\r\n\0".ToCharArray());
        }

        private byte[] Decrypt(byte[] masterKey, byte[] iv, byte[] authKey, byte[] cipherText)
        {
            var input = new byte[cipherText.Length + authKey.Length];

            Array.Copy(cipherText, 0, input, 0, cipherText.Length);
            Array.Copy(authKey, 0, input, cipherText.Length, authKey.Length);


            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters =
                      new AeadParameters(new KeyParameter(masterKey), 128, iv);

            cipher.Init(false, parameters);

            var output = new byte[cipher.GetOutputSize(input.Length)];

            var outputOffset = cipher.ProcessBytes
                           (input, 0, input.Length, output, 0);

            cipher.DoFinal(output, outputOffset);

            return output;
        }
    }
}
