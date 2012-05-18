using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GitCommands;
using GitExtensions.Commands;
using GitUI;

namespace GitExtensions
{
    internal class CommandRunner
    {
        internal static void RunCloneCommand(string[] args)
        {
            if (args.Length > 2)
                GitUICommands.Instance.StartCloneDialog(args[2]);
            else
                GitUICommands.Instance.StartCloneDialog();
        }

        internal static void RunFileEditorCommand(string[] args)
        {
            using (var formEditor = new FormEditor(args[2]))
            {
                if (formEditor.ShowDialog() == DialogResult.Cancel)
                    Environment.ExitCode = -1;
            }
        }

        internal static void RunFileHistoryCommand(string[] args)
        {
            //Remove working dir from filename. This is to prevent filenames that are too
            //long while there is room left when the workingdir was not in the path.
            string fileHistoryFileName = args[2].Replace(Settings.WorkingDir, "").Replace('\\', '/');

            GitUICommands.Instance.StartFileHistoryDialog(fileHistoryFileName);
        }


        internal static void RunCommandBasedOnArgument(string[] args, Dictionary<string, string> arguments)
        {
            if (args[1] == "mergetool" || args[1] == "mergeconflicts")
            {
                new MergeToolCommand().Execute(arguments);
                return;
            }
            if (args[1] == "gitbash")
            {
                new BashCommand().Execute();
                return;
            }
            if (args[1] == "gitignore")
            {
                new IgnoreCommand().Execute();
                return;
            }
            if (args[1] == "remotes")
            {
                new RemotesCommand().Execute();
                return;
            }
            if (args[1] == "blame")
            {
                new BlameCommand().Execute(args);
                return;
            }
            if (args[1] == "browse")
            {
                new BrowseCommand().Execute(args);
                return;
            }
            if (args[1] == "cleanup")
            {
                new FormCleanupRepository().ShowDialog();
                return;
            }
            if (args[1] == "add" || args[1] == "addfiles")
            {
                new AddFilesCommand().Execute();
                return;
            }
            if (args[1] == "apply" || args[1] == "applypatch")
            {
                new ApplyPatchCommand().Execute();
                return;
            }
            if (args[1] == "branch")
            {
                new BranchCommand().Execute();
                return;
            }
            if (args[1] == "checkout" || args[1] == "checkoutbranch")
            {
                new CheckoutBranchCommand().Execute();
                return;
            }
            if (args[1] == "checkoutrevision")
            {
                GitUICommands.Instance.StartCheckoutRevisionDialog();
                return;
            }
            if (args[1] == "init")
            {
                new InitCommand().Execute(args);
                return;
            }
            if (args[1] == "clone")
            {
                RunCloneCommand(args);
                return;
            }
            if (args[1] == "commit")
            {
                new CommitCommand().Execute(arguments);
                return;
            }
            if (args[1] == "filehistory")
            {
                RunFileHistoryCommand(args);
                return;
            }
            if (args[1] == "fileeditor")
            {
                RunFileEditorCommand(args);
                return;
            }
            if (args[1] == "formatpatch")
            {
                GitUICommands.Instance.StartFormatPatchDialog();
                return;
            }
            if (args[1] == "pull")
            {
                new PullCommand().Execute(arguments);
                return;
            }
            if (args[1] == "push")
            {
                new PushCommand().Execute(arguments);
                return;
            }
            if (args[1] == "settings")
            {
                GitUICommands.Instance.StartSettingsDialog();
                return;
            }
            if (args[1] == "searchfile")
            {
                new SearchFileCommand().Execute();
                return;
            }
            if (args[1] == "viewdiff")
            {
                GitUICommands.Instance.StartCompareRevisionsDialog();
                return;
            }
            if (args[1] == "rebase")
            {
                new RebaseCommand().Execute(arguments);
                return;
            }
            if (args[1] == "merge")
            {
                new MergeCommand().Execute(arguments);
                return;
            }
            if (args[1] == "cherry")
            {
                new CherryPickCommand().Execute();                
                return;
            }
            if (args[1] == "revert")
            {
                new RevertCommand().Execute(args[2]);
                return;
            }
            if (args[1] == "tag")
            {
                new TagCommand().Execute();
                return;
            }
            if (args[1] == "about")
            {
                new AboutCommand().Execute();
                return;
            }
            if (args[1] == "stash")
            {
                new StashCommand().Execute();
                return;
            }
            if (args[1] == "synchronize")
            {
                new SynchronizeCommand().Execute(arguments);
                return;
            }
            if (args[1] == "openrepo")
            {
                new OpenRepositoryCommand().Execute(args);
                return;
            }
            Application.Run(new FormCommandlineHelp());
        }

    }
}