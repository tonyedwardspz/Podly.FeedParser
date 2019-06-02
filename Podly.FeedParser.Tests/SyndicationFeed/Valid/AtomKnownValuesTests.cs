using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.SyndicationFeed.Valid
{
    [TestFixture]
    public class AtomKnownValuesTests : BaseKnownValueTest
    {
        public static IEnumerable<TestCaseData> TestCases = KnownValueTestLoader.LoadAtomKnownValueTestCases();

        public AtomKnownValuesTests() : base(new FileSystemFeedFactory(), KnownValueTestLoader.LoadAtomKnownValueTestCases())
        {
        }
    }
}
