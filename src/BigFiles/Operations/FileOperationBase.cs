using System;
using System.Collections.Generic;
namespace BigFiles.Operations
{

    public interface IOperation
    {
        IEnumerable<InputLine> ReadLines();
        void Execute();
    }

    [Usage("/xx")]
    public abstract class FileOperationBase : IOperation
    {
        private IOperation parent;
        protected IOperation Parent
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
        public abstract void Execute();
    }
}
