using GitUI;

namespace GitExtensions.Commands
{
    class StashCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartStashDialog();
        }
    }
}
