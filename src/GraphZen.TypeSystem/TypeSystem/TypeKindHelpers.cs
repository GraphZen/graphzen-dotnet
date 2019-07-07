// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public static class TypeKindHelpers
    {
        [NotNull]
        // ReSharper disable once AssignNullToNotNullAttribute
        private static ImmutableDictionary<Type, TypeKind> KindByType { get; } = new Dictionary<Type, TypeKind>
        {
            {typeof(ScalarTypeDefinition), TypeKind.Scalar},
            {typeof(ScalarType), TypeKind.Scalar},
            {typeof(IScalarType), TypeKind.Scalar},
            {typeof(IScalarTypeDefinition), TypeKind.Scalar},


            {typeof(UnionTypeDefinition), TypeKind.Union},
            {typeof(UnionType), TypeKind.Union},
            {typeof(IUnionType), TypeKind.Union},
            {typeof(IUnionTypeDefinition), TypeKind.Union},

            {typeof(ObjectTypeDefinition), TypeKind.Object},
            {typeof(ObjectType), TypeKind.Object},
            {typeof(IObjectType), TypeKind.Object},
            {typeof(IObjectTypeDefinition), TypeKind.Object},

            {typeof(InputObjectTypeDefinition), TypeKind.InputObject},
            {typeof(InputObjectType), TypeKind.InputObject},
            {typeof(IInputObjectType), TypeKind.InputObject},
            {typeof(IInputObjectTypeDefinition), TypeKind.InputObject},


            {typeof(EnumTypeDefinition), TypeKind.Enum},
            {typeof(EnumType), TypeKind.Enum},
            {typeof(IEnumType), TypeKind.Enum},
            {typeof(IEnumTypeDefinition), TypeKind.Enum},

            {typeof(InterfaceTypeDefinition), TypeKind.Interface},
            {typeof(InterfaceType), TypeKind.Interface},
            {typeof(IInterfaceType), TypeKind.Interface},
            {typeof(IInterfaceTypeDefinition), TypeKind.Interface},

            {typeof(ListType), TypeKind.List},
            {typeof(IListType), TypeKind.List},

            {typeof(INonNullType), TypeKind.NonNull},
            {typeof(NonNullType), TypeKind.NonNull}
        }.ToImmutableDictionary();


        public static bool TryGetTypeKindFromDefinition<TGraphQLType>(out TypeKind kind)
            where TGraphQLType : NamedTypeDefinition =>
            KindByType.TryGetValue(typeof(TGraphQLType), out kind);

        public static bool TryGetTypeKindFromType<TGraphQLType>(out TypeKind kind)
            where TGraphQLType : NamedType =>
            KindByType.TryGetValue(typeof(TGraphQLType), out kind);



        public static string ToDisplayString(this TypeKind kind)
        {
            switch (kind)
            {
                case TypeKind.Scalar:
                    return "scalar";
                case TypeKind.Object:
                    return "object";
                case TypeKind.Interface:
                    return "interface";
                case TypeKind.Union:
                    return "union";
                case TypeKind.Enum:
                    return "enum";
                case TypeKind.InputObject:
                    return "input object";
                case TypeKind.List:
                    return "list";
                case TypeKind.NonNull:
                    return "non-null";
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }


        public static bool IsInputType(this TypeKind kind)
        {
            switch (kind)
            {
                case TypeKind.Enum:
                case TypeKind.InputObject:
                case TypeKind.Scalar:
                    return true;
                case TypeKind.Object:
                case TypeKind.Interface:
                case TypeKind.Union:
                    return false;
                default:
                    throw new InvalidOperationException($"Cannot infer if input type from type kind {kind}.");
            }
        }

        public static bool IsOutputType(this TypeKind kind)
        {
            switch (kind)
            {
                case TypeKind.Scalar:
                case TypeKind.Object:
                case TypeKind.Interface:
                case TypeKind.Union:
                case TypeKind.Enum:
                    return true;
                case TypeKind.InputObject:
                    return false;
                default:
                    throw new InvalidOperationException($"Cannot infer if output type from type kind {kind}.");
            }
        }
    }
}