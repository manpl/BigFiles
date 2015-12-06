using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigFiles.Operations
{
    [Usage("/count")]
    public class CountOperation : FileOperationBase
    {
        private int count = 0;
        private IOperation parent;

        public CountOperation(IOperation parent)
            : base(parent)
        {
            this.parent = parent;
        }

        public override IEnumerable<InputLine> ReadTextChunk()
        {
           foreach(var item in Parent.ReadTextChunk())
           {
               count++;
               yield return item;
           }

           Console.WriteLine("Count: {0}\n", count);
        }
    }
}
