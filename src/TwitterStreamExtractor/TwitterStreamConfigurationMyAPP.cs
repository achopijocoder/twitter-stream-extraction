using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStreamExtractor
{
    public class TwitterStreamConfigurationMyAPP : ITwitterStreamConfiguration
    {
        List<string> ITwitterStreamConfiguration.GetTrackedHashtags()
        {
            return new List<string>() { "#test" };
        }

        void ITwitterStreamConfiguration.NewTweetPublished(string[] hashtag)
        {
            return;
        }
    }
}
