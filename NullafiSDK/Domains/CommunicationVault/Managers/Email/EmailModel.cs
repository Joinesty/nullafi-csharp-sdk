using System;
using System.Collections.Generic;
using System.Text;

namespace NullafiSDK.Domains.CommunicationVault.Managers.Email
{
    public class EmailModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string EmailToken { get; set; }
        public string EmailHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public string Tags { get; set; }
    }
}
