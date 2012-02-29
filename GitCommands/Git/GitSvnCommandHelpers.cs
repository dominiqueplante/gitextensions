using System;
using System.IO;
using System.Text;

namespace GitCommands
{
    /// <summary>
    /// git svn commands:
    /// svn clone
    /// svn fetch
    /// svn rebase
    /// svn dcommit
    /// </summary>
    public static class GitSvnCommandHelpers
    {
        private const string SvnPrefix = "svn";

        public static string CloneCmd(string fromSvn, string toPath, string authorsFile)
        {
            toPath = GitCommandHelpers.FixPath(toPath);
            StringBuilder sb = new StringBuilder();
            sb.Append(SvnPrefix);
            sb.Append(" clone ");
            sb.AppendFormat("\"{0}\"", fromSvn.Trim());
            sb.Append(' ');
            sb.AppendFormat("\"{0}\"", toPath.Trim());
            if (authorsFile != null && authorsFile.Trim().Length!=0)
            {
                sb.Append(" --authors-file ");
                sb.AppendFormat("\"{0}\"", authorsFile.Trim());
            }
            return sb.ToString();
        }

        public static bool CheckRefsRemoteSvn()
        {
            string svnremote = GetConfigSvnRemoteFetch();
            return svnremote != null && svnremote.Trim().StartsWith(":refs/remote");
        }

        public static string GetConfigSvnRemoteFetch()
        {
            return Settings.Module.RunCmd(Settings.GitCommand, "config svn-remote.svn.fetch");
        }

        public static string RebaseCmd()
        {
            return "svn rebase";
        }

        public static string DcommitCmd()
        {
            return "svn dcommit";
        }

        public static bool ValidSvnWorkingDir()
        {
            return ValidSvnWorkingDir(Settings.WorkingDir);
        }

        public static bool ValidSvnWorkingDir(string dir)
        {
            if (string.IsNullOrEmpty(dir))
                return false;

            var path = GetGitSVNPath(dir);
            if (PathExistsAsFileOrDirectory(path))
                return true;

            string directoryWithSVN = Path.Combine(dir, "svn");
            return !dir.Contains(".git") && Directory.Exists(directoryWithSVN);
        }

        public static string GetGitSVNPath(string dir)
        {
            string pathWithGit = Path.Combine(dir, ".git");
            return Path.Combine(pathWithGit, "svn");
        }

        public static bool PathExistsAsFileOrDirectory(string path)
        {
            if (Directory.Exists(path) || File.Exists(path))
                return true;
            return false;
        }
    }
}
