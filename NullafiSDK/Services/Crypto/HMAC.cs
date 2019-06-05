using System;
using System.Text;
using System.Security.Cryptography;

using Nullafi.Models;

namespace Nullafi.Services.Crypto
{

    public class HMAC
    {
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
 