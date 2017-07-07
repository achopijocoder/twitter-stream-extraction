using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace TwitterStreamExtractor.Utils
{
    public class Mail
    {
        
        public static void SendMail(MailConfiguration config, string message)
        {
            try
            {
                //From Address
                string FromAddress = config.FromAddress;
                string FromAdressTitle = config.FromAddressTitle;
                //To Address
                string ToAddress = config.ToAddress;
                string ToAdressTitle = config.ToAdressTitle;
                string Subject = config.Subject;
                string BodyContent = "Message: " + message;

                //Smtp Server
                string SmtpServer = config.SmtpServer;
                //Smtp Port Number
                int SmtpPortNumber = config.SmtpPortNumber;

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
                    client.Authenticate(config.FromAddress, config.Password);
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
