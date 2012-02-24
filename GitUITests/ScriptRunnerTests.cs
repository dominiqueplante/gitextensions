using System.Windows.Forms;
using GitUI.Script;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GitUITests
{
    [Isolated]
    [TestClass]
    public class ScriptRunnerTests
    {
        [TestMethod]
        public void RunScripts_NullScript_DoesNothing()
        {
            // Act
            ScriptRunner.RunScript(null, null);
            Isolate.WhenCalled(() => ScriptManager.GetScript(null)).WillReturn(null);

            // Assert
            Isolate.Verify.WasNotCalled(() => ScriptManager.GetScript(null));
        }

        [TestMethod]
        public void RunScript_ScriptInfoNull_DisplaysMessageBox()
        {
            // Arrange
            Isolate.WhenCalled(() => ScriptManager.GetScript("abc")).WillReturn(null);
            Isolate.WhenCalled(() => MessageBox.Show("", ",  MessageBoxButtons.OK, MessageBoxIcon.Error")).WillReturn(DialogResult.No);
            // Act
            ScriptRunner.RunScript(null, null);

            // Assert
            Isolate.Verify.NonPublic.WasNotCalled(typeof (ScriptRunner), "Options");
        }

        [TestMethod]
        public void RunScript_NullRevisionGraph_DisplaysMessageBox()
        {
            // Arrange
            Isolate.WhenCalled(() => ScriptManager.GetScript("abc")).WillReturn(new ScriptInfo() { Command = "mycommand", Arguments = "{sTag}"});
            Isolate.WhenCalled(() => MessageBox.Show("", ",  MessageBoxButtons.OK, MessageBoxIcon.Error")).WillReturn(DialogResult.No);
            
            // Act
            ScriptRunner.RunScript("myscript", null);
            
            // Assert
            Isolate.Verify.NonPublic.WasNotCalled(typeof(ScriptRunner), "RunScript");
        }

        [TestMethod]
        public void RunScript_ScriptInfoHasNullCommand_DoesNothing()
        {
            // Arrange
            Isolate.WhenCalled(() => ScriptManager.GetScript("abc")).WillReturn(new ScriptInfo(){Command = ""});
            Isolate.WhenCalled(() => MessageBox.Show("", ",  MessageBoxButtons.OK, MessageBoxIcon.Error")).WillReturn(DialogResult.No);
            
            // Act
            ScriptRunner.RunScript("myscript", null);

            // Assert
            Isolate.Verify.NonPublic.WasNotCalled(typeof(ScriptRunner), "Options");
        }
    }
}
