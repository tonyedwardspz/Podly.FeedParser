using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.FeedFactory.Valid.Http
{
    [TestFixture, Description("Runs all of the routine tests on the HttpFeedFactory object for Atom 1.0 feeds.")]
    public class HttpFeedFactoryTestsAtom : BaseFeedFactoryTests<Atom10Feed>
    {
        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadValidAtomTestCases(TestFileLoader.TestFileType.Http);

        public HttpFeedFactoryTestsAtom()
            : base(new HttpFeedFactory(), FeedType.Atom10, TestFileLoader.LoadValidAtomTestCases(TestFileLoader.TestFileType.Http))
        {
        }
    }
}
