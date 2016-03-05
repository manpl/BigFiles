using System;
using System.Collections.Generic;
using System.Linq;

namespace BigFiles.Operations
{
    [Usage("/Take")]
    public class TakeOperation : FileOperationBase
    {
        public int NoToTake { get; private set; }

        public TakeOperation(IOperation parent, String noToTake) : base(parent) {
            this.NoToTake = int.Parse(noToTake);   
        }

        public override IEnumerable<InputLine> ReadTextChunk()
        {
            return this.Parent.ReadTextChunk().Take(NoToTake);
        }
    }
}
