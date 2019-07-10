using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using GraphZen.Infrastructure;

namespace GraphZen
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cmd = new RootCommand
            {
                new Command("gen")
                {
                    Handler = CommandHandler.Create(() => { ConfigurationTestCodeGenerator.Generate(); })
                }
            };

            var cliBuilder = new CommandLineBuilder(cmd);
            var cli = cliBuilder.Build();
            // ReSharper disable once PossibleNullReferenceException
            cli.InvokeAsync(args).Wait();
        }
    }
}