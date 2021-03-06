﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TwitterStreamExtractor.Core.Model;

namespace TwitterStreamExtractor.Core.Data
{
    public class DayStatContext
    {
        private readonly IMongoDatabase _database = null;
        private readonly string _conn = null;
        private readonly string _dbname = null;
        private readonly string _collection = null;

        public DayStatContext(IConfigurationRoot Configuration)
        {
            _conn = Configuration["BBDD:ConnectionString"];
            _dbname = Configuration["BBDD:Database"];
            _collection = Configuration["BBDD:CollectionDayStat"];

            var client = new MongoClient(_conn);
            if (client != null)
                _database = client.GetDatabase(_dbname);
        }

        public IMongoCollection<DayStats> DayStats
        {
            get
            {
                return _database.GetCollection<DayStats>(_collection);
            }
        }
    }
}
