using Microsoft.Extensions.Configuration;
using System;
using Tweetinvi.Models;
using TwitterStreamExtractor.Core.Infrastructure;
using TwitterStreamExtractor.Core.Service;
using TwitterStreamExtractor.Utils.Mail;

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
        private static IDayStatService _dayStatService;

        /// <summary>
        /// Configuration service for the mail log
        /// </summary>
        private static MailConfiguration mailConfig;

        public static void Main(string[] args)
        {
            #region Configuration

            //Configure app
            Configuration = AppConfiguration.ConfigureApp("appsettings.dev.json");

            //Configure mail service
            mailConfig = AppConfiguration.ConfigureMail(Configuration);

            //Configure twitter credentials
            _credentials = AppConfiguration.ConfigureTwitterCredentials(Configuration);

            //configure twitterStreamAction
            _dayStatService = new DayStatsService(Configuration);

            #endregion

            try
            {
                var stream = Tweetinvi.Stream.CreateFilteredStream(_credentials);

                //add tracks to follow
                foreach (string s in _dayStatService.GetTrackedHashtags())
                {
                    stream.AddTrack(s);
                }

                stream.MatchingTweetReceived += (sender, e) =>
                {
                    if (e.Tweet.IsRetweet) return;
                    _dayStatService.NewTweetPublished(e.MatchingTracks, DateTime.Now);
                };

                stream.DisconnectMessageReceived += (sender, e) =>
                {
                    Console.WriteLine("Disconnected'" + e.DisconnectMessage.Reason + "'");
                    Mail.SendMail(mailConfig, " Disconnected " + e.DisconnectMessage.Reason);
                };

                stream.StreamStopped += (sender, e) =>
                {
                    Console.WriteLine("Stopped '" + e.DisconnectMessage + "'");
                    Mail.SendMail(mailConfig, " Stopped " + e.DisconnectMessage.Reason);
                };

                stream.StartStreamMatchingAnyCondition();
            }
            catch (Exception e)
            {
                Mail.SendMail(mailConfig, " EXCEPTION " + e.Message);
            }
        }
    }
}