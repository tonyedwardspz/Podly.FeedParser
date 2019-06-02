using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Podly.FeedParser.Tests.AsyncTests
{
    [TestFixture, Description("Tests to ensure that the appropriate exceptions are thrown when an Asynchronous FileSystemFactory tries to parse a missing feed.")]
    public class AsyncMissingFileSysFeedFactoryTest : BaseMultiTestCase
    {
        protected IFeedFactory Factory;
        protected FeedType FeedType;

        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.FileSys);

        public AsyncMissingFileSysFeedFactoryTest()
            : base(TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.FileSys))
        {
            Factory = new FileSystemFeedFactory();
            FeedType = FeedType.Rss20;
        }

        [Test, TestCaseSource("TestCases"), Description("Tests to see is a MissingFeedException is thrown when the FileSys feed factory attempts to DownloadXml from a non-existent file.")]
        public void DoesFileSystemFeedFactoryThrowExceptionWhenDownloadXmlAsyncAcceptsMissingFile(string rsslocation)
        {
            var feeduri = new Uri(rsslocation);
            Assert.That(() =>
            {
                var result = Factory.BeginDownloadXml(feeduri, null);
                var resultantTuple = Factory.EndDownloadXml(result);
            }, Throws.TypeOf<MissingFeedException>());
        }

        [Test, TestCaseSource("TestCases"), Description("Tests to see is a MissingFeedException is thrown when the FileSys feed factory attempts to DownloadXml from a non-existent file.")]
        public void DoesFileSystemFeedFactoryThrowExceptionWhenCreateFeedAsyncAcceptsMissingFile(string rsslocation)
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
