// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public static class TypeComparators
    {
        public static bool IsSubtypeOf(this IGraphQLType maybeSubType, IGraphQLType superType,
            Schema schema)
        {
            Check.NotNull(maybeSubType, nameof(maybeSubType));
            Check.NotNull(superType, nameof(superType));
            Check.NotNull(schema, nameof(schema));
            // Equivalent type is a valid subtype
            if (maybeSubType.Equals(superType))
            {
                return true;
            }

            // If superType is non-null, maybeSubType must also be non-null.
            if (superType is NonNullType superTypeNonNull)
            {
                if (maybeSubType is NonNullType maybeSubTypeNonNull)
                {
                    return maybeSubTypeNonNull.OfType.IsSubtypeOf(superTypeNonNull.OfType, schema);
                }

                return false;
            }
            else if (maybeSubType is NonNullType maybeSubTypeNonNull)
            {
                // If superType is nullable, maybeSubType may be non-null or nullable.
                return maybeSubTypeNonNull.OfType.IsSubtypeOf(superType, schema);
            }

            if (superType is ListType superTypeList)
            {
                if (maybeSubType is ListType maybeSubTypeList)
                {
                    return maybeSubTypeList.OfType.IsSubtypeOf(superTypeList.OfType, schema);
                }

                return false;
            }

            if (maybeSubType is ListType)
            // If superType is not a list, maybeSubType must also be not a list.
            {
                return false;
            }


            // If superType type is an abstract type, maybeSubType type may be a currently
            // possible object type.
            if (superType is IAbstractType superTypeAbstract &&
                maybeSubType is ObjectType maybeSubTypeObject
                && schema.IsPossibleType(superTypeAbstract, maybeSubTypeObject)
            )
            {
                return true;
            }

            // Otherwise, the child type is not a valid subtype of the parent type.
            return false;
        }
    }
}