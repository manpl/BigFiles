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

        public override IEnumerable<InputLine> ReadTextChunk()
        {
            foreach (var line in Parent.ReadTextChunk())
            {
                Console.WriteLine(line.Content);
                yield return line;
            }
        }
    }
}
