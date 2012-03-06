namespace GitCommands
{
    public class GitBlameLine
    {
        //Line
        public string CommitGuid { get; set; }
        public string FinalLineNumber { get; set; }
        public string OriginLineNumber { get; set; }
        public string LineText { get; set; }
    }
}