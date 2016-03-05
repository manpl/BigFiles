using System.Collections.Generic;
using System.Linq;

namespace BigFiles.Operations
{
    [Usage("/Skip")]
    public class SkipOperation : FileOperationBase
    {
        public int NoToSkip { get; private set; }
        public SkipOperation(IOperation parent, string noToSkip) : base(parent) {
            this.NoToSkip = int.Parse(noToSkip);
        }

        public override IEnumerable<InputLine> ReadTextChunk()
        {
            return this.Parent.ReadTextChunk().Skip(NoToSkip);
        }
    }
}
