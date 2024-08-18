namespace Podly.FeedParser
{
    public class Rss20Feed : BaseSyndicationFeed
    {
        #region Constructors

        /// <summary>
        /// Default constructor for Rss20Feed objects
        /// </summary>
        public Rss20Feed() : base(FeedType.Rss20){}

        /// <summary>
        /// Constructor for Rss20Feed objects
        /// </summary>
        /// <param name="feeduri">The Uri which uniquely identifies the feed</param>
        public Rss20Feed(string feeduri) : base(feeduri, FeedType.Rss20)
        {
        }

        #endregion
    }
}
