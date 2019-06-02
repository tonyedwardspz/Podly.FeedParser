using NUnit.Framework;
using System.Collections.Generic;

namespace Podly.FeedParser.Tests.FeedFactory.Valid.FileSys
{
    [TestFixture, Description("Tests all of the BaseFeedFactory super class' functionality with Atom feeds by way of a FileFeedFactory instance.")]
    public class FileFeedFactoryTestsAtom : BaseFeedFactoryTests<Atom10Feed>
    {
        public static IEnumerable<TestCaseData> TestCases = TestFileLoader.LoadValidAtomTestCases(TestFileLoader.TestFileType.FileSys);

        public FileFeedFactoryTestsAtom()
            : base(new FileSystemFeedFactory(), FeedType.Atom10, TestFileLoader.LoadValidAtomTestCases(TestFileLoader.TestFileType.FileSys))
        {
        }

    }
}
