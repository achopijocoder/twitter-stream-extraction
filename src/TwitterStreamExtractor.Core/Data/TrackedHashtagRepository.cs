using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using TwitterStreamExtractor.Core.Model;
using TwitterStreamExtractor.Core.Interfaces;

namespace TwitterStreamExtractor.Core.Data
{
    public class TrackedHashtagRepository : ITrackedHashtagRepository
    {
        private readonly TrackedHashtagContext _context = null;

        public TrackedHashtagRepository(IConfigurationRoot config)
        {
            _context = new TrackedHashtagContext(config);
        }

        public async Task<IEnumerable<TrackedHashtag>> GetAllTrackedHashtag()
        {
            try
            {
                return await _context.TrackedHashtag.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<TrackedHashtag> GetTrackedHashtag(string ht)
        {
            var filter = Builders<TrackedHashtag>.Filter.Eq("HashTag", ht);

            try
            {
                return await _context.TrackedHashtag
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddTrackedHashtag(TrackedHashtag item)
        {
            try
            {
                await _context.TrackedHashtag.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveTrackedHashtag(string ht)
        {
            try
            {
                return await _context.TrackedHashtag.DeleteOneAsync(
                     Builders<TrackedHashtag>.Filter.Eq("HashTag", ht));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<UpdateResult> UpdateTrackedHashtag(TrackedHashtag item, string ht)
        {
            var filter = Builders<TrackedHashtag>.Filter.Eq(s => s.HashTag, item.HashTag);
            var update = Builders<TrackedHashtag>.Update
                            .Set(s => s.HashTag, ht);

            try
            {
                return await _context.TrackedHashtag.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveAllTrackedHashtags()
        {
            try
            {
                return await _context.TrackedHashtag.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
