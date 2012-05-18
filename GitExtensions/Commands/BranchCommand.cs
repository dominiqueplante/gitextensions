using GitUI;

namespace GitExtensions.Commands
{
    class BranchCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartCreateBranchDialog();
        }
    }
}
