namespace Podly.FeedParser
{
    internal class DefaultFeedInstanceProvider : IFeedInstanceProvider
    {
        public Rss20Feed CreateRss20Feed(string feeduri)
        {
            return new Rss20Feed(feeduri);
        }
    }
}

