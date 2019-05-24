using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Email
{
    public class EmailService
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }

        public EmailService(string smtpHost, int smtpPort, string smtpUser, string smtpPass)
        {
            this.SmtpHost = smtpHost;
            this.SmtpPort = smtpPort;
            this.SmtpUser = smtpUser;
            this.SmtpPass = smtpPass;
        }

        public void SendExternalEmail(MailMessage msg)
        {
            SmtpClient client = new SmtpClient()
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPass),
                Port = SmtpPort,
                Host = SmtpHost,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
            {
                client.Send(msg);
            }
        }

        public MailMessage BuildMailMessage(string to, string subject, string body)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.From = new MailAddress("x@x.x"); // OBS
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            return msg;
        }
    }
}
