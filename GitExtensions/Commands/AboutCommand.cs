using System.Windows.Forms;
using GitUI;

namespace GitExtensions.Commands
{
    class AboutCommand
    {
        internal void Execute()
        {
            Application.Run(new AboutBox());
        }
    }
}
