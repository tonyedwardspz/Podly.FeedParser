using System.IO;
using NUnit.Framework;

namespace Podly.FeedParser.Tests
{
    [TestFixture, Description("Tests whether or not our TestFileLoader works properly since many other tests depend on it.")]
    public class TestFileTester
    {

        [Test, Description("Exercises the modules which load RSS test files.")]
        public void CanFindRssFileSysTests()
        {
            DirectoryInfo rssDir = new DirectoryInfo(TestFileLoader.ValidFileSysRssTestDir);
            Assert.That(rssDir.Exists, string.Format("Rss test file directory [{0}] not found",TestFileLoader.ValidFileSysRssTestDir));

            var rssDirFiles = rssDir.GetFiles(TestFileLoader.TestFileSearchPattern);
            Assert.That(rssDirFiles.Length > 0, string.Format("No test files found in RSS test directory [{0}]",TestFileLoader.ValidFileSysRssTestDir));

            var testCases = TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.FileSys);
            Assert.That(rssDirFiles.Length == testCases.Count, string.Format("The number of files in the RSS directory [{0}] should match the number of test cases produced from them.",TestFileLoader.ValidFileSysRssTestDir));

        }

        [Test, Description("Exercises the modules which load all test files.")]
        public void CanFindAlValidFileSysTests()
        {
            var atomTestCases = TestFileLoader.LoadValidAtomTestCases(TestFileLoader.TestFileType.FileSys);
            var rssTestCases = TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.FileSys);
            var allTestCases = TestFileLoader.LoadAllValidTestCases(TestFileLoader.TestFileType.FileSys);

            Assert.That(rssTestCases.Count >= 1, "There should be at least one Rss test case.");
            Assert.That(atomTestCases.Count >= 1, "There should be at least one Atom test case.");
            Assert.That(allTestCases.Count >= 2, "There should be at least 2 test cases total.");

            Assert.That(allTestCases.Count == (rssTestCases.Count + atomTestCases.Count), "The number of test cases should be equal.");
        }

        [Test, Description("Exercises the modules which load all test files.")]
        public void CanFindAllValidHttpTests()
        {
            var atomTestCases = TestFileLoader.LoadValidAtomTestCases(TestFileLoader.TestFileType.Http);
            var rssTestCases = TestFileLoader.LoadValidRssTestCases(TestFileLoader.TestFileType.Http);
            var allTestCases = TestFileLoader.LoadAllValidTestCases(TestFileLoader.TestFileType.Http);

            Assert.That(rssTestCases.Count >= 1, "There should be at least one Rss test case.");
            Assert.That(atomTestCases.Count >= 1, "There should be at least one Atom test case.");
            Assert.That(allTestCases.Count >= 2, "There should be at least 2 test cases total.");

            Assert.That(allTestCases.Count == (rssTestCases.Count + atomTestCases.Count), "The number of test cases should be equal.");
        }

        [Test, Description("Exercises the modules which used for loading test URIs for 'missing case' tests.")]
        public void CanFindAllMissingTests()
        {
            var httpTestCases = TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.Http);
            var fileSysTestCases = TestFileLoader.LoadMissingTestCases(TestFileLoader.TestFileType.FileSys);

            Assert.That(httpTestCases.Count >= 1, "There should be at least one Http test case for missing files.");
            Assert.That(fileSysTestCases.Count >= 1, "There should be at least one FileSys test case for missing files.");
        }
    }
}