using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.CommunicationVault.Managers.Email
{
    public class EmailRequest
    {
        public string Email { get; set; }
        public string EmailHash { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
    }
}
