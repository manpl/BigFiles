using BigFiles.CommandLine;
using BigFiles.Logging;
using BigFiles.Operations;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.IOFile;
using System;
using System.Linq;

namespace BigFiles
{
    class Program
    {
        static int Main(string[] args)
        {
            ConfigureLogging();

            var parser = new CommandLineParser();

            try
            {
                var operation = parser.Parse(args);
                operation.ReadTextChunk().ToList();
            }
            catch (CommandLineException ex)
            {
                Log.Error("Error: {message}", ex.Message);
                parser.PrintUsage();
            }
            catch (Exception ex)
            {
                Log.Error("Exception: {exception}", ex.ToString());
                return -1;
            }

            return 0;
        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new ThreadIdEnricher())
                .WriteTo.Sink(new FileSink(@"bf.logs.json", new JsonFormatter(false, null, true), null), LogEventLevel.Verbose)
                .WriteTo.RollingFile("bf.log", Serilog.Events.LogEventLevel.Verbose)
                .WriteTo.ColoredConsole(
                      outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}", 
#if !DEBUG
                      restrictedToMinimumLevel: LogEventLevel.Error
#else
                      restrictedToMinimumLevel: LogEventLevel.Verbose
#endif
)
                .MinimumLevel.Debug()
                .CreateLogger();
        }
    }
}
