
using System;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Nullafi.Models;

namespace Nullafi.Services.Crypto
{

    public class Aesgcm
    {
        private readonly SecureRandom _random = new SecureRandom();

        public readonly int AuthTagBitLength = 128;
        public readonly int SecretKeyBitLength = 256;
        public readonly int IvBitLength = 128;
        
        public byte[] GenerateMasterKey()
        {
            var key = new byte[SecretKeyBitLength / 8];
            _random.NextBytes(key);
            return key;
        } 

        public string GenerateStringMasterKey()
        {
            return Convert.ToBase64String(GenerateMasterKey());
        }

        public byte[] GenerateIv()
        {
            var key = new byte[IvBitLength / 8];
            _random.NextBytes(key);
            return key;
        }

        public string GenerateStringIv()
        {
            return Convert.ToBase64String(GenerateIv());
        }

        public AesEncryptedData Encrypt(byte[] masterKey, byte[] iv, string plainText)
        {
            var bytePlainText = Encoding.UTF8.GetBytes(plainText);
            return Encrypt(masterKey, iv, bytePlainText);
        }

        public AesEncryptedData Encrypt(byte[] masterKey, byte[] iv, byte[] plainText)
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

        public string Decrypt(byte[] masterKey, byte[] iv, byte[] authTag, string cipherText)
        {
            var byteCipherText = Convert.FromBase64String(cipherText);
            var bytePlainText = Decrypt(masterKey, iv, authTag, byteCipherText);
            return bytePlainText == null ? null : Encoding.UTF8.GetString(bytePlainText).TrimEnd("\r\n\0".ToCharArray());
        }

        public byte[] Decrypt(byte[] masterKey, byte[] iv, byte[] authKey, byte[] cipherText)
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
