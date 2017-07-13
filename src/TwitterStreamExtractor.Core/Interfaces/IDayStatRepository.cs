using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using TwitterStreamExtractor.Core.Model;
using System.Threading.Tasks;

namespace TwitterStreamExtractor.Core.Interfaces
{
    public interface IDayStatRepository
    {
        Task<IEnumerable<DayStats>> GetAllDayStat();
        Task<DayStats> GetDayStat(DateTime date);
        Task AddDayStat(DayStats item);
        Task<DeleteResult> RemoveDayStat(string id);

        Task<UpdateResult> UpdateDayStat(DateTime date);

        // should be used with high cautious, only in relation with demo setup
        Task<DeleteResult> RemoveAllDayStat();
    }
}
