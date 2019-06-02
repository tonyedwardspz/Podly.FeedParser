using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Podly.FeedParser.Tests.AsyncTests
{
    public class AsyncMissingHttpFeedFactoryTests : BaseMultiTestCase
    {
        protected IFeedFactory Factory;
        protected FeedType FeedType;

        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.Http);

        public AsyncMissingHttpFeedFactoryTests()
            : base(TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.Http))
        {
            Factory = new HttpFeedFactory();
            FeedType = FeedType.Rss20;
        }

        [Test, TestCaseSource("TestCases"), Description("Tests to see is a MissingFeedException is thrown when the Http feed factory attempts to DownloadXml from a non-existent file.")]
        public void DoesHttpFeedFactoryThrowExceptionWhenDownloadXmlAsyncAcceptsMissingFile(string rsslocation)
        {
            var feeduri = new Uri(rsslocation);
            Assert.That(() =>
            {
                var result = Factory.BeginDownloadXml(feeduri, null);
                var resultantTuple = Factory.EndDownloadXml(result);
            }, Throws.TypeOf<MissingFeedException>());
        }

        [Test, TestCaseSource("TestCases"), Description("Tests to see is a MissingFeedException is thrown when the Http feed factory attempts to DownloadXml from a non-existent file.")]
        public void DoesHttpFeedFactoryThrowExceptionWhenCreateFeedAsyncAcceptsMissingFile(string rsslocation)
        {
            var feeduri = new Uri(rsslocation);
            Assert.That(() =>
            {
                var result = Factory.BeginCreateFeed(feeduri, null);
                var resultantFeed = Factory.EndCreateFeed(result);
            }, Throws.TypeOf<MissingFeedException>());
        }

    }
}
