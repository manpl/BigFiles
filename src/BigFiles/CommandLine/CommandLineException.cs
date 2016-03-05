using System;

namespace BigFiles.CommandLine
{
    public class CommandLineException : Exception
    {
        public CommandLineException(String message, String hint, Exception ex) : base(message + "\n" + hint, ex)
        {

        }

        public CommandLineException(String message) : base(message)
        {

        }

        public CommandLineException()
        {

        }
    }
}
