// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract class NamedType : AnnotatableMember, INamedType
    {
        protected NamedType(string name, string? description, Type? clrType,
            IReadOnlyList<IDirectiveAnnotation> directives, Schema schema) : base(directives, schema)
        {
            Name = name;
            Description = description;
            ClrType = clrType;
            Description = description;
            IsIntrospection = SpecReservedNames.IntrospectionTypeNames.Contains(name);
            // ReSharper disable once VirtualMemberCallInConstructor
            IsSpec = IsIntrospection || Kind == TypeKind.Scalar && SpecReservedNames.ScalarTypeNames.Contains(Name);
        }

        public abstract TypeKind Kind { get; }
        public bool IsIntrospection { get; }
        public bool IsSpec { get; }
        public string Name { get; }

        [GraphQLIgnore] public Type? ClrType { get; }

        [GraphQLCanBeNull] public string? Description { get; }
        ISchemaDefinition IMemberDefinition.Schema => Schema;
        IEnumerable<IMemberDefinition> IMemberParentDefinition.Children() => Children();

        public abstract IEnumerable<IMember> Children();


        public static NamedType From(INamedTypeDefinition definition, Schema schema)
        {
            switch (definition)
            {
                case IScalarTypeDefinition __:
                    return ScalarType.From(__, schema);
                case IUnionTypeDefinition __:
                    return UnionType.From(__, schema);
                case IObjectTypeDefinition __:
                    return ObjectType.From(__, schema);
                case IInputObjectTypeDefinition __:
                    return InputObjectType.From(__, schema);
                case IEnumTypeDefinition __:
                    return EnumType.From(__, schema);
                case IInterfaceTypeDefinition __:
                    return InterfaceType.From(__, schema);
            }

            throw new InvalidOperationException($"Unknown type definition: {definition.GetType()}");
        }

        public override string ToString() => Name;
    }
}