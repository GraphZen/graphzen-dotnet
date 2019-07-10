// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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