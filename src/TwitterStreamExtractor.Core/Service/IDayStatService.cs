using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStreamExtractor.Core.Service
{
    public interface IDayStatService
    {
        /// <summary>
        /// Action to take when a new Tweet that matches the tracked hashtags has been published 
        /// </summary>
        /// <param name="hashtag"></param>
        void NewTweetPublished(string[] hashtags, DateTime d);
    }
}
