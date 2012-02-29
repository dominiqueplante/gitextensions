using GitCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GitCommandsIsolatedTests
{
    [TestClass]
    [Isolated]
    public class GitSvnCommandHelpersIsolatedTests
    {
        [TestMethod]
        public void ValidSvnWorkingDir_ValidDir_ReturnsOk()
        {
            // Arrange
            Isolate.WhenCalled(() => GitSvnCommandHelpers.PathExistsAsFileOrDirectory("")).WillReturn(true);

            // Act
            var res = GitSvnCommandHelpers.ValidSvnWorkingDir("mydir");

            // Assert
            Assert.IsTrue(res);
        }
    }
}
