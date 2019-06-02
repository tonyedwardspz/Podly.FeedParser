using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.FeedFactory.Valid.FileSys
{
    [TestFixture, Description("Tests all of the BaseFeedFactory super class' functionality with RSS feeds by way of a FileFeedFactory instance.")]
    public class FileFeedFactoryTestsRss : BaseFeedFactoryTests<Rss20Feed>
    {
        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.FileSys);

        public FileFeedFactoryTestsRss()
            : base(new FileSystemFeedFactory(), FeedType.Rss20, TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.FileSys))
        {
        }
    }
}
