namespace Podly.FeedParser
{
    public class Atom10Feed : BaseSyndicationFeed
    {
        #region Constructors

        /// <summary>
        /// Default constructor for Atom10Feed
        /// </summary>
        public Atom10Feed():base(FeedType.Atom10){}

        /// <summary>
        /// Constuctor for Atom10Feed object
        /// </summary>
        /// <param name="feeduri">The Uri used to identify the feed</param>
        public Atom10Feed(string feeduri) : base(feeduri, FeedType.Atom10)
        {
        }

        #endregion

        /// <summary>
        /// The description of this atom feed. Currently not implemented.
        /// </summary>
        public string Description
        {
            get => string.Empty;
            set => value = string.Empty;
        }

        /// <summary>
        /// The language this atom feed is encoded in. Currently not implemented.
        /// </summary>
        public string Language
        {
            get => string.Empty;
            set => value = string.Empty;
        }

        /// <summary>
        /// The image URL of this RSS feed. Currently not implemented.
        /// </summary>
        public string CoverImageUrl 
        { 
            get => string.Empty;
            set => value = string.Empty;
        }
    }
}
