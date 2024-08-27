using System;
using System.Linq;
using System.Xml.Linq;

namespace Podly.FeedParser.Xml
{
    public class LinqFeedXmlParser : FeedXmlParserBase
    {
        #region Overrides of FeedXmlParserBase

        public override void ParseFeed(IFeed feed, string xml, int maxItems = 9999)
        {
            switch (feed.FeedType)
            {
                case FeedType.Rss20:
                    var rssFeed = feed as Rss20Feed;
                    ParseRss20Header(rssFeed, xml);
                    ParseRss20Items(rssFeed, xml, maxItems);
                    break;
            }
        }

        public override FeedType CheckFeedType(string feedxml)
        {
            var doc = XDocument.Parse(feedxml);
            var xmlRootElement = doc.Root;
            if (xmlRootElement.Name.LocalName.Contains(RssRootElementName) && xmlRootElement.Attribute(RssVersionAttributeName).Value == "2.0")
            {
                return FeedType.Rss20;
            }
            else
                throw new InvalidFeedXmlException("Unable to determine feedtype (but was able to parse file) for feed");
        }

        #region RSS 2.0 Parsing Methods

        protected virtual void ParseRss20Header(Rss20Feed rssFeed, string xml)
        {
            var document = XDocument.Parse(xml);
            var channel = document.Root.Element("channel");

            rssFeed.Title = channel.Element("title").Value;
            rssFeed.Description = channel.Element("description").Value;

            var linkNode = channel.Element("link");
            rssFeed.Link = linkNode == null ? string.Empty : linkNode.Value;

            var dateTimeNode = (from dateSelector in channel.Elements("pubDate")
                                select dateSelector).FirstOrDefault();
            if (dateTimeNode == null)
            {
                rssFeed.LastUpdated = DateTime.UtcNow;
            }
            else
            {
                DateTime timeOut;
                DateTime.TryParse(dateTimeNode.Value, out timeOut);
                rssFeed.LastUpdated = timeOut.ToUniversalTime();
            }

            var generatorNode = channel.Element("generator");
            rssFeed.Generator = generatorNode == null ? string.Empty : generatorNode.Value;

            var languageNode = channel.Element("language");
            rssFeed.Language = languageNode == null ? string.Empty : languageNode.Value;

            var imageNode = channel.Element("image")?.Element("url")?.Value;
            if (imageNode == null) {
                imageNode = channel.Element(XName.Get("image", "http://www.itunes.com/dtds/podcast-1.0.dtd"))?.Attribute("href")?.Value;
            }
            rssFeed.CoverImageUrl = imageNode == null ? string.Empty : imageNode;
        }

        private void ParseRss20Items(Rss20Feed rssFeed, string xml, int maxItems)
        {
            var document = XDocument.Parse(xml);
            var feedItemNodes = document.Root.Element("channel").Elements("item").Take(maxItems);
            foreach (var item in feedItemNodes)
            {
                rssFeed.Items.Add(ParseRss20SingleItem(item));
            }
        }

        protected virtual Rss20FeedItem CreateRss20FeedItem()
        {
            return new Rss20FeedItem();
        }

        protected virtual BaseFeedItem ParseRss20SingleItem(XElement itemNode)
        {
            var titleNode = itemNode.Element("title");
            var datePublishedNode = itemNode.Element("pubDate");
            var authorNode = itemNode.Element("author");
            var commentsNode = itemNode.Element("comments");
            var idNode = itemNode.Element("guid");
            var contentNode = itemNode.Element("description");
            var linkNode = itemNode.Element("link");
            var enclosureNode = itemNode.Element("enclosure");

            var itunesAuthorNode = itemNode.Element(XName.Get("author", "http://www.itunes.com/dtds/podcast-1.0.dtd"));
            var itunesImageNode = itemNode.Element(XName.Get("image", "http://www.itunes.com/dtds/podcast-1.0.dtd"));
            var itunesDurationNode = itemNode.Element(XName.Get("duration", "http://www.itunes.com/dtds/podcast-1.0.dtd"));
            var itunesEpisodeNode = itemNode.Element(XName.Get("episode", "http://www.itunes.com/dtds/podcast-1.0.dtd"));

            Rss20FeedItem item = CreateRss20FeedItem();

            item.Title = titleNode == null ? string.Empty : titleNode.Value;
            item.DatePublished = datePublishedNode == null ? DateTime.UtcNow : SafeGetDate(datePublishedNode.Value);

            item.Author = authorNode == null ? string.Empty : authorNode.Value;
            item.Author = itunesAuthorNode == null ? item.Author : itunesAuthorNode.Value;

            item.Comments = commentsNode == null ? string.Empty : commentsNode.Value;
            item.Id = idNode == null ? string.Empty : idNode.Value;
            item.Content = contentNode == null ? string.Empty : contentNode.Value;
            item.Link = linkNode == null ? string.Empty : linkNode.Value;
            item.Cover = itunesImageNode?.Attribute("href")?.Value ?? string.Empty;

            item.MediaUrl = SafeGetAttribute(enclosureNode, "url");

            item.MediaLength = SafeGetAttribute(enclosureNode, "length");
            item.MediaLength = item.MediaLength == null ? string.Empty : itunesDurationNode.Value;
            item.MediaType = SafeGetAttribute(enclosureNode, "type");
            
            item.EpisodeNumber = itunesEpisodeNode == null ? string.Empty : itunesEpisodeNode.Value;

            var categoryNodes = itemNode.Elements("category");
            foreach (var categoryNode in categoryNodes)
            {
                item.Categories.Add(categoryNode.Value);
            }

            return item;
        }

        private static string SafeGetAttribute(XElement node, string attributeName)
        {
            if (node == null) return null;
            var attribute = node.Attribute(attributeName);

            return attribute == null ? null : attribute.Value;
        }

        #endregion

        #endregion
    }
}
