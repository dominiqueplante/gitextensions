using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GitCommands;

namespace GitUI.Blame
{
    public sealed partial class BlameControl : GitExtensionsControl
    {
        private GitBlame Blame { get; set; }

        private string _lastRevision;
        private RevisionGrid _revGrid;
        int lastTooltipX = -100;
        int lastTooltipY = -100;
        string lastTooltip = "";
        GitBlameHeader lastBlameHeader;
        bool bChangeScrollPosition;

        public BlameControl()
        {
            InitializeComponent();
            Translate();

            BlameCommitter.IsReadOnly = true;
            BlameCommitter.EnableScrollBars(false);
            BlameCommitter.ShowLineNumbers = false;
            BlameCommitter.DisableFocusControlOnHover = true;
            BlameCommitter.ScrollPosChanged += BlameCommitter_ScrollPosChanged;
            BlameCommitter.MouseMove += BlameCommitter_MouseMove;
            BlameCommitter.MouseLeave += BlameCommitter_MouseLeave;

            BlameFile.IsReadOnly = true;
            BlameFile.ScrollPosChanged += BlameFile_ScrollPosChanged;
            BlameFile.SelectedLineChanged += BlameFile_SelectedLineChanged;
            BlameFile.RequestDiffView += ActiveTextAreaControlDoubleClick;
            BlameFile.MouseMove += BlameFile_MouseMove;
        }

        #region BlameCommitter Event Handlers

        void BlameCommitter_MouseLeave(object sender, EventArgs e)
        {
            blameTooltip.Hide(this);
        }

        void BlameCommitter_MouseMove(object sender, MouseEventArgs e)
        {
            if (!BlameFile.Focused)
                BlameFile.Focus();

            if (Blame == null)
                return;

            int line = BlameCommitter.GetLineFromVisualPosY(e.Y);

            if (line >= Blame.Lines.Count)
                return;

            GitBlameHeader blameHeader = Blame.FindHeaderForCommitGuid(Blame.Lines[line].CommitGuid);

            string tooltipText = blameHeader.ToString();

            int newTooltipX = splitContainer2.SplitterDistance + 60;
            int newTooltipY = e.Y + splitContainer1.SplitterDistance + 20;

            if (lastTooltip == tooltipText && Math.Abs(lastTooltipX - newTooltipX) <= 5 && Math.Abs(lastTooltipY - newTooltipY) <= 5)
                return;
            lastTooltip = tooltipText;
            lastTooltipX = newTooltipX;
            lastTooltipY = newTooltipY;
            blameTooltip.Show(tooltipText, this, newTooltipX, newTooltipY);
        }

        void BlameCommitter_ScrollPosChanged(object sender, EventArgs e)
        {
            if (!bChangeScrollPosition)
            {
                bChangeScrollPosition = true;
                SyncBlameFileView();
                bChangeScrollPosition = false;
            }
            Rectangle rect = BlameCommitter.ClientRectangle;
            rect = BlameCommitter.RectangleToScreen(rect);
            if (!rect.Contains(MousePosition))
                return;
            Point p = BlameCommitter.PointToClient(MousePosition);
            var me = new MouseEventArgs(0, 0, p.X, p.Y, 0);
            BlameCommitter_MouseMove(null, me);
        }
        #endregion

        #region BlameFile Event Handlers
        void BlameFile_MouseMove(object sender, MouseEventArgs e)
        {
            if (Blame == null)
                return;

            int line = BlameFile.GetLineFromVisualPosY(e.Y);

            if (line >= Blame.Lines.Count)
                return;

            GitBlameHeader blameHeader = Blame.FindHeaderForCommitGuid(Blame.Lines[line].CommitGuid);

            if (blameHeader == lastBlameHeader)
                return;
            BlameCommitter.ClearHighlighting();
            BlameFile.ClearHighlighting();
            for (int i = 0; i < Blame.Lines.Count; i++)
            {
                if (Blame.Lines[i].CommitGuid != blameHeader.CommitGuid)
                    continue;
                Color blameColor = Color.FromArgb(225, 225, 225);
                BlameCommitter.HighlightLine(i, blameColor);
                BlameFile.HighlightLine(i, blameColor);
            }
            BlameCommitter.Refresh();
            BlameFile.Refresh();
            lastBlameHeader = blameHeader;
        }

        void BlameFile_SelectedLineChanged(object sender, int selectedLine)
        {
            if (selectedLine >= Blame.Lines.Count)
                return;

            var newRevision = Blame.Lines[selectedLine].CommitGuid;

            if (_lastRevision == newRevision)
                return;

            _lastRevision = newRevision;
            commitInfo.SetRevision(_lastRevision);
        }

        void BlameFile_ScrollPosChanged(object sender, EventArgs e)
        {
            if (bChangeScrollPosition)
                return;
            bChangeScrollPosition = true;
            SyncBlameCommitterView();
            bChangeScrollPosition = false;
        }

        private void ActiveTextAreaControlDoubleClick(object sender, EventArgs e)
        {
            var gitRevision = new GitRevision(_lastRevision) { ParentGuids = new[] { _lastRevision + "^" } };
            if (_revGrid != null)
            {
                _revGrid.SetSelectedRevision(gitRevision);
            }
            else
            {
                var frm = new FormDiffSmall(gitRevision);
                frm.ShowDialog(this);
            }
        }
        #endregion

        #region Control Synch

        private void SyncBlameFileView()
        {
            BlameFile.ScrollPos = BlameCommitter.ScrollPos;
        }

        private void SyncBlameCommitterView()
        {
            BlameCommitter.ScrollPos = BlameFile.ScrollPos;
        }

        #endregion

        public void LoadBlame(string guid, string fileName, RevisionGrid revGrid)
        {
            var scrollpos = BlameFile.ScrollPos;

            _revGrid = revGrid;
            Blame = Settings.Module.Blame(fileName, guid);

            LoadBlame_SetupBlameCommitter();
            LoadBlame_SetupBlameFile(fileName, scrollpos);
            commitInfo.SetRevision(guid);
        }

        internal void LoadBlame_SetupBlameFile(string fileName, int scrollpos)
        {
            BlameFile.ViewText(fileName, Blame.GetBlameFileForBlameControl());
            BlameFile.ScrollPos = scrollpos;
        }

        internal void LoadBlame_SetupBlameCommitter()
        {
            BlameCommitter.ViewText("committer.txt", Blame.GetCommitersForBlameControl());
        }
    }
}
