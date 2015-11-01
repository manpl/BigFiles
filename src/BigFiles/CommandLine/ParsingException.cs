using System;
namespace BigFiles.CommandLine
{
    public class ParsingException : Exception
    {
        public string Token { get; private set; }

        public ParsingException(String token)
        {
            this.Token = token;
        }
    }
}
