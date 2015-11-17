using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigFiles.Operations
{
    [Usage("/Take")]
    public class TakeOperation : FileOperationBase
    {
        public int NoToTake { get; private set; }

        public TakeOperation(IOperation parent, String noToTake) : base(parent) {
            this.NoToTake = int.Parse(noToTake);   
        }

        public override IEnumerable<InputLine> ReadLines()
        {
            return this.Parent.ReadLines().Take(NoToTake);
        }
    }
}
