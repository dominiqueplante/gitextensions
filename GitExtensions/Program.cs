using System;
using System.IO;
using System.Windows.Forms;
using GitCommands;
using GitUI;
using System.Collections.Generic;

namespace GitExtensions
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            string[] args = Environment.GetCommandLineArgs();
            DoSplashWork(args);

            if (args.Length <= 1)
            {
                GitUICommands.Instance.StartBrowseDialog();
            }
            else  // if we are here args.Length > 1
            {
                RunCommand(args);
            }

            Settings.SaveSettings();
        }

        private static void DoSplashWork(string[] args)
        {
            FormSplash.Show("Load settings");
            Settings.LoadSettings();
            CheckHomePathWhenNecessary();
            //Register plugins
            FormSplash.SetAction("Load plugins");
            PluginLoader.LoadAsync();

            ChooseTranslationWhenNecessary();
            CheckSettingsWhenNecessary();
            SetWorkingDirectoryWhenNecessary(args);

            FormSplash.Hide();
        }

        private static void SetWorkingDirectoryWhenNecessary(string[] args)
        {
            if (args.Length >= 3)
            {
                if (Directory.Exists(args[2]))
                    Settings.WorkingDir = args[2];

                if (string.IsNullOrEmpty(Settings.WorkingDir))
                {
                    if (args[2].Contains(Settings.PathSeparator.ToString()))
                        Settings.WorkingDir = args[2].Substring(0, args[2].LastIndexOf(Settings.PathSeparator));
                }

                //Do not add this working dir to the recent repositories. It is a nice feature, but it
                //also increases the startup time
                //if (Settings.Module.ValidWorkingDir())
                //    Repositories.RepositoryHistory.AddMostRecentRepository(Settings.WorkingDir);
            }

            if (string.IsNullOrEmpty(Settings.WorkingDir))
            {
                string findWorkingDir = GitModule.FindGitWorkingDir(Directory.GetCurrentDirectory());
                if (GitModule.ValidWorkingDir(findWorkingDir))
                    Settings.WorkingDir = findWorkingDir;
            }
        }

        private static void CheckHomePathWhenNecessary()
        {
            if (!Settings.RunningOnWindows())
                return;
            //Quick HOME check:
            FormSplash.SetAction("Check home path");
            FormFixHome.CheckHomePath();
        }

        private static void CheckSettingsWhenNecessary()
        {
            if (!ShouldCheckSettings)
                return;
            FormSplash.SetAction("Check settings");
            using (var settings = new FormSettings())
            {
                if (settings.CheckSettings())
                    return;
                FormSettings.AutoSolveAllSettings();
                GitUICommands.Instance.StartSettingsDialog();
            }
        }

        private static void ChooseTranslationWhenNecessary()
        {
            if (!string.IsNullOrEmpty(Settings.Translation))
                return;
            using (var formChoose = new FormChooseTranslation())
            {
                formChoose.ShowDialog();
            }
        }

        private static bool ShouldCheckSettings
        {
            get
            {
                if (Application.UserAppDataRegistry == null)
                    return true;
                if (Settings.GetValue<string>("checksettings", null) == null)
                    return true;
                if (!Settings.GetValue<string>("checksettings", null).Equals("false", StringComparison.OrdinalIgnoreCase))
                    return true;
                return string.IsNullOrEmpty(Settings.GitCommand);
            }
        }

        private static void RunCommand(string[] args)
        {
            if (args.Length <= 1)
            {
                return;                
            }

            if (args[1].Equals("blame") && args.Length <= 2)
            {
                MessageBox.Show("Cannot open blame, there is no file selected.", "Blame");
                return;
            }

            if (args[1].Equals("filehistory") && args.Length <= 2)
            {
                MessageBox.Show("Cannot open file history, there is no file selected.", "File history");
                return;
            }
            
            if (args[1].Equals("fileeditor") && args.Length <= 2)
            {
                MessageBox.Show("Cannot open file editor, there is no file selected.", "File editor");
                return;
            }

            var arguments = InitializeArguments(args);
            CommandRunner.RunCommandBasedOnArgument(args, arguments);
        }

        private static Dictionary<string, string> InitializeArguments(string[] args)
        {
            var arguments = new Dictionary<string, string>();

            for (int i = 2; i < args.Length; i++)
            {
                if (args[i].StartsWith("--") && i + 1 < args.Length && !args[i + 1].StartsWith("--"))
                    arguments.Add(args[i].TrimStart('-'), args[++i]);
                else if (args[i].StartsWith("--"))
                    arguments.Add(args[i].TrimStart('-'), null);
            }
            return arguments;
        }
    }
}