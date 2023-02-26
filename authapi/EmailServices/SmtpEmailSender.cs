using System.Net;
using System.Net.Mail;

namespace EmailServices
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host, _userName, _password;
        private int _port;
        private bool _enableSSL;

        public SmtpEmailSender(string host, string userName, string password, int port, bool enableSSL)
        {
            _host = host;
            _userName = userName;
            _password = password;
            _port = port;
            _enableSSL = enableSSL;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_host,_port)
            {
                Credentials=new NetworkCredential(_userName,_password),
                EnableSsl=_enableSSL
            };

            return client.SendMailAsync(new MailMessage(_userName,email,subject,htmlMessage){IsBodyHtml=true});
        }




    }
}