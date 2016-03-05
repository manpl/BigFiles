namespace BigFiles
{
    public class InputLine
    {
        public string Content { get; }
        public int OrgLineNumber { get; }
        public InputLine(string content, int lineNumber)
        {
            Content = content;
            OrgLineNumber = lineNumber;
        }

        public override string ToString() => Content;
    }
}
