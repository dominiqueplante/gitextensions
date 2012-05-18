using GitUI;

namespace GitExtensions.Commands
{
    class RevertCommand
    {
        internal void Execute(string p)
        {
            System.Windows.Forms.Application.Run(new FormRevert(p));
        }
    }
}
