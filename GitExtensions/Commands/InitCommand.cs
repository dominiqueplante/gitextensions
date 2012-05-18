using GitUI;

namespace GitExtensions.Commands
{
    class InitCommand
    {
        internal void Execute(string[] args)
        {
            if (args.Length > 2)
                GitUICommands.Instance.StartInitializeDialog(args[2]);
            else
                GitUICommands.Instance.StartInitializeDialog();
        }
    }
}