namespace Podly.FeedParser
{
    public interface IFeedInstanceProvider
    {
        Rss20Feed CreateRss20Feed(string feeduri);
    }
}
