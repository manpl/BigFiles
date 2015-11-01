using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigFiles.Operations
{
    [Usage("/output")]
    public class OutputOperation : FileOperationBase
    {
        public OutputOperation(IFileOperation parent)
            : base(parent)
        {
        }

        public override IEnumerable<InputLine> ReadLines()
        {
            foreach (var line in this.Parent.ReadLines())
            {
                Console.WriteLine(line);
                yield return line;
            }
        }

        public override void Execute()
        {
            this.Parent.Execute();
        }
    }
}
