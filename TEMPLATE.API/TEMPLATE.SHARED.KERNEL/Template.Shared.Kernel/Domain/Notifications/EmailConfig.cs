using System.Collections.Generic;

namespace Template.Shared.Kernel.Domain.Notifications
{
    public class EmailConfig
    {
        public bool Active { get; set; }

        public string Host { get; set; }

        public int? Port { get; set; }

        public string SenderEmail { get; set; }

        public string SenderPassword { get; set; }

        public bool? EnableSSL { get; set; }

        public List<string> SendOnlyTo { get; set; }
    }
}
