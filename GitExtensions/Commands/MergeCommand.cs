using System.Collections.Generic;
using GitUI;

namespace GitExtensions.Commands
{
    class MergeCommand
    {
        internal void Execute(Dictionary<string, string> arguments)
        {
            string branch = null;
            if (arguments.ContainsKey("branch"))
                branch = arguments["branch"];
            GitUICommands.Instance.StartMergeBranchDialog(branch);
        }
    }
}