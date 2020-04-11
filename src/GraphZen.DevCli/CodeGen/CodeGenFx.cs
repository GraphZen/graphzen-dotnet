// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.QueryEngine;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class CodeGenFx
    {
        public static void Generate()
        {
            var generators = GetPartialTypeGenerators();
            PartialTypeGenerator.Generate(generators);
        }

        private static IEnumerable<PartialTypeGenerator> GetPartialTypeGenerators()
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
                t.Assembly.GetTypes().Where(_ => _.Namespace != null && _.Namespace.StartsWith(nameof(GraphZen))));

            foreach (var type in types)
            {
                foreach (var partialGen in PartialTypeGenerator.FromType(type))
                {
                    yield return partialGen;
                }
            }

            yield return new SchemaDefinitionTypeAccessorGenerator();
            yield return new SchemaTypeAccessorGenerator();
        }

        public static List<(string kind, string type)> NamedTypes { get; } = typeof(NamedType).Assembly.GetTypes()
            .Where(typeof(NamedType).IsAssignableFrom)
            .Where(_ => !_.IsAbstract)
            .OrderBy(_ => _.Name)
            .Select(_ => (_.Name.Replace("Type", ""), _.Name))
            .ToList();
    }
}