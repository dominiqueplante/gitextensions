using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GitCommands;
using GitUI;

namespace GitExtensions
{
    internal class CommandRunner
    {
        internal static void RunMergeToolOrConflictCommand(Dictionary<string, string> arguments)
        {
            if (!arguments.ContainsKey("quiet") || Settings.Module.InTheMiddleOfConflictedMerge())
                GitUICommands.Instance.StartResolveConflictsDialog();
        }

        internal static void RunBlameCommand(string[] args)
        {
            // Remove working dir from filename. This is to prevent filenames that are too
            // long while there is room left when the workingdir was not in the path.
            string filenameFromBlame = args[2].Replace(Settings.WorkingDir, "").Replace('\\', '/');
            GitUICommands.Instance.StartBlameDialog(filenameFromBlame);
        }

        internal static void RunInitCommand(string[] args)
        {
            if (args.Length > 2)
                GitUICommands.Instance.StartInitializeDialog(args[2]);
            else
                GitUICommands.Instance.StartInitializeDialog();
        }

        internal static void RunCloneCommand(string[] args)
        {
            if (args.Length > 2)
                GitUICommands.Instance.StartCloneDialog(args[2]);
            else
                GitUICommands.Instance.StartCloneDialog();
        }

        internal static void Commit(Dictionary<string, string> arguments)
        {
            GitUICommands.Instance.StartCommitDialog(arguments.ContainsKey("quiet"));
        }

        internal static void RunOpenRepoCommand(string[] args)
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

        internal static void Push(Dictionary<string, string> arguments)
        {
            GitUICommands.Instance.StartPushDialog(arguments.ContainsKey("quiet"));
        }

        internal static void RunMergeCommand(Dictionary<string, string> arguments)
        {
            string branch = null;
            if (arguments.ContainsKey("branch"))
                branch = arguments["branch"];
            GitUICommands.Instance.StartMergeBranchDialog(branch);
        }

        internal static void Pull(Dictionary<string, string> arguments)
        {
            UpdateSettingsBasedOnArguments(arguments);
            GitUICommands.Instance.StartPullDialog(arguments.ContainsKey("quiet"));
        }

        internal static void UpdateSettingsBasedOnArguments(Dictionary<string, string> arguments)
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

        internal static void RunSynchronizeCommand(Dictionary<string, string> arguments)
        {
            Commit(arguments);
            Pull(arguments);
            Push(arguments);
        }

        internal static void RunRebaseCommand(Dictionary<string, string> arguments)
        {
            string branch = null;
            if (arguments.ContainsKey("branch"))
                branch = arguments["branch"];
            GitUICommands.Instance.StartRebaseDialog(branch);
        }

        internal static void RunSearchFileCommand()
        {
            var searchWindow = new SearchWindow<string>(FindFileMatches);
            Application.Run(searchWindow);
            Console.WriteLine(Settings.WorkingDir + searchWindow.SelectedItem);
        }

        internal static IList<string> FindFileMatches(string name)
        {
            var candidates = Settings.Module.GetFullTree("HEAD");

            string nameAsLower = name.ToLower();

            return candidates.Where(fileName => fileName.ToLower().Contains(nameAsLower)).ToList();
        }

        internal static void RunCommandBasedOnArgument(string[] args, Dictionary<string, string> arguments)
        {
            if (args[1] == "mergetool" || args[1] == "mergeconflicts")
            {
                RunMergeToolOrConflictCommand(arguments);
                return;
            }
            if (args[1] == "gitbash")
            {
                Settings.Module.RunBash();
                return;
            }
            if (args[1] == "gitignore")
            {
                GitUICommands.Instance.StartEditGitIgnoreDialog();
                return;
            }
            if (args[1] == "remotes")
            {
                GitUICommands.Instance.StartRemotesDialog();
                return;
            }
            if (args[1] == "blame")
            {
                RunBlameCommand(args);
                return;
            }
            if (args[1] == "browse")
            {
                GitUICommands.Instance.StartBrowseDialog(GetParameterOrEmptyStringAsDefault(args, "-filter"));
                return;
            }
            if (args[1] == "cleanup")
            {
                new FormCleanupRepository().ShowDialog();
                return;
            }
            if (args[1] == "add" || args[1] == "addfiles")
            {
                GitUICommands.Instance.StartAddFilesDialog();
                return;
            }
            if (args[1] == "apply" || args[1] == "applypatch")
            {
                GitUICommands.Instance.StartApplyPatchDialog();
                return;
            }
            if (args[1] == "branch")
            {
                GitUICommands.Instance.StartCreateBranchDialog();
                return;
            }
            if (args[1] == "checkout" || args[1] == "checkoutbranch")
            {
                GitUICommands.Instance.StartCheckoutBranchDialog();
                return;
            }
            if (args[1] == "checkoutrevision")
            {
                GitUICommands.Instance.StartCheckoutRevisionDialog();
                return;
            }
            if (args[1] == "init")
            {
                RunInitCommand(args);
                return;
            }
            if (args[1] == "clone")
            {
                RunCloneCommand(args);
                return;
            }
            if (args[1] == "commit")
            {
                Commit(arguments);
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
                Pull(arguments);
                return;
            }
            if (args[1] == "push")
            {
                Push(arguments);
                return;
            }
            if (args[1] == "settings")
            {
                GitUICommands.Instance.StartSettingsDialog();
                return;
            }
            if (args[1] == "searchfile")
            {
                RunSearchFileCommand();
                return;
            }
            if (args[1] == "viewdiff")
            {
                GitUICommands.Instance.StartCompareRevisionsDialog();
                return;
            }
            if (args[1] == "rebase")
            {
                RunRebaseCommand(arguments);
                return;
            }
            if (args[1] == "merge")
            {
                RunMergeCommand(arguments);
                return;
            }
            if (args[1] == "cherry")
            {
                GitUICommands.Instance.StartCherryPickDialog();
                return;
            }
            if (args[1] == "revert")
            {
                Application.Run(new FormRevert(args[2]));
                return;
            }
            if (args[1] == "tag")
            {
                GitUICommands.Instance.StartCreateTagDialog();
                return;
            }
            if (args[1] == "about")
            {
                Application.Run(new AboutBox());
                return;
            }
            if (args[1] == "stash")
            {
                GitUICommands.Instance.StartStashDialog();
                return;
            }
            if (args[1] == "synchronize")
            {
                RunSynchronizeCommand(arguments);
                return;
            }
            if (args[1] == "openrepo")
            {
                RunOpenRepoCommand(args);
                return;
            }
            Application.Run(new FormCommandlineHelp());
        }

        internal static string GetParameterOrEmptyStringAsDefault(string[] args, string paramName)
        {
            if (args.Any(arg => arg.StartsWith(paramName + "=")))
            {
                return args[2].Replace(paramName + "=", "");
            }

            return String.Empty;
        }
    }
}