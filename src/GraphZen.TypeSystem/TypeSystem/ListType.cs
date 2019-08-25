// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class ListType : IListType
    {
        private ListType(IGraphQLType type)
        {
            OfType = Check.NotNull(type, nameof(type));
        }

        public IGraphQLType OfType { get; }
        public TypeKind Kind { get; } = TypeKind.List;

        public SyntaxNode ToSyntaxNode()
        {
            return this.ToTypeSyntax();
        }

        public static ListType Of(IGraphQLType type)
        {
            return new ListType(type);
        }

        private bool Equals(ListType other)
        {
            return Equals(OfType, other.OfType);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((ListType)obj);
        }

        public override int GetHashCode()
        {
            return OfType.GetHashCode();
        }

        public override string ToString()
        {
            return $"[{OfType}]";
        }
    }
}