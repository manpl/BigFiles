using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigFiles.Operations
{
    internal class UsageAttribute : Attribute
    {
        public UsageAttribute(String hint)
        {
            this.Hint = hint;
        }

        public String Hint { get; set; }

        public override string ToString()
        {
            return Hint;
        }
    }
}
