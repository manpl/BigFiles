using System;
namespace BigFiles.CommandLine
{
    public class ParsingException : CommandLineException
    {
        public string Token { get; }

        public ParsingException(String token)
        {
            Token = token;
        }
    }
}
