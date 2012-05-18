using System.IO;
using GitCommands;
using GitUI;

namespace GitExtensions.Commands
{
    class OpenRepositoryCommand
    {
        internal void Execute(string[] args)
        {
            if (args.Length > 2 && File.Exists(args[2]))
            {
                string path = File.ReadAllText(args[2]);
                if (Directory.Exists(path))
                {
                    Settings.WorkingDir = path;
                }
            }

            GitUICommands.Instance.StartBrowseDialog();
        }
    }
}
