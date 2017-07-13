using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using Tweetinvi.Models;
using TwitterStreamExtractor.Core.Infrastructure;
using TwitterStreamExtractor.Core.Model;
using TwitterStreamExtractor.Core.Service;
using TwitterStreamExtractor.Utils.Mail;
using Nito.AsyncEx;

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
        /// Interface for different implementations for twitterStreamAction
        /// </summary>
        private static ITrackedHashtagService _trackedHashtagService;

        /// <summary>
        /// Configuration service for the mail log
        /// </summary>
        private static MailConfiguration mailConfig;

        public static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        static async void MainAsync(string[] args)
        {
            #region Configuration

            //Configure app
            Configuration = AppConfiguration.ConfigureApp("appsettings.dev.json");

            //Configure mail service
            mailConfig = AppConfiguration.ConfigureMail(Configuration);

            //Configure twitter credentials
            _credentials = AppConfiguration.ConfigureTwitterCredentials(Configuration);

            //configure dayStats service
            _dayStatService = new DayStatsService(Configuration);

            //configure trackedHashtags service
            _trackedHashtagService = new TrackedHashtagService(Configuration);

            #endregion

            try
            {
                var stream = Tweetinvi.Stream.CreateFilteredStream(_credentials);

                IEnumerable<TrackedHashtag> hts = await _trackedHashtagService.GetTrackedHashtags();
                //add tracks to follow
                foreach (TrackedHashtag s in hts)
                {
                    stream.AddTrack(s.HashTag);
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