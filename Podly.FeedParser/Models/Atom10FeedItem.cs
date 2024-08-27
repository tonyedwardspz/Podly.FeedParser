using System;

namespace Podly.FeedParser
{
    public class Atom10FeedItem : BaseFeedItem
    {
        public DateTime Updated { get; set; }

        /// <summary>
        /// The description of this atom feed. Currently not implemented.
        /// </summary>
        public string Description
        {
            get => string.Empty;
            set => value = string.Empty;
        }

        /// <summary>
        /// The cover image URL of this podcast episode. Currently not implemented.
        /// </summary>
        public string Cover { 
            get => string.Empty; 
            set => value = string.Empty; 
        }

        /// <summary>
        /// The episode number of this podcast episode. Currently not implemented.
        /// </summary>
        public string EpisodeNumber { 
            get => string.Empty; 
            set => value = string.Empty; 
        }
    }
}
