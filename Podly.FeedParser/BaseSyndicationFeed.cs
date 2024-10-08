﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Podly.FeedParser
{
    [XmlInclude(typeof(Rss20FeedItem))]
    public abstract class BaseSyndicationFeed : IFeed
    {
        #region Constructors

        protected BaseSyndicationFeed(FeedType feedType)
        {
            FeedType = feedType;
            Items = new List<BaseFeedItem>();
        }

        protected BaseSyndicationFeed(string feeduri, FeedType feedtype) : this(feedtype)
        {
            FeedUri = feeduri;
        }

        #endregion

        #region ISyndicationFeed Members

        public string Title
        {
            get; set;
        }

        public string Link
        {
            get; set;
        }

        public string FeedUri
        {
            get; set;
        }

        public DateTime LastUpdated
        {
            get; set;
        }

        public string Generator
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string Language
        {
            get; set;
        }

        public string CoverImageUrl 
        { 
            get; set; 
        }

        public FeedType FeedType
        {
            get; set;
        }
        
        public List<BaseFeedItem> Items
        {
            get; set;
        }

        #endregion

    }
}
