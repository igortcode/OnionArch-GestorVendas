using System;

namespace Gestao.Application.Settings
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Client { get; set; }
        public string Key { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string ContactUsRecipient { get; set; }
        public Uri UriPortal { get; set; }
    }
}
