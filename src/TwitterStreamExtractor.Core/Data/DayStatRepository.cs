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
    public class DayStatRepository : IDayStatRepository
    {
        private readonly DayStatContext _context = null;

        public DayStatRepository(IConfigurationRoot config)
        {
            _context = new DayStatContext(config);
        }

        public async Task<IEnumerable<DayStats>> GetAllDayStat()
        {
            try
            {
                return await _context.DayStats.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DayStats> GetDayStat(DateTime date)
        {
            var filter = Builders<DayStats>.Filter.Eq("Date", date);

            try
            {
                return await _context.DayStats
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddDayStat(DayStats item)
        {
            try
            {
                await _context.DayStats.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveDayStat(string id)
        {
            try
            {
                return await _context.DayStats.DeleteOneAsync(
                     Builders<DayStats>.Filter.Eq("Id", id));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<UpdateResult> UpdateDayStat(DateTime d)
        {
            //hay que calcular el numero de samples hasta el momento actual
            var numSamples = d.Hour * 60 + d.Minute;

            var filter = Builders<DayStats>.Filter.Eq(s => s.Date, new DateTime(d.Year, d.Month,d.Day,0,0,0));
            var update = Builders<DayStats>.Update
                            .Set(s => s.TotalSamples, numSamples)
                            .Inc(s => s.TotalTweets, 1)
                            .Inc(s => s.Counters[d.Hour][d.Minute],1);

            try
            {
                return await _context.DayStats.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<ReplaceOneResult> UpdateDayStat(string id, DayStats item)
        {
            try
            {
                return await _context.DayStats
                            .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveAllDayStat()
        {
            try
            {
                return await _context.DayStats.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
