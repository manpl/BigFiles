using BigFiles.CommandLine;
using BigFiles.Operations;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace BigFiles.Tests
{
    public class CommandLineParserTests
    {
        private CommandLineParser parser = new CommandLineParser();

        public CommandLineParserTests()
        {
            FileReader.FileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
             {
                { @"input.txt", new MockFileData("Some content") }
            });
        }

        [Fact]
        public void OnlyFileNameProvided_ReturnsNullOperation()
        {
            var result = parser.Parse(@"input.txt");

            Assert.IsType<FileReader>(result);
        }

        [Fact]
        public void CountStringProvided_ReturnsCountOperation()
        {
            var result = parser.Parse(@"input.txt", "/count");

            Assert.IsType<CountOperation>(result);
        }

        [Fact]
        public void ReplaceWithParametersProvided()
        {
            var result = parser.Parse(@"input.txt", "/replace", "john", "tim");

            Assert.IsType<ReplaceOperation>(result);
        }

        [Fact]
        public void FilterWithParametersProvided()
        {
            var result = parser.Parse(@"input.txt", "/filter", "john");

            Assert.IsType<FilterOperation>(result);
        }


        [Fact]
        public void Compound()
        {
            var result = parser.Parse(@"input.txt", "/replace", "john", "tim", "/filter", "cat", "/count");

            Assert.IsType<CountOperation>(result);
        }

    }
}
