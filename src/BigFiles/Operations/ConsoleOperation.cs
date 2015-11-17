using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigFiles.Operations
{
    [Usage("/ConsoleOutput")]
    public class ConsoleOperation : FileOperationBase
    {
        public ConsoleOperation(IOperation parent):base(parent)
        {
        }

        public override IEnumerable<InputLine> ReadLines()
        {
            foreach (var line in Parent.ReadLines())
            {
                Console.WriteLine(line.Content);
                yield return line;
            }
        }
    }
}
