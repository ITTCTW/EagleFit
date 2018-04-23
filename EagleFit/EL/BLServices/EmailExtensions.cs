using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace EL.Models
{
    public static class EmailExtensions
    {
        public static void Send(this IdentityMessage message)
        {
            try
            {
                //waarden die we nodig hebben uit web.config appsettings halen
                var password = ConfigurationManager.AppSettings["password"];
                var from = ConfigurationManager.AppSettings["from"];
                var host = ConfigurationManager.AppSettings["host"];
                var port = int.Parse(ConfigurationManager.AppSettings["port"]);

                //De email aanmaken
                var email = new MailMessage(from, message.Destination, message.Subject, message.Body);
                email.IsBodyHtml = true;

                //smtp client aanmaken
                var client = new SmtpClient(host, port);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from, password);

                //email verzenden
                client.Send(email);
            }

            //Als dit niet lukt geven we een bericht weer
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
