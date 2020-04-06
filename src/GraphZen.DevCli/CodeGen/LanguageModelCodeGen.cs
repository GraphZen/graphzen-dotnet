// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class LanguageModelCodeGen
    {
        private static List<string> NodeTypes { get; } = typeof(SyntaxNode).Assembly.GetTypes()
            .Where(typeof(SyntaxNode).IsAssignableFrom)
            .Where(_ => !_.IsAbstract)
            .Select(_ => _.Name)
            .OrderBy(_ => _).ToList();

        private const string LanguageModelNamespace = nameof(GraphZen) + "." + nameof(LanguageModel);


        public static void Generate()
        {
            GenSyntaxNode();
        }

        private static void GenSyntaxNode()
        {
            var schema = Schema.Create(sb =>
            {
                var syntaxNode = sb.Enum("SyntaxNode");
                foreach (var nodeType in NodeTypes)
                {
                    syntaxNode.Value(nodeType,
                        v => v.Description($"Indicates an <see cref=\"{nodeType}Syntax\"/> node."));
                }
            });

            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(LanguageModelNamespace, cs => { cs.AddEnum(schema.GetEnum("SyntaxNode")); });
        }
    }
}