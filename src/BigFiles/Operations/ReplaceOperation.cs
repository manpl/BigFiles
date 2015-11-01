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

        public ReplaceOperation(IFileOperation operation, string from, string to)
            : base(operation)
        {
            this.from = from;
            this.to = to;
        }

        public override IEnumerable<InputLine> ReadLines()
        {
            foreach (var line in Parent.ReadLines())
            {
                line.ReplaceContent(from, to);
                yield return line;
            }
        }

        public override void Execute()
        {
        }
    }
}
