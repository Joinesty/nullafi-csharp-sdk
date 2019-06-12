namespace Nullafi.Models
{
    public class AesEncryptedData
    {
        public string EncryptedData { get; set; }
        public string AuthTag { get; set; }
        public string Iv { get; set; }
    }
}
