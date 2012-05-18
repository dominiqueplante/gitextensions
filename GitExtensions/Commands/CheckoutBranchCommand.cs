using GitUI;

namespace GitExtensions.Commands
{
    class CheckoutBranchCommand
    {
        internal void Execute()
        {
            GitUICommands.Instance.StartCheckoutBranchDialog();
        }
    }
}
