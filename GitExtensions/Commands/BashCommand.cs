using GitCommands;

namespace GitExtensions.Commands
{
    class BashCommand
    {
        internal void Execute()
        {
            Settings.Module.RunBash();
        }
    }
}