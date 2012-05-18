using GitCommands;
using GitUI;

namespace GitExtensions.Commands
{
    class BlameCommand
    {
        internal void Execute(string[] args)
        {
            // Remove working dir from filename. This is to prevent filenames that are too
            // long while there is room left when the workingdir was not in the path.
            string filenameFromBlame = args[2].Replace(Settings.WorkingDir, "").Replace('\\', '/');
            GitUICommands.Instance.StartBlameDialog(filenameFromBlame);
        }
    }
}
