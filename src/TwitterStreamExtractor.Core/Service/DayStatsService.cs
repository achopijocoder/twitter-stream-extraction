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
            d = d.ToUniversalTime();
            DayStats ds = await _repository.GetDayStat(new DateTime(d.Year, d.Month,d.Day,0,0,0));

            if(ds == null)
            {
                int[][] count = new int[24][];
                for (int i = 0; i < 24; i++) count[i] = new int[60];
                count[d.Hour][d.Minute] = 1;

                DayStats new_ds = new DayStats()
                {
                    Date = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0),
                    TotalSamples = d.Hour * 60 + d.Minute,
                    TotalTweets = 1,
                    Counters = count
                };

                await _repository.AddDayStat(new_ds);
            }
            else
            {
                await _repository.UpdateDayStat(d);
            }

            return;
        }
    }
}
