using BigFiles.CommandLine;
using BigFiles.Logging;
using BigFiles.Operations;
using Serilog;
using System;
using System.Linq;

namespace BigFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogging();

            var parser = new CommandLineParser();

            try
            {
                var operation = parser.Parse(args);
                operation.ReadLines().ToList();
                operation.Execute();
            }
            catch (ParsingException ex)
            {
                Log.Error("Unrecognized command line input: {0}, exception: {1}", ex.Token, ex.ToString());
                parser.PrintUsage();
            }
            catch (Exception ex)
            {
                Log.Error("Exception: {0}", ex.ToString());
            }
        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.With(new ThreadIdEnricher())
                  .WriteTo.ColoredConsole(
                  outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
                  .MinimumLevel.Debug()
                  .CreateLogger();
        }
    }
}
