using Nullafi.Models;
using System;

using System.Text;

using Nullafi.Services.Crypto;

namespace Nullafi
{
    
    public class Security
    {
        
        public AESGCM aes {get; }
        public RSA rsa {get; }
        public HMAC hmac {get; }

        public Security()
        {
            this.aes = new AESGCM();
            this.rsa = new RSA();
            this.hmac = new HMAC();
        }

    }
}
