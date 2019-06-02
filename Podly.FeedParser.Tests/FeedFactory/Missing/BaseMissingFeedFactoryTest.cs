using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Podly.FeedParser.Tests.FeedFactory.Missing
{
    
    public abstract class BaseMissingFeedFactoryTest : BaseFeedFactoryTests<Rss20Feed>
    {
        protected BaseMissingFeedFactoryTest(IFeedFactory factory, FeedType feedtype, IEnumerable<TestCaseData> testcases)
            : base(factory, feedtype, testcases)
        {}

        [Test, TestCaseSource("TestCases"), Description("Tests whether or not the FeedFactory can successfully determine the feed's type.")]
        public override void TestFactoryFeedTypeDetection(string rsslocation)
        {
            Uri feeduri = new Uri(rsslocation);
            Assert.That(() => Factory.CheckFeedType(feeduri), Throws.TypeOf<MissingFeedException>());
        }

        [Test, TestCaseSource("TestCases"), Description("Tests whether or not the FeedFactory fails to parse the feed which does not exist.")]
        public override void TestFactoryFeedObjectSynthesis(string rsslocation)
        {
            Uri feeduri = new Uri(rsslocation);
            Assert.That(() => Factory.CreateFeed(feeduri), Throws.TypeOf<MissingFeedException>());
        }

        [Test, TestCaseSource("TestCases"), Description("Tests that the FeedFactory properly fails to ping the missing URI.")]
        public override void TestFactoryFeedUriPing(string rsslocation)
        {
            Uri feeduri = new Uri(rsslocation);
            Assert.That(!Factory.PingFeed(feeduri), string.Format("Should not have been able to ping feed at location {0}", feeduri.OriginalString));
        }

        [Test, TestCaseSource("TestCases"), Description("Ensures that the FeedFactory object properly fails to load any XML from the missing file.")]
        public override void TestFactoryFeedXmlDownload(string rsslocation)
        {
            Uri feeduri = new Uri(rsslocation);
            Assert.That(() => Factory.DownloadXml(feeduri), Throws.TypeOf<MissingFeedException>());
        }
    }
}
