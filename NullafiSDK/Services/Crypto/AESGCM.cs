
using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

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

        public string SimpleDecrypt(string encryptedMessage, byte[] key)
        {
            if (string.IsNullOrEmpty(encryptedMessage))
                throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");

            var cipherText = Convert.FromBase64String(encryptedMessage);
            var plainText = SimpleDecrypt(cipherText, key, nonSecretPayloadLength);
            return plainText == null ? null : Encoding.UTF8.GetString(plainText);
        }

        
        

        public byte[] SimpleDecrypt(byte[] encryptedMessage, byte[] key, int nonSecretPayloadLength = 0)
        {
            //User Error Checks
            if (key == null || key.Length != SECRET_KEY_BIT_LENGTH / 8)
                throw new ArgumentException(String.Format("Key needs to be {0} bit!", SECRET_KEY_BIT_LENGTH), "key");

            if (encryptedMessage == null || encryptedMessage.Length == 0)
                throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");

            using (var cipherStream = new MemoryStream(encryptedMessage))
            using (var cipherReader = new BinaryReader(cipherStream))
            {
                //Grab Payload
                var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);

                //Grab Nonce
                var nonce = cipherReader.ReadBytes(IV_BIT_LENGTH / 8);
             
                var cipher = new GcmBlockCipher(new AesFastEngine());
                var parameters = new AeadParameters(new KeyParameter(key), AUTH_TAG_BIT_LENGTH, nonce, nonSecretPayload);
                cipher.Init(false, parameters);

                //Decrypt Cipher Text
                var cipherText = cipherReader.ReadBytes(encryptedMessage.Length - nonSecretPayloadLength - nonce.Length);
                var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];  

                try
                {
                    var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
                    cipher.DoFinal(plainText, len);

                }
                catch (InvalidCipherTextException)
                {
                    //Return null if it doesn't authenticate
                    return null;
                }

                return plainText;
            }

        }

    }
}
 