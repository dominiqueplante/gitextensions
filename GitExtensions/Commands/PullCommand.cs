using System.Collections.Generic;
using GitCommands;
using GitUI;

namespace GitExtensions.Commands
{
    class PullCommand
    {
        internal void UpdateSettingsBasedOnArguments(Dictionary<string, string> arguments)
        {
            if (arguments.ContainsKey("merge"))
                Settings.PullMerge = "merge";
            if (arguments.ContainsKey("rebase"))
                Settings.PullMerge = "rebase";
            if (arguments.ContainsKey("fetch"))
                Settings.PullMerge = "fetch";
            if (arguments.ContainsKey("autostash"))
                Settings.AutoStash = true;
        }

        internal void Execute(Dictionary<string, string> arguments)
        {
            UpdateSettingsBasedOnArguments(arguments);
            GitUICommands.Instance.StartPullDialog(arguments.ContainsKey("quiet"));
        }
    }
}
