using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterStreamExtractor.Core.Model;

namespace TwitterStreamExtractor.Core.Service
{
    public interface ITrackedHashtagService
    {
        /// <summary>
        /// Returns the list of hashtags to track
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TrackedHashtag>> GetTrackedHashtags();
    }
}
