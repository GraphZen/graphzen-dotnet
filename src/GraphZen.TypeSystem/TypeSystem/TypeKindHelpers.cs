// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public static class TypeKindHelpers
    {
        private static IReadOnlyDictionary<TypeKind, string> TypeKindDisplayStrings { get; } =
            new Dictionary<TypeKind, string>
            {
                {TypeKind.Scalar, nameof(TypeKind.Scalar)},
                {TypeKind.Object, nameof(TypeKind.Object)},
                {TypeKind.Interface, nameof(TypeKind.Interface)},
                {TypeKind.Union, nameof(TypeKind.Union)},
                {TypeKind.Enum, nameof(TypeKind.Enum)},
                {TypeKind.InputObject, "Input Object"},
                {TypeKind.List, nameof(TypeKind.List)},
                {TypeKind.NonNull, "Non-Null"}
            }.ToImmutableDictionary();

        private static IReadOnlyDictionary<TypeKind, string> TypeKindDisplayStringsLower { get; } =
            TypeKindDisplayStrings.ToImmutableDictionary(_ => _.Key, _ => _.Value.ToLower());


        private static IReadOnlyDictionary<Type, TypeKind> KindByType { get; } = new Dictionary<Type, TypeKind>
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

        public static string ToDisplayStringLower(this TypeKind kind) => TypeKindDisplayStringsLower[kind];
        public static string ToDisplayString(this TypeKind kind) => TypeKindDisplayStrings[kind];


        public static bool TryGetTypeKindFromDefinition<TGraphQLType>(out TypeKind kind)
            where TGraphQLType : NamedTypeDefinition =>
            KindByType.TryGetValue(typeof(TGraphQLType), out kind);

        public static bool TryGetTypeKindFromType<TGraphQLType>(out TypeKind kind)
            where TGraphQLType : NamedType =>
            KindByType.TryGetValue(typeof(TGraphQLType), out kind);


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