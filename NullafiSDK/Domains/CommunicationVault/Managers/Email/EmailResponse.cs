using System;
using System.Collections.Generic;
using System.Text;

namespace Nullafi.Domains.CommunicationVault.Managers.Email
{
    public class EmailResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string EmailAlias { get; set; }
        public string Iv { get; set; }
        public string AuthTag { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
