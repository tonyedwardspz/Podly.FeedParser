using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.FeedFactory.Valid.Http
{
    [TestFixture, Description("Runs all of the routine tests on the HttpFeedFactory object for RSS 2.0 feeds.")]
    public class HttpFeedFactoryTestsRss: BaseFeedFactoryTests<Rss20Feed>
    {
        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.Http);

        public HttpFeedFactoryTestsRss()
            : base(new HttpFeedFactory(), FeedType.Rss20, TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.Http))
        {
        }
    }
}
