using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TwitterStreamExtractor.Core.Model;

namespace TwitterStreamExtractor.Core.Data
{
    public class TrackedHashtagContext
    {
        private readonly IMongoDatabase _database = null;
        private readonly string _conn = null;
        private readonly string _dbname = null;
        private readonly string _collection = null;

        public TrackedHashtagContext(IConfigurationRoot Configuration)
        {
            _conn = Configuration["BBDD:ConnectionString"];
            _dbname = Configuration["BBDD:Database"];
            _collection = Configuration["BBDD:CollectionTrackedHashtags"];

            var client = new MongoClient(_conn);
            if (client != null)
                _database = client.GetDatabase(_dbname);
        }

        public IMongoCollection<TrackedHashtag> TrackedHashtag
        {
            get
            {
                return _database.GetCollection<TrackedHashtag>(_collection);
            }
        }
    }
}
