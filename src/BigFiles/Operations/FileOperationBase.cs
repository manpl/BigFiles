using System;
using System.Collections.Generic;
namespace BigFiles.Operations
{

    public interface IOperation
    {
        IEnumerable<InputLine> ReadLines();
    }

    [Usage("/xx")]
    public abstract class FileOperationBase : IOperation
    {
        private IOperation parent;
        protected internal IOperation Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Parent");
                }

                this.parent = value;
            }
        }

        public FileOperationBase(IOperation parent)
        {
            Parent = parent;
        }

        public abstract IEnumerable<InputLine> ReadLines();
    }
}
