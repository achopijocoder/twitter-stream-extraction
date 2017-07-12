using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TwitterStreamExtractor.Core.Model
{
    public class DayStats
    {
        [BsonId]
        public string Id { get; set; }
        public int TotalTweets { get; set; } = 0;
        public int TotalSamples { get; set; } = 0;
        public List<int[]> Counters { get; set; } = new List<int[]>();
    }
}