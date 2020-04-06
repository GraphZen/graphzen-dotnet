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
            GenSyntaxKind();
            GenSyntaxNodePartials();
        }

        private static void GenSyntaxKind()
        {
            var schema = Schema.Create(sb =>
            {
                var syntaxKind = sb.Enum("SyntaxKind");
                foreach (var nodeType in NodeTypes)
                {
                    var kindValue = nodeType.Replace("Syntax", "");
                    syntaxKind.Value(kindValue,
                        v => v.Description($"Indicates an <see cref=\"{nodeType}\"/> node."));
                }
            });

            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(LanguageModelNamespace, ns => { ns.Enum(schema.GetEnum("SyntaxKind")); });
            csharp.WriteToFile("./src/GraphZen.LanguageModel/LanguageModel/Syntax/SyntaxKind.Generated.cs");
        }

        private static void GenSyntaxNodePartials()
        {
            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(LanguageModelNamespace, ns =>
            {
                foreach (var nodeType in NodeTypes)
                {
                    var kind = nodeType.Replace("Syntax", "");
                    ns.PartialClass(nodeType, @class =>
                    {
                        @class.AppendLine($@"
	    /// <summary>Empty, read-only list of <see cref=""{nodeType}""/> nodes.</summary>
		public static IReadOnlyList<{nodeType}> EmptyList {{get;}} = ImmutableList<{nodeType}>.Empty; 
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor""/> enters a <see cref=""{nodeType}""/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.Enter{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor""/> leaves a <see cref=""{nodeType}""/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.Leave{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor{{TResult}}""/> enters a <see cref=""{nodeType}""/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.Enter{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor{{TResult}}""/> leaves a <see cref=""{nodeType}""/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.Leave{kind}(this);
		public override SyntaxKind Kind {{get;}} = SyntaxKind.{kind};	
");
                    });
                }
            });

            csharp.WriteToFile("./src/GraphZen.LanguageModel/LanguageModel/Syntax/SyntaxNode.Generated.cs");
        }
    }
}