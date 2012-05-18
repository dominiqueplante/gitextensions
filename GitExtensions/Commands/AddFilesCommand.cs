using GitUI;

namespace GitExtensions.Commands
{
    class AddFilesCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartAddFilesDialog();
        }
    }
}
