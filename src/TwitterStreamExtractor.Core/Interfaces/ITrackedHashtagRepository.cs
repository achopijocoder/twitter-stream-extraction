using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using TwitterStreamExtractor.Core.Model;
using System.Threading.Tasks;

namespace TwitterStreamExtractor.Core.Interfaces
{
    public interface ITrackedHashtagRepository
    {
        Task<IEnumerable<TrackedHashtag>> GetAllTrackedHashtag();
        Task<TrackedHashtag> GetTrackedHashtag(string ht);
        Task AddTrackedHashtag(TrackedHashtag item);
        Task<DeleteResult> RemoveTrackedHashtag(string ht);

        Task<UpdateResult> UpdateTrackedHashtag(TrackedHashtag item, string ht);

        // should be used with high cautious, only in relation with demo setup
        Task<DeleteResult> RemoveAllTrackedHashtags();
    }
}
