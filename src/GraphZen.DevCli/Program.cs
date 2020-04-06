// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.Diagnostics.CodeAnalysis;
using GraphZen.CodeGen;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    internal class Program
    {
        private static readonly Dictionary<string, Action> CodeGenTasks = new Dictionary<string, Action>
        {
            { nameof(LanguageModel),LanguageModelCodeGen.Generate },
            {"typeSystem", TypeSystemCodeGen.Generate},
            {"factories", FactoryGenerator.GenerateFactoryMethods}
        };

        private static Command Command(string name) => new Command(name)
        { Handler = CommandHandler.Create(CodeGenTasks[name]) };


        private static void Main(string[] args)
        {
            var root = new RootCommand
            {
                new Command("benchmark")
            };
            var genCmd = new Command("gen")
            {
                Command("typeSystem")
            };
            genCmd.Handler = CommandHandler.Create(() =>
            {
                Console.WriteLine("all handlers");
                foreach (var (_, action) in CodeGenTasks)
                {
                    action();
                }
            });
            root.AddCommand(genCmd);


            var cliBuilder = new CommandLineBuilder(root);
            var cli = cliBuilder.Build();
            cli.InvokeAsync(args).Wait();
        }
    }
}