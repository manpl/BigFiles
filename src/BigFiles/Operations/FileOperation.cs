using System;
using System.Collections.Generic;
namespace BigFiles.Operations
{

    public interface IFileOperation
    {
        IEnumerable<InputLine> ReadLines();
        void Execute();
    }

    [Usage("/xx")]
    public abstract class FileOperationBase : IFileOperation
    {
        private IFileOperation parent;
        protected IFileOperation Parent
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

        public FileOperationBase(IFileOperation parent)
        {
            Parent = parent;
        }

        public abstract IEnumerable<InputLine> ReadLines();
        public abstract void Execute();
    }
}
