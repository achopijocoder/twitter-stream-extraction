using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStreamExtractor
{
    public interface ITwitterStreamConfiguration
    {
        /// <summary>
        /// Returns the list of hashtags to track
        /// </summary>
        /// <returns></returns>
        List<string> GetTrackedHashtags();

        /// <summary>
        /// Action to take when a new Tweet that matches the tracked hashtags has been published 
        /// </summary>
        /// <param name="hashtag"></param>
        void NewTweetPublished(string[] hashtag);
    }
}
