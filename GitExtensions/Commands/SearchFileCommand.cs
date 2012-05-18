using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GitCommands;
using GitUI;

namespace GitExtensions.Commands
{
    internal class SearchFileCommand
    {
        public void Execute()
        {
            var searchWindow = new SearchWindow<string>(FindFileMatches);
            Application.Run(searchWindow);
            Console.WriteLine(Settings.WorkingDir + searchWindow.SelectedItem);
        }
        internal  IList<string> FindFileMatches(string name)
        {
            var candidates = Settings.Module.GetFullTree("HEAD");

            string nameAsLower = name.ToLower();

            return candidates.Where(fileName => fileName.ToLower().Contains(nameAsLower)).ToList();
        }
    }
}