using System.Collections.Generic;
using GitUI;

namespace GitExtensions.Commands
{
    class CommitCommand
    {
        internal void Execute(Dictionary<string, string> arguments)
        {
            GitUICommands.Instance.StartCommitDialog(arguments.ContainsKey("quiet"));
        }
    }
}
