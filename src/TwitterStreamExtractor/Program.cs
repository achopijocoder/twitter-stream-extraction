using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.IO;
using Tweetinvi.Models;
using TwitterStreamExtractor.Utils;

namespace TwitterStreamExtractor
{
    public class Program
    {
        /// <summary>
        /// Configuration app
        /// </summary>
        public static IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Credentials for twitter api 
        /// </summary>
        private static ITwitterCredentials _credentials;

        /// <summary>
        /// Interface for different implementations for twitterStreamAction
        /// </summary>
        private static ITwitterStreamAction twitterStreamAction;

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.dev.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            twitterStreamAction = new TwitterStreamActionMyAPP();

            Mail.SendMail(Configuration, " Stopped " );

            Console.ReadKey();
            try
            {
                var creds = new TwitterCredentials(
                    Configuration["TwitterAPI:CONSUMERKEY"],
                    Configuration["TwitterAPI:CONSUMER_SECRET"],
                    Configuration["TwitterAPI:ACCESS_TOKEN"],
                    Configuration["TwitterAPI:ACCESS_TOKEN_SECRET"]);

                var stream = Tweetinvi.Stream.CreateFilteredStream(creds);

                //add tracks to follow
                foreach (string s in twitterStreamAction.GetTrackedHashtags())
                {
                    stream.AddTrack(s);
                }

                stream.MatchingTweetReceived += (sender, e) =>
                {
                    if (e.Tweet.IsRetweet) return;
                    twitterStreamAction.NewTweetPublished(e.MatchingTracks);
                };

                stream.DisconnectMessageReceived += (sender, e) =>
                {
                    Console.WriteLine("Disconnected'" + e.DisconnectMessage.Reason + "'");
                    Mail.SendMail(Configuration, " Disconnected " + e.DisconnectMessage.Reason);
                };

                stream.StreamStopped += (sender, e) =>
                {
                    Console.WriteLine("Stopped '" + e.DisconnectMessage + "'");
                    Mail.SendMail(Configuration, " Stopped " + e.DisconnectMessage.Reason);
                };

                stream.StartStreamMatchingAnyCondition();
            }
            catch (Exception e)
            {
                Mail.SendMail(Configuration, " EXCEPTION " + e.Message);
            }
        }
    }
}