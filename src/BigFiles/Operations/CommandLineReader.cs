using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigFiles.Operations
{
    public class CommandLineReader : IOperation
    {
        public IEnumerable<InputLine> ReadLines()
        {
            string line = null;
            int no = 0;

            while ((line = Console.ReadLine()) != null)
            {
                yield return new InputLine { Content = line, OrgLineNumber = no};
            }
        }
    }
}
