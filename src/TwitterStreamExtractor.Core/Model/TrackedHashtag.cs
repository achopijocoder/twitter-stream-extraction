using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace TwitterStreamExtractor.Core.Model
{
    [BsonIgnoreExtraElements]
    public class TrackedHashtag
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string HashTag { get; set; }
    }
}