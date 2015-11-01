using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BigFiles.Operations
{
    [Usage("/filter <regex>")]
    public class FilterOperation : FileOperationBase
    {
        private Regex filter;

        public FilterOperation(IFileOperation parent, string filterText) : base(parent)
        {
            this.filter = new Regex(filterText, RegexOptions.Singleline);
        }

        public override IEnumerable<InputLine> ReadLines()
        {
            foreach (var line in Parent.ReadLines())
            {
                if(filter.IsMatch(line.Content))
                {
                    yield return line;
                }
            }
        }

        public override void Execute()
        {
            Parent.Execute();
        }
    }
}
