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
        private IFileOperation parent;

        public CountOperation(IFileOperation parent)
            : base(parent)
        {
            this.parent = parent;
        }

        public override IEnumerable<InputLine> ReadLines()
        {
           foreach(var item in Parent.ReadLines())
           {
               count++;
               yield return item;
           }
        }

        public override void Execute()
        {
            Parent.Execute();
            Console.WriteLine("Count: {0}\n", count);
        }
    }
}
