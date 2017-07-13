using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace TwitterStreamExtractor.Core.Model
{
    [BsonIgnoreExtraElements]
    public class DayStats
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        [BsonDateTimeOptions(DateOnly =true, Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        public DateTime Date { get; set; }

        public int TotalTweets { get; set; } = 1;
        public int TotalSamples { get; set; } = 0;
        public int[][] Counters { get; set; }

    }
}