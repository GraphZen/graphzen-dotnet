// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.CodeGen.CodeGenFx;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public static class LanguageModelCodeGen
    {
        public static IReadOnlyList<Type> NodeTypes { get; } =
            typeof(SyntaxNode).Assembly.GetTypes()
                .Where(typeof(SyntaxNode).IsAssignableFrom)
                .Where(_ => !_.IsAbstract)
                .OrderBy(_ => _.Name).ToList();

        private static IReadOnlyList<(string kind, string type)> NodeTypeKind { get; } = NodeTypes
            .Select(_ => (_.Name.Replace("Syntax", ""), _.Name))
            .ToList();

        private const string LanguageModelNamespace = nameof(GraphZen) + "." + nameof(LanguageModel);

        public static GeneratedCode GenSyntaxVisitorPartials()
        {
            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(LanguageModelNamespace, ns =>
            {
                ns.AbstractPartialClass(
                    "GraphQLSyntaxVisitor"
                    , @class =>
                    {
                        foreach (var (kind, nodeType) in NodeTypeKind)
                        {
                            @class.AppendLine($@"
     /// <summary>Called when the visitor enters a <see cref=""{nodeType}""/> node.</summary>
        public virtual void Enter{kind}( {nodeType} node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref=""{nodeType}""/> node.</summary>
        public virtual void Leave{kind}( {nodeType} node) => OnLeave(node);
");
                        }
                    });

                ns.AbstractPartialClass(
                    "GraphQLSyntaxVisitor<TResult>"
                    , @class =>
                    {
                        foreach (var (kind, nodeType) in NodeTypeKind)
                        {
                            @class.AppendLine($@"
     /// <summary>Called when the visitor enters a <see cref=""{nodeType}""/> node.</summary>
        public virtual TResult  Enter{kind}( {nodeType} node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref=""{nodeType}""/> node.</summary>
        public virtual TResult Leave{kind}( {nodeType} node) => OnLeave(node);
");
                        }
                    });
            });
            return new GeneratedCode("./src/GraphZen.LanguageModel/LanguageModel/GraphQLSyntaxVisitor.Generated.cs",
                csharp.ToString());
        }

        public static GeneratedCode GenSyntaxKindEnum()
        {
            var schema = Schema.Create(sb =>
            {
                var syntaxKind = sb.Enum("SyntaxKind");
                foreach (var (kind, type) in NodeTypeKind)
                {
                    syntaxKind.Value(kind,
                        v => v.Description($"Indicates an <see cref=\"{type}\"/> node."));
                }
            });

            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(LanguageModelNamespace, ns => { ns.Enum(schema.GetEnum("SyntaxKind")); });
            return new GeneratedCode("./src/GraphZen.LanguageModel/LanguageModel/Syntax/SyntaxKind.Generated.cs", csharp.ToString());
        }
    }
}