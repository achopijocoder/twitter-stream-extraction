using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TwitterStreamExtractor.Core.Data;
using TwitterStreamExtractor.Core.Interfaces;
using TwitterStreamExtractor.Core.Model;

namespace TwitterStreamExtractor.Core.Service
{
    public class DayStatsService : IDayStatService
    {
        readonly IDayStatRepository _repository;

        public DayStatsService(IConfigurationRoot config)
        {
            _repository = new DayStatRepository(config);
        }

        public List<string> GetTrackedHashtags()
        {
            return new List<string>() { "#test" };
        }

        public async void NewTweetPublished(string[] hashtags, DateTime d)
        {
            DateTime utc = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0).ToUniversalTime();
            string id = utc.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
            DayStats ds = await _repository.GetDayStat(utc.ToString());
            if(ds != null)
            {
                //Update
                _repository.
            }
            else
            {
                DayStats newds = new DayStats()
                {
                    TotalSamples = 1,
                    TotalTweets = 1,
                    Id = id
                };
                //add new
                _repository.AddDayStat();
            }
            

            return;
        }
    }
}
