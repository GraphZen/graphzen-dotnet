// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract class NamedTypeDefinition : AnnotatableMember, INamedTypeDefinition
    {
        protected NamedTypeDefinition(string name, string? description, Type? clrType,
            IReadOnlyList<IDirective> directives, Schema schema) : base(directives, schema)
        {
            Name = name;
            Description = description;
            ClrType = clrType;
            Description = description;
            IsIntrospection = name.IsIntrospectionType();
            // ReSharper disable once VirtualMemberCallInConstructor
            IsSpec = IsIntrospection || Kind == TypeKind.Scalar && Name.IsSpecScalar();
        }

        public abstract TypeKind Kind { get; }
        public bool IsIntrospection { get; }
        public bool IsSpec { get; }
        public string Name { get; }

        [GraphQLIgnore] public Type? ClrType { get; }

        [GraphQLCanBeNull] public string? Description { get; }
        ISchema IMember.Schema => Schema;


        public static NamedTypeDefinition From(INamedTypeDefinition definition, Schema schema)
        {
            switch (definition)
            {
                case IScalarType __:
                    return ScalarType.From(__, schema);
                case IUnionType __:
                    return UnionType.From(__, schema);
                case IObjectType __:
                    return ObjectType.From(__, schema);
                case IInputObjectType __:
                    return InputObjectType.From(__, schema);
                case IEnumType __:
                    return EnumType.From(__, schema);
                case IInterfaceType __:
                    return InterfaceType.From(__, schema);
            }

            throw new InvalidOperationException($"Unknown type definition: {definition.GetType()}");
        }

        public override string ToString() => Name;
    }
}