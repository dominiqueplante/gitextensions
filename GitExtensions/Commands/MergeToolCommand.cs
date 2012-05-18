using System.Collections.Generic;
using GitCommands;
using GitUI;

namespace GitExtensions.Commands
{
    class MergeToolCommand
    {
        internal void Execute(Dictionary<string, string> arguments)
        {
            if (!arguments.ContainsKey("quiet") || Settings.Module.InTheMiddleOfConflictedMerge())
                GitUICommands.Instance.StartResolveConflictsDialog();
        }
    }
}