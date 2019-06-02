using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.FeedFactory.Missing.FileSys
{
    [TestFixture, Description("Tests the FileFeedFactory's behavior when its given a number of missing files.")]
    public class MissingFileSysFeedFactoryTests : BaseMissingFeedFactoryTest
    {
        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.FileSys);

        public MissingFileSysFeedFactoryTests() : base(new FileSystemFeedFactory(), FeedType.Rss20, TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.FileSys))
        {
        }
    }
}
