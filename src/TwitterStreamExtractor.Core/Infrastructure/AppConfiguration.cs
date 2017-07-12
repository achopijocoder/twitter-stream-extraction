using Microsoft.Extensions.Configuration;
using System.IO;
using Tweetinvi.Models;
using TwitterStreamExtractor.Utils.Mail;

namespace TwitterStreamExtractor.Core.Infrastructure
{
    public class AppConfiguration
    {
        static public IConfigurationRoot ConfigureApp(string appSettingsFile)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.dev.json", optional: true, reloadOnChange: true)
                .Build();
        }

        static public TwitterCredentials ConfigureTwitterCredentials(IConfigurationRoot config)
        {
            return new TwitterCredentials(
                    config["TwitterAPI:CONSUMERKEY"],
                    config["TwitterAPI:CONSUMER_SECRET"],
                    config["TwitterAPI:ACCESS_TOKEN"],
                    config["TwitterAPI:ACCESS_TOKEN_SECRET"]);
        }

        static public MailConfiguration ConfigureMail(IConfigurationRoot config)
        {
            return new MailConfiguration(
                    config["Mail:FromAddress"],
                    config["Mail:FromAddressTitle"],
                    config["Mail:ToAddress"],
                    config["Mail:ToAdressTitle"],
                    config["Mail:Subject"],
                    config["Mail:SmtpServer"],
                    int.Parse(config["Mail:SmtpPortNumber"]),
                    config["Mail:Password"]
                );
        }
    }
}
