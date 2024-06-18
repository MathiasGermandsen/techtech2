using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace techtech2.MailService
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class MailService
    {
        private readonly SmtpClient smtpClient;
        private readonly string fromAddress;

        public MailService(string smtpHost, int smtpPort, string fromAddressParameter, string smtpUsername, string smtpPassword)
        {
            smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };
            fromAddress = fromAddressParameter;
        }

        public void SendEmail(string toAddress, string subject, string body)
        {
            var mailMessage = new MailMessage(fromAddress, toAddress, subject, body);
            smtpClient.Send(mailMessage);
        }
    }
}