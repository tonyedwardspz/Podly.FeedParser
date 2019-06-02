using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.SyndicationFeed.Valid
{
    [TestFixture, Description("All basic tests for ensuring that high-level Atom feed parsing actually works.")]
    public class AtomFeedTest : BaseSyndicationFeedTest<Atom10Feed>
    {
        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadValidAtomTestCases(TestFileLoader.TestFileType.FileSys);

        public AtomFeedTest()
            : base(new FileSystemFeedFactory(), TestFileLoader.LoadValidAtomTestCases(TestFileLoader.TestFileType.FileSys))
        {
        }
    }
}

