using System;
using System.Text;
using System.Security.Cryptography;

namespace Nullafi.Services.Crypto
{
    /// <summary>
    /// Hmac
    /// </summary>
    public class Hmac
    {
        /// <summary>
        /// Generate a hash for the real data
        /// </summary>
        /// <param name="value"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public string Hash(string value, string secret)
        {
            var keyByte = Encoding.UTF8.GetBytes(secret);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                return Convert.ToBase64String(hmacsha256.Hash);
            }
        }
    }
}
 