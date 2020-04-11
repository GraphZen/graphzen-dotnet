// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public class SyntaxNodeGenerator : PartialTypeGenerator
    {
        public static IEnumerable<SyntaxNodeGenerator> CreateAll() =>
            LanguageModelCodeGen.NodeTypes.Select(_ => new SyntaxNodeGenerator(_));

        private SyntaxNodeGenerator(Type targetType) : base(targetType)
        {
        }

        public override void Apply(StringBuilder @class)
        {
            var type = TargetType.Name;
            var kind = type.Replace("Syntax", "");
            @class.AppendLine($@"
	    /// <summary>Empty, read-only list of <see cref=""{type}""/> nodes.</summary>
		public static IReadOnlyList<{type}> EmptyList {{get;}} = ImmutableList<{type}>.Empty; 
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor""/> enters a <see cref=""{type}""/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.Enter{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor""/> leaves a <see cref=""{type}""/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.Leave{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor{{TResult}}""/> enters a <see cref=""{type}""/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.Enter{kind}(this);
		/// <summary>Called when a <see cref=""GraphQLSyntaxVisitor{{TResult}}""/> leaves a <see cref=""{type}""/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.Leave{kind}(this);
		public override SyntaxKind Kind {{get;}} = SyntaxKind.{kind};	
");
        }
    }
}