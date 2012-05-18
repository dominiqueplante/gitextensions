using GitUI;

namespace GitExtensions.Commands
{
    class IgnoreCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartEditGitIgnoreDialog();
        }
    }
}
