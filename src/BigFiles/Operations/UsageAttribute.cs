using System;

namespace BigFiles.Operations
{
    internal class UsageAttribute : Attribute
    {
        public String Hint { get; }

        public UsageAttribute(String hint)
        {
            this.Hint = hint;
        }

        public override string ToString() => $"Usage[{Hint}]";
    }
}
