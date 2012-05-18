using GitUI;

namespace GitExtensions.Commands
{
    class RemotesCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartRemotesDialog();
        }
    }
}
