using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStreamExtractor
{
    public class TwitterStreamActionMyAPP : ITwitterStreamAction
    {
        List<string> ITwitterStreamAction.GetTrackedHashtags()
        {
            throw new NotImplementedException();
        }

        void ITwitterStreamAction.NewTweetPublished(string[] hashtag)
        {
            throw new NotImplementedException();
        }
    }
}
