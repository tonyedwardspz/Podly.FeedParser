﻿using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.SyndicationFeed.Valid
{
    [TestFixture, Description("All basic tests for ensuring that high-level RSS feed parsing actually works.")]
    public class RssFeedTest : BaseSyndicationFeedTest<Rss20Feed>
    {
        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.FileSys);
        public RssFeedTest() : base(new FileSystemFeedFactory(), TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.FileSys))
        {
        }
    }
}
