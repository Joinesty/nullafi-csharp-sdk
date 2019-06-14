using Nullafi.Services.Crypto;

namespace Nullafi
{

    /// <summary>
    /// Security
    /// </summary>
    public class Security
    {

        /// <summary>
        /// Aesgcm Class
        /// </summary>
        public Aesgcm Aes {get; }
        /// <summary>
        /// RSA Class
        /// </summary>
        public RSA RSA { get; }
        /// <summary>
        /// Hmac Class
        /// </summary>
        public Hmac Hmac {get; }

        /// <summary>
        /// Create an instance of Security
        /// </summary>
        /// <returns></returns>
        public Security()
        {
            Aes = new Aesgcm();
            RSA = new RSA();
            Hmac = new Hmac();
        }

    }
}
