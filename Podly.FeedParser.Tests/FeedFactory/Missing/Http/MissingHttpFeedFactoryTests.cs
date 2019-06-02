using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.FeedFactory.Missing.Http
{
    [TestFixture, Description("Tests the FileFeedFactory's behavior when its given a number of missing files.")]
    public class MissingHttpFeedFactoryTests : BaseMissingFeedFactoryTest
    {
        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.Http);

        public MissingHttpFeedFactoryTests()
            : base(new HttpFeedFactory(), FeedType.Rss20, TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.Http))
        {
        }
    }
}
