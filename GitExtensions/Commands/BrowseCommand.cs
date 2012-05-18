using System;
using System.Linq;
using GitUI;

namespace GitExtensions.Commands
{
    class BrowseCommand
    {
        internal string GetParameterOrEmptyStringAsDefault(string[] args, string paramName)
        {
            if (args.Any(arg => arg.StartsWith(paramName + "=")))
            {
                return args[2].Replace(paramName + "=", "");
            }

            return String.Empty;
        }
        internal void Execute(string[] args)
        {
            GitUICommands.Instance.StartBrowseDialog(GetParameterOrEmptyStringAsDefault(args, "-filter"));
        }
    }
}
