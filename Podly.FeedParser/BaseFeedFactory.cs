using System;
using System.Threading.Tasks;
using System.Xml;
using Podly.FeedParser.Xml;

namespace Podly.FeedParser
{
    public abstract partial class BaseFeedFactory : IFeedFactory
    {

        #region protected members

        protected IFeedXmlParser _parser;
        private readonly IFeedInstanceProvider _instanceProvider;

        #endregion

        #region constructors

        protected BaseFeedFactory(IFeedXmlParser parser)
        {
            _parser = parser;
        }

        protected BaseFeedFactory(IFeedXmlParser parser, IFeedInstanceProvider instanceProvider)
            :this(parser)
        {
            _instanceProvider = instanceProvider;
            _instanceProvider = instanceProvider ?? new DefaultFeedInstanceProvider();
        }

        #endregion

        #region abstract IFeedFactory members

        public abstract Task<bool> PingFeed(Uri feeduri);


        public abstract Task<string> DownloadXml(Uri feeduri);


        #endregion

        #region IFeedFactory Members



        public IFeed CreateFeed(Uri feeduri)
        {
            var feedxml = this.DownloadXml(feeduri);
            var feedtype = this.CheckFeedType(feedxml.Result);
            return this.CreateFeed(feeduri, feedtype, feedxml.Result);

        }

        public IFeed CreateFeed(Uri feeduri, int maxItems)
        {
            var feedxml = this.DownloadXml(feeduri);
            var feedtype = this.CheckFeedType(feedxml.Result);
            return this.CreateFeed(feeduri, feedtype, feedxml.Result, maxItems);

        }

        public IFeed CreateFeed(Uri feeduri, FeedType feedtype)
        {
            var feedxml = this.DownloadXml(feeduri);
            return this.CreateFeed(feeduri, feedtype, feedxml.Result);
        }


        public IFeed CreateFeed(Uri feeduri, FeedType feedtype, string feedxml, int maxItems = 9999)
        {
            try
            {
                IFeed returnFeed = _instanceProvider.CreateRss20Feed(feeduri.OriginalString);
                try
                {
                    _parser.ParseFeed(returnFeed, feedxml, maxItems);
                }
                catch (System.Xml.XmlException ex)
                {
                    throw new InvalidFeedXmlException(string.Format("The xml for feed {0} is invalid", feeduri), ex);
                }


                return returnFeed;
            }
            catch (XmlException ex)
            {
                throw new InvalidFeedXmlException(string.Format("Invalid XML for feed {0}", feeduri.AbsoluteUri), ex);
            }
        }

        public FeedType CheckFeedType(Uri feeduri)
        {
            try
            {
                var strXmlContent = this.DownloadXml(feeduri);
                return this.CheckFeedType(strXmlContent.Result);
            }
            catch (MissingFeedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidFeedXmlException(
                    string.Format("Unable to parse feedtype for feed at {0}", feeduri.AbsoluteUri), ex);
            }
        }

        public FeedType CheckFeedType(string feedxml)
        {
            try
            {
                return _parser.CheckFeedType(feedxml);
            }
            catch (XmlException ex)
            {
                throw new InvalidFeedXmlException("Unable to parse feedtype from feed", ex);
            }
        }

        #endregion
    }
}
