using GitUI;

namespace GitExtensions.Commands
{
    class ApplyPatchCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartApplyPatchDialog();
        }
    }
}
