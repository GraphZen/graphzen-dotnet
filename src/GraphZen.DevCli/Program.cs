// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using static GraphZen.CodeGen.CodeGenTasks;

namespace GraphZen
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cmd = new RootCommand { new Command("gen") { Handler = CommandHandler.Create(RunCodeGen) } };
            var cliBuilder = new CommandLineBuilder(cmd);
            var cli = cliBuilder.Build();
            cli.InvokeAsync(args).Wait();
        }
    }
}