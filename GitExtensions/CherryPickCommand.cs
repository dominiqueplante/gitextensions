using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitUI;

namespace GitExtensions
{
    class CherryPickCommand
    {
        public void Execute()
        {
            GitUICommands.Instance.StartCherryPickDialog();
        }
    }
}
