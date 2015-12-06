using BigFiles.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BigFiles.Tests.Operations
{
    public class ReplaceOperationTests
    {
        IOperation operation;

        [Fact]
        public void SingleOccurance_ReadTextChunk_ReplacesOne()
        {
            IOperation baseOperation = new InMemoryReader()
                .AddLine("from");

            operation = new ReplaceOperation(baseOperation, "from", "to");

            var result = operation.ReadTextChunk().First();
            Assert.Equal("to", result.Content);
        }

        [Fact]
        public void DoubleOccurance_ReadTextChunk_ReplacesBoth()
        {
            IOperation baseOperation = new InMemoryReader()
                .AddLine("from from");

            operation = new ReplaceOperation(baseOperation, "from", "to");

            var result = operation.ReadTextChunk().First();
            Assert.Equal("to to", result.Content);
        }

        [Fact]
        public void SingleOccuranceWithRegex_ReadTextChunk_Replaces()
        {
            IOperation baseOperation = new InMemoryReader()
                .AddLine("from");

            operation = new ReplaceOperation(baseOperation, "fr.m", "to");

            var result = operation.ReadTextChunk().First();
            Assert.Equal("to", result.Content);
        }

        [Fact]
        public void DoubleOccuranceWithRegex_ReadTextChunk_Replaces()
        {
            IOperation baseOperation = new InMemoryReader()
                .AddLine("from from");

            operation = new ReplaceOperation(baseOperation, "fr.m", "to");

            var result = operation.ReadTextChunk().First();
            Assert.Equal("to to", result.Content);
        }
    }
}
