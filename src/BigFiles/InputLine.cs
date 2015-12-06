using System.Text.RegularExpressions;
namespace BigFiles
{
    public class InputLine
    {
        public string Content { get; set; }
        public int OrgLineNumber { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
