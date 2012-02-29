using GitCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitCommandsTests
{
    [TestClass]
    public class GitSvnCommandHelpersTests
    {
        [TestMethod]
        public void GetGitSVNPath_ValidPath_ReturnsPathWithSVNandGitInPath()
        {
            // Act
            var res = GitSvnCommandHelpers.GetGitSVNPath("abc");

            // Assert
            Assert.AreEqual("abc\\.git\\svn", res);
        }

        [TestMethod]
        public void CloneCmd_OkPathNoAuthors_Ok()
        {
            // Act
            var result = GitSvnCommandHelpers.CloneCmd("from", "to", null);

            // Assert
            Assert.AreEqual("svn clone \"from\" \"to\"", result);
        }

        [TestMethod]
        public void CloneCmd_OkPathWithAuthors_Ok()
        {
            // Act
            var result = GitSvnCommandHelpers.CloneCmd("from", "to", "myauthors");

            // Assert
            Assert.AreEqual("svn clone \"from\" \"to\" --authors-file \"myauthors\"", result);
        }
    }
}