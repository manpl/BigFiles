using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            foreach (var line in Parent.ReadTextChunk())
            {
                line.ReplaceContent(from, to);
                yield return line;
            }
        }
    }
}
