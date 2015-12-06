using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BigFiles.Operations
{
    [Usage("/replace <from> <to>")]
    public class ReplaceOperation : FileOperationBase
    {
        private string from;
        private string to;

        public ReplaceOperation(IOperation operation, string from, string to)
            : base(operation)
        {
            this.from = from;
            this.to = to;
        }

        public override IEnumerable<InputLine> ReadTextChunk()
        {
            Regex regex = new Regex(from, RegexOptions.Compiled);

            foreach (var line in Parent.ReadTextChunk())
            {
                line.Content = regex.Replace(line.Content, to);
                yield return line;
            }
        }
    }
}
