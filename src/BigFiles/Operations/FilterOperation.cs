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

        public FilterOperation(IOperation parent, string filterText) : base(parent)
        {
            this.filter = new Regex(filterText, RegexOptions.Singleline);
        }

        public override IEnumerable<InputLine> ReadTextChunk()
        {
            foreach (var line in Parent.ReadTextChunk())
            {
                if(filter.IsMatch(line.Content))
                {
                    yield return line;
                }
            }
        }
    }
}
