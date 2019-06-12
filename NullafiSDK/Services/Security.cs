using Nullafi.Services.Crypto;

namespace Nullafi
{
    
    public class Security
    {
        
        public Aesgcm Aes {get; }
        public RSA RSA {get; }
        public Hmac Hmac {get; }

        public Security()
        {
            Aes = new Aesgcm();
            RSA = new RSA();
            Hmac = new Hmac();
        }

    }
}
