using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterStreamExtractor.Core.Data;
using TwitterStreamExtractor.Core.Interfaces;
using TwitterStreamExtractor.Core.Model;

namespace TwitterStreamExtractor.Core.Service
{
    public class TrackedHashtagService : ITrackedHashtagService
    {
        readonly ITrackedHashtagRepository _repository;

        public TrackedHashtagService(IConfigurationRoot config)
        {
            _repository = new TrackedHashtagRepository(config);
        }

        public async Task<IEnumerable<TrackedHashtag>> GetTrackedHashtags()
        {
            return await _repository.GetAllTrackedHashtag();
        }
    }
}
