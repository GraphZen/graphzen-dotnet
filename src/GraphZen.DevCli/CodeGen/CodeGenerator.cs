// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.CodeGen.Generators;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.QueryEngine;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using LibGit2Sharp;

namespace GraphZen.CodeGen
{
    public static class CodeGenerator
    {
        public static void Generate()
        {
            AssertChangesCommitted();

            var generated = new List<GeneratedCode>
            {
                LanguageModelCodeGen.GenSyntaxKindEnum(),
                LanguageModelCodeGen.GenSyntaxVisitorPartials()
            };
            generated.AddRange(PartialTypeGenerator.Generate(CreatePartialTypeGenerators()));
            generated.AddRange(TypeSystemSpecCodeGenerator.ScaffoldSystemSpec());

            var generatedFiles = Directory.GetFiles(".", "*Generated.cs", SearchOption.AllDirectories);
            foreach (var _ in generatedFiles)
            {
                File.Delete(_);
                Console.Write($"Deleted {_}");
            }

            foreach (var _ in generated)
            {
                _.WriteToFile();
            }
        }

        private static void AssertChangesCommitted()
        {
            using var repo = new Repository("./");
            if (repo.RetrieveStatus().IsDirty)
            {
                Console.WriteLine("There are uncommitted files in the repository. Save changes ([Y]/N)?");
                var commitMessage = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(commitMessage) ||
                    commitMessage.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    Commands.Stage(repo, "*");
                    var sig = repo.Config.BuildSignature(DateTimeOffset.Now);
                    repo.Commit("saving changes before code-gen", sig, sig);
                }
            }
        }

        private static IReadOnlyList<PartialTypeGenerator> CreatePartialTypeGenerators()
        {
            var types = new[]
                {
                    typeof(GraphQLNameAttribute), // Abstractions
                    typeof(GraphQLClient), // Client
                    typeof(IInfrastructure<>), // Infrastructure
                    typeof(SyntaxNode), // LanguageModel
                    typeof(IExecutor), // QueryEngine
                    typeof(Schema) // TypeSystem
                }.SelectMany(t =>
                    t.Assembly.GetTypes().Where(_ => _.Namespace != null && _.Namespace.StartsWith(nameof(GraphZen))))
                .ToList();

            var generators = new List<PartialTypeGenerator>
            {
                new SchemaDefinitionTypeAccessorGenerator(),
                new SchemaTypeAccessorGenerator()
            };
            generators.AddRange(PartialTypeGenerator.FromTypes(types));
            generators.AddRange(SyntaxNodeGenerator.CreateAll());
            return generators;
        }
    }
}