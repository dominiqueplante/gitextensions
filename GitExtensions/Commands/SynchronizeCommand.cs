using System.Collections.Generic;

namespace GitExtensions.Commands
{
    class SynchronizeCommand
    {
        internal void Execute(Dictionary<string, string> arguments)
        {
            new CommitCommand().Execute(arguments);
            new PullCommand().Execute(arguments);
            new PushCommand().Execute(arguments);
        }
    }
}
