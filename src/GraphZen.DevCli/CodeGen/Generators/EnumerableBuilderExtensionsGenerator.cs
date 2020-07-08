// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public class EnumerableBuilderExtensionsGenerator : PartialTypeGenerator
    {
        public override IReadOnlyList<string> Usings { get; } = new List<string>()
        {
            "GraphZen.TypeSystem.Taxonomy"
        };

        public EnumerableBuilderExtensionsGenerator() : base(typeof(EnumerableBuilderExtensions))
        {
        }

        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, config) in SchemaBuilderInterfaceGenerator.Kinds)
            {
                if (kind != "Type")
                {
                    csharp.AppendLine($@"
    public static IEnumerable<{config.TypeName}Builder> Where(this IEnumerable<{config.TypeName}Builder> source,
            Func<I{config.TypeName}Definition, bool> predicate) =>
            source.Where(_=> predicate(_.GetInfrastructure<{config.TypeName}Definition>()));

");
                }
            }
        }
    }
}