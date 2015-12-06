using BigFiles.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigFiles.Tests.Operations
{
    public class InMemoryReader : IOperation
    {
        private IList<String> Lines = new List<String>();

        public InMemoryReader AddLine(String content)
        {
            Lines.Add(content);
            return this;
        }

        public IEnumerable<InputLine> ReadTextChunk()
        {
            int i=0;

            foreach (var line in Lines)
            {
                yield return new InputLine() { Content = line, OrgLineNumber = i++ };
            }
        }
    }
}
