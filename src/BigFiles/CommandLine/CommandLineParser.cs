using BigFiles.Operations;
using System;
using System.Linq;
using System.Reflection;
using Serilog;

namespace BigFiles.CommandLine
{
    public class CommandLineParser
    {
        static Type[] OperationTypes = null;

        static CommandLineParser()
        {
           var types = Assembly.GetAssembly(typeof(CommandLineParser)).DefinedTypes;
            OperationTypes = types.ToList().Where(t => typeof(FileOperationBase).IsAssignableFrom(t)).ToArray();
        }

        public IFileOperation Parse(string inputArgs)
        {
            Log.Information("Parsing {0}", inputArgs);
            IFileOperation lastOperation = null;
            var tokens = inputArgs.Split('/');

            lastOperation = new NullOperation(tokens[0]);

            for (int index =1 ; index < tokens.Length; index++)
            {
                var operationWithArgs = (tokens[index] ?? "").Trim().Split(' ');
                lastOperation = ParseToken(operationWithArgs, lastOperation);
            }

            return lastOperation;
        }

        private IFileOperation ParseToken(string[] operationWithArgs, IFileOperation lastOperation)
        {
            var operationName = operationWithArgs[0];
            Log.Debug("Parsing input for operation {0}", operationName);
            var constuctorArgs = (new object[] { lastOperation }).Union(operationWithArgs.Skip(1)).ToArray();
            var operationType = OperationTypes.FirstOrDefault(type => type.Name.ToLower().StartsWith(operationName));
            if (operationType != null)
            {
                return (IFileOperation)Activator.CreateInstance(operationType, constuctorArgs);
            }

            throw new ParsingException(operationName ?? "");
        }

        public void PrintUsage()
        {
            var desc = OperationTypes.Select(op => op.GetCustomAttribute<UsageAttribute>()).Where(o => o != null);
            Console.WriteLine("Usage:");
            Console.Write("bf.exe path ");
            desc.ToList().ForEach(option => Console.Write("[" + option.Hint + "]"));
            Console.WriteLine();
        }
    }
}
