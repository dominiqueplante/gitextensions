using GitUI;

namespace GitExtensions.Commands
{
    class TagCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartCreateTagDialog();
        }
    }
}
