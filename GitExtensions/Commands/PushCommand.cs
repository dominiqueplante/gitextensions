using System.Collections.Generic;
using GitUI;

namespace GitExtensions.Commands
{
    class PushCommand
    {
        internal void Execute(Dictionary<string, string> arguments)
        {
            GitUICommands.Instance.StartPushDialog(arguments.ContainsKey("quiet"));
        }
    }
}
