using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitCommands;

namespace GitCommandsTests
{
    /// <summary>

    /// </summary>
    [TestClass]
    public class FindValidworkingDirTest
    {
        private string GetCurrentDir()
        {
            string path = typeof(FindValidworkingDirTest).Assembly.Location;

            return path.Substring(0, path.LastIndexOf('\\'));
        }

        [TestMethod]
        public void TestWorkingDir_EndsWithFolder_Ok()
        {
            // Act
            Settings.WorkingDir = GetCurrentDir();

            // Assert
            CheckWorkingDir();            
        }

        [TestMethod]
        public void TestWorkingDir_EndsWithFile_Ok()
        {
            // Act
            Settings.WorkingDir = GetCurrentDir() + "\\testfile.txt";

            // Assert
            CheckWorkingDir();
        }

        [TestMethod]
        public void TestWorkingDir_WindowsWithPathSeparator_Ok()
        {
            // Act
            Settings.WorkingDir = GetCurrentDir() + "\\";

            // Assert
            CheckWorkingDir();            
        }

        [TestMethod]
        public void TestWorkingDir_WindowsWithDoublePathSeparator_Ok()
        {
            // Act
            Settings.WorkingDir = GetCurrentDir() + "\\\\";

            // Assert
            CheckWorkingDir();
        }

        [TestMethod]
        public void TestWorkingDir_EndsWithMoreNestedFolder_Ok()
        {
            // Act
            Settings.WorkingDir = GetCurrentDir() + "\\test\\test\\tralala";

            // Assert
            CheckWorkingDir();
        }

        private static void CheckWorkingDir()
        {
            //Should not contain double slashes -> \\
            Assert.IsFalse(Settings.WorkingDir.Contains("\\\\"));

            //Should end with slash
            Assert.IsTrue(Settings.WorkingDir.EndsWith("\\"));
        }
    }
}
