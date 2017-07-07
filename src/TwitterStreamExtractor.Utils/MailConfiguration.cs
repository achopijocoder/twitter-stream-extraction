
namespace TwitterStreamExtractor.Utils
{
    public class MailConfiguration
    {
        public string FromAddress { get; set; }
        public string FromAddressTitle { get; set; }
        public string ToAddress { get; set; }
        public string ToAdressTitle { get; set; }
        public string Subject { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPortNumber { get; set; }
        public string Password { get; set; }

        public MailConfiguration(
            string fromAddres,
            string fromAddressTitle, 
            string toAddress, 
            string toAddressTitle,
            string subject,
            string smtpServer,
            int smtpPortNumber,
            string pass)
        {
            this.FromAddress = fromAddres;
            this.FromAddressTitle = fromAddressTitle;
            this.ToAddress = toAddress;
            this.ToAdressTitle = toAddressTitle;
            this.Subject = subject;
            this.SmtpPortNumber = smtpPortNumber;
            this.SmtpServer = smtpServer;
            this.Password = pass;
        }
    }
}
