using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStreamExtractor
{
    public class TwitterStreamConfigurationMyAPP : ITwitterStreamConfiguration
    {
        List<string> ITwitterStreamConfiguration.GetTrackedHashtags()
        {
            throw new NotImplementedException();
        }

        void ITwitterStreamConfiguration.NewTweetPublished(string[] hashtag)
        {
            throw new NotImplementedException();
        }
    }
}
