// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     GraphQL AST node
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public abstract class SyntaxNode : ISyntaxNodeLocation
    {
        private readonly Lazy<string> _printed;

        protected SyntaxNode(SyntaxLocation? location)
        {
            Location = location;
            _printed = new Lazy<string>(() => new Printer().Print(this));
        }

        public abstract SyntaxKind Kind { [DebuggerStepThrough] get; }


        internal string DebuggerDisplay => ToSyntaxString();

        public SyntaxLocation? Location { get; }


        public abstract IEnumerable<SyntaxNode> Children();

        public IEnumerable<SyntaxNode> DescendantsAndSelf()
        {
            yield return this;
            foreach (var desc in Descendants())
            {
                yield return desc;
            }
        }

        public IEnumerable<SyntaxNode> Descendants()
        {
            foreach (var c in Children())
            {
                yield return c;
                foreach (var cd in c.Descendants())
                {
                    yield return cd;
                }
            }
        }


        public abstract void VisitLeave(GraphQLSyntaxVisitor visitor);
        public abstract void VisitEnter(GraphQLSyntaxVisitor visitor);

        public abstract TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor);
        public abstract TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor);


        public string ToSyntaxString() => _printed.Value;
    }
}