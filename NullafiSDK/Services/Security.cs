using NullafiSDK.Models;
using System;

using System.Text;

using NullafiSDK.Services.Crypto;

namespace NullafiSDK
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
