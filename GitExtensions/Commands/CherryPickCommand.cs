using GitUI;

namespace GitExtensions.Commands
{
    class CherryPickCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartCherryPickDialog();
        }
    }
}
