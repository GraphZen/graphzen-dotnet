// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
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
        public SyntaxNode ToSyntaxNode() => this.ToTypeSyntax();
        public static ListType Of(IGraphQLType type) => new ListType(type);
        private bool Equals([NotNull] ListType other) => Equals(OfType, other.OfType);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((ListType) obj);
        }

        public override int GetHashCode() => OfType.GetHashCode();

        public override string ToString() => $"[{OfType}]";
    }
}