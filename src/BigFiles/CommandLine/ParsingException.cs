using System;
namespace BigFiles.CommandLine
{

    public class CommandLineException : Exception
    {

    }

    public class ParsingException : CommandLineException
    {
        public string Token { get; private set; }

        public ParsingException(String token)
        {
            this.Token = token;
        }
    }
}
