using MailKit.Net.Smtp;
using MimeKit;
using System;
using Microsoft.Extensions.Configuration;

namespace TwitterStreamExtractor.Utils
{
    public class Mail
    {
        public static void SendMail(IConfigurationRoot config, string message)
        {
            try
            {
                //From Address
                string FromAddress = config["Mail:FromAddress"];
                string FromAdressTitle = config["Mail:FromAddressTitle"];
                //To Address
                string ToAddress = config["Mail:ToAddress"];
                string ToAdressTitle = config["Mail:ToAddressTitle"];
                string Subject = config["Mail:Subject"];
                string BodyContent = "Message: " + message;

                //Smtp Server
                string SmtpServer = config["Mail:SmtpServer"];
                //Smtp Port Number
                int SmtpPortNumber = int.Parse(config["Mail:SmtpPortNumber"]);

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent

                };

                using (var client = new SmtpClient())
                {

                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    // Note: only needed if the SMTP server requires authentication
                    // Error 5.5.1 Authentication 
                    client.Authenticate(config["Mail:FromAddress"], config["Mail:Password"]);
                    client.Send(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully !!");
                    Console.ReadLine();
                    client.Disconnect(true);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
