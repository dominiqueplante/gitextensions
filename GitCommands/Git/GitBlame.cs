using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitCommands
{
    public class GitBlame
    {
        public GitBlame()
        {
            Headers = new List<GitBlameHeader>();
            Lines = new List<GitBlameLine>();
        }
        public IList<GitBlameHeader> Headers { get; private set; }
        public IList<GitBlameLine> Lines { get; private set; }

        public GitBlameHeader FindHeaderForCommitGuid(string commitGuid)
        {
            return Headers.First(h => h.CommitGuid == commitGuid);
        }

        public string GetCommitersForBlameControl()
        {
            var blameCommitter = new StringBuilder();
            for (var i = 0; i < Lines.Count; i++)
            {
                GitBlameLine blameLine = Lines[i];

                if (i > 0 && Lines[i - 1].CommitGuid == blameLine.CommitGuid)
                {
                    blameCommitter.AppendLine(new string(' ', 200));
                }
                else
                {
                    GitBlameHeader blameHeader = FindHeaderForCommitGuid(blameLine.CommitGuid);
                    string blameHeaderInfo =
                        (blameHeader.Author + " - " + blameHeader.AuthorTime + " - " + blameHeader.FileName +
                         new string(' ', 100)).Trim(new[] { '\r', '\n' });
                    blameCommitter.AppendLine(blameHeaderInfo);
                }
            }
            return blameCommitter.ToString();
        }

        public string GetBlameFileForBlameControl()
        {
            var blameFile = new StringBuilder();
            foreach (GitBlameLine blameLine in Lines)
            {
                if (blameLine.LineText == null)
                    blameFile.AppendLine("");
                else
                    blameFile.AppendLine(blameLine.LineText.Trim(new[] { '\r', '\n' }));
            }
            return blameFile.ToString();
        }
    }
}
