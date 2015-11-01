namespace BigFiles
{
    public class InputLine
    {
        public string Content { get; set; }
        public int OrgLineNumber { get; set; }

        public void ReplaceContent(string from, string to)
        {
            this.Content = this.Content.Replace(from, to);
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
