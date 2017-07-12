using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TwitterStreamExtractor.Core.Model;

namespace TwitterStreamExtractor.Core.Data
{
    public class DayStatContext
    {
        private readonly IMongoDatabase _database = null;

        public DayStatContext(IConfigurationRoot Configuration)
        {
            var client = new MongoClient(Configuration["BBDD:ConnectionString"]);
            if (client != null)
                _database = client.GetDatabase(Configuration["BBDD:Database"]);
        }

        public IMongoCollection<DayStats> DayStats
        {
            get
            {
                return _database.GetCollection<DayStats>("DayStats");
            }
        }
    }
}
