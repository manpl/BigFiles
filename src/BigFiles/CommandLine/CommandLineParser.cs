using BigFiles.Operations;
using System;
using System.Linq;
using System.Reflection;
using Serilog;
using System.Text.RegularExpressions;

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

        public static Regex InputRegex = new Regex(@"\w+?");

        public IOperation Parse(params string[] inputArgs)
        {
            Log.Information("Received: {inputArgs}", inputArgs);
            
            if(inputArgs == null)
            {
                throw new ArgumentNullException("args");
            }
            if (inputArgs.Length == 0)
            {
                throw new ArgumentException("args");
            }

            return ParseImpl(inputArgs);
        }

        private IOperation ParseImpl(string[] inputArgs)
        {
            var startIndex = 0;
            string filename = null;
            IOperation lastOperation = null;

            if (!inputArgs[0].StartsWith("/"))
            {
                startIndex = 1;
                filename = inputArgs[0];
                lastOperation = new FileReader(filename);
            }
            else
            {
                startIndex = 0;
                lastOperation = new CommandLineReader();
            }
            // skip filename
            var arguments = inputArgs.Skip(startIndex).ToArray();

            var args = arguments
                // store original indexes
                .Select((item, index) => new { item, index })
                .Where(e => e.item.StartsWith("/"))
                .Select(e => new
                {
                    name = e.item.Substring(1),
                    args = arguments
                        // find operation element in arguments
                        .Skip(e.index + 1)
                        // take all elements until next operation
                        .TakeWhile(item => !item.StartsWith("/"))
                        .ToArray()
                });

            args.ToList().ForEach(op =>
            {
                Log.Debug("Operation: {name}, Args: {args}", op.name, op.args);
                lastOperation = ParseToken(op.name, op.args, lastOperation);
            });
            return lastOperation;
        }

        private IOperation ParseToken(string operationName, string[] arguments, IOperation lastOperation)
        {
            Log.Debug("Parsing input for operation {operation}", operationName);
            var constuctorArgs = (new object[] { lastOperation }).Concat(arguments).ToArray();
            var operationType = OperationTypes.FirstOrDefault(type => type.Name.ToLower().StartsWith(operationName));
            if (operationType != null)
            {
                try
                {
                    Log.Debug("Operation found: {operation}", operationType.Name);
                    return (IOperation)Activator.CreateInstance(operationType, constuctorArgs);
                }
                catch (MissingMethodException ex)
                {

                    var message = String.Format("Cannot create operation with specified arguments: [{0}]", String.Join(",", arguments));
                    var hint = "HINT:";
                    var availableConstructors = operationType.GetConstructors();
                    foreach (var c in availableConstructors)
                    {
                        var args = c.GetParameters().Skip(1).Select(arg => arg.Name).ToArray();
                        hint += String.Format("\nAvailable set of arguments for {0} is {1}", operationType.Name, String.Join(",", args));
                    }

                    throw new CommandLineException(message, hint, ex);
                }
            }

            throw new CommandLineException(String.Format("Unrecognized command line input: {0}", operationName));
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
