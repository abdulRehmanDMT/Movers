using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoversApi.ViewModels
{
    public class NotificationMetadata
    {
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
