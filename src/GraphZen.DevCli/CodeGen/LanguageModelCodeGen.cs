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
            csharp.Namespace(LanguageModelNamespace, ns =>
            {
                ns.Enum(schema.GetEnum("SyntaxNode"));

                foreach (var nodeType in NodeTypes)
                {
                    var className = nodeType + "Syntax";
                    ns.PartialClass(className, @class =>
                    {
                        @class.AppendLine($@"
	    /// <summary>Empty, read-only list of <see cref=""{className}""/> nodes.</summary>
		public static IReadOnlyList<{className}> EmptyList {{get;}} = new List<{className}>(0).AsReadOnly();
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor""/> enters a <see cref=""{className}""/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.Enter{nodeType}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor""/> leaves a <see cref=""{className}""/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.Leave{nodeType}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor{{TResult}}""/> enters a <see cref=""{className}""/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.Enter{nodeType}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor{{TResult}}""/> leaves a <see cref=""{className}""/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.Leave{nodeType}(this);
		public override SyntaxKind Kind {{get;}} = SyntaxKind.{nodeType};	
");
                    });
                }
            });

            CodeGenHelpers.WriteFile("./src/GraphZen.LanguageModel/LanguageModel/Syntax/SyntaxNode.Generated.cs",
                csharp.ToString());
        }
    }
}