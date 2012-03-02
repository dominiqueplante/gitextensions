using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatchApply;

namespace GitCommandsTests
{
    [TestClass]
    public class PatchManagerTest
    {
        [TestMethod]
        public void TestCorrectlyLoadsTheRightNumberOfDiffsInAPatchFile()
        {
            PatchManager manager = NewManager();
            var testPatch = Encoding.UTF8.GetString(TestResource.TestPatch);
            manager.LoadPatch(testPatch, false);

            Assert.AreEqual(12, manager.Patches.Count);
        }

        [TestMethod]
        public void GetMD5Hash_AString_ReturnsMD5ForString()
        {
            var pm = new PatchManager();
            var res = pm.GetMD5Hash("hello world");
            Assert.AreEqual("5eb63bbbe01eeed093cb22bb8f5acdc3", res);
        }
        [TestMethod]
        public void GetSelectedLinesAsPatch_Empty_returnsNull()
        {
            // Act
            var res = PatchManager.GetSelectedLinesAsPatch(null, 0, 0, true);

            // Assert
            Assert.IsNull(res);
        }

        [TestMethod]
        public void GetSelectedLinesAsPatch_BogusLines_returnsPath()
        {
            var result = PatchManager.GetSelectedLinesAsPatch("abcde@@ -1,1, +2,3 @@ foo bar\nfghijklmnopqr-stuvw\nxyz", 7, 15, true);
            Assert.AreEqual("abcde@@ -1,1 +1,1 @@\nfghijklmnopqr-stuvw\nxyz", result);
        }

        //\n\\ No newline at end of file\n
        [TestMethod]
        public void GetSelectedLinesAsPatch_BogusLinesWithNoNewlineAtEndOfFile_returnsPath()
        {
            var result = PatchManager.GetSelectedLinesAsPatch("abcde@@ -1,1, +2,3 @@ foo bar\nfghijklmnopqr-stuvw\nxyz\n\\ No newline at end of file\n", 7, 15, true);
            Assert.AreEqual("abcde@@ -1,1 +1,1 @@\nfghijklmnopqr-stuvw\nxyz", result);
        }

        [TestMethod]
        public void TestCorrectlyLoadsTheRightFilenamesInAPatchFile()
        {
            PatchManager manager = NewManager();
            var testPatch = Encoding.UTF8.GetString(TestResource.TestPatch);
            manager.LoadPatch(testPatch, false);

            Assert.AreEqual(12, manager.Patches.Select(p => p.FileNameA).Distinct().Count());
            Assert.AreEqual(12, manager.Patches.Select(p => p.FileNameB).Distinct().Count());
        }

        [TestMethod]
        public void TestCorrectlyLoadsOneBinaryPatch()
        {
            PatchManager manager = NewManager();
            var testPatch = Encoding.UTF8.GetString(TestResource.TestPatch);
            manager.LoadPatch(testPatch, false);
            
            Assert.AreEqual(1, manager.Patches.Count(p => p.File == Patch.FileType.Binary));
        }

        [TestMethod]
        public void TestCorrectlyLoadsOneNewFile()
        {
            PatchManager manager = NewManager();
            var testPatch = Encoding.UTF8.GetString(TestResource.TestPatch);
            manager.LoadPatch(testPatch, false);

            Assert.AreEqual(1, manager.Patches.Count(p => p.Type == Patch.PatchType.NewFile));
        }

        [TestMethod]
        public void TestCorrectlyLoadsOneDeleteFile()
        {
            PatchManager manager = NewManager();
            var testPatch = Encoding.UTF8.GetString(TestResource.TestPatch);
            manager.LoadPatch(testPatch, false);

            Assert.AreEqual(1, manager.Patches.Count(p => p.Type == Patch.PatchType.DeleteFile));
        }

        [TestMethod]
        public void TestCorrectlyLoadsTenChangeFiles()
        {
            PatchManager manager = NewManager();
            var testPatch = Encoding.UTF8.GetString(TestResource.TestPatch);
            manager.LoadPatch(testPatch, false);

            Assert.AreEqual(10, manager.Patches.Count(p => p.Type == Patch.PatchType.ChangeFile));
        }

        private static PatchManager NewManager()
        {
            return new PatchManager();
        }
    }
}