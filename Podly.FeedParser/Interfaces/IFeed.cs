﻿using System;
using System.Collections.Generic;

namespace Podly.FeedParser
{
    /// <summary>
    /// The interface used to represent all of the common elements between different types of syndicaiton feeds.
    /// The most common use case for this library doesn't require more than what's contained in this interface, although
    /// subsequent child implementations do have additional members which only pertain to the specifications of particular
    /// syndication feed standards.
    /// </summary>
    public interface IFeed
    {
        /// <summary>
        /// The title of the syndication feed.
        /// </summary>
        string Title { get;}

        /// <summary>
        /// A link to the general web resource hosting the syndication feed.
        /// I.e. the feed URI might be http://www.aaronstannard.com/syndication.axd but the link is to just http://www.aaronstannard.com/, the blog hosting the syndicated content.
        /// </summary>
        string Link { get; }

        /// <summary>
        /// A Uri referring to the feed itself.
        /// </summary>-
        string FeedUri { get; }

        /// <summary>
        /// The UTC date and time this syndication feed was last updated.
        /// </summary>
        DateTime LastUpdated { get; }

        /// <summary>
        /// The generator of this feed.
        /// </summary>
        string Generator { get; }

        /// <summary>
        /// The description of this syndication feed. Not implemented in Atom.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The language this syndication feed is encoded in. Not implemented in Atom.
        /// </summary>
        string Language { get; }

        /// <summary>
        /// The image URL of this syndication feed. Not implemented in Atom.
        /// </summary>
        string CoverImageUrl { get; }

        /// <summary>
        /// The type of syndication feed.
        /// </summary>
        FeedType FeedType { get; }

        /// <summary>
        /// Returns a list of all of the items in the feed.
        /// </summary>
        /// <value>An list of IFeedItems.</value>
        List<BaseFeedItem> Items { get; }
    }
}
