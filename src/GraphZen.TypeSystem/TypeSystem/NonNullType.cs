// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class NonNullType : INonNullType
    {
        private NonNullType(INullableType ofType)
        {
            OfType = ofType;
        }

        public INullableType OfType { get; }

        IGraphQLType IWrappingType.OfType => OfType;
        public TypeKind Kind { get; } = TypeKind.NonNull;

        public SyntaxNode ToSyntaxNode() => this.ToTypeSyntax();


        public static NonNullType Of(INullableType type) => new NonNullType(Check.NotNull(type, nameof(type)));

        private bool Equals(NonNullType other) => Equals(OfType, other.OfType);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((NonNullType)obj);
        }

        public override int GetHashCode() => OfType.GetHashCode();

        public override string ToString() => $"{OfType}!";
    }
}