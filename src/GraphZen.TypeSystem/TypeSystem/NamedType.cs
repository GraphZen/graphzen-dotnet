// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public abstract class NamedType : AnnotatableMember, INamedType
    {
        protected NamedType([NotNull] string name, string description, Type clrType,
            [NotNull] IReadOnlyList<IDirectiveAnnotation> directives) : base(directives)
        {
            Name = name;
            Description = description;
            ClrType = clrType;
        }

        public abstract TypeKind Kind { get; }
        public string Name { get; }
        public override string Description { get; }

        [GraphQLIgnore]
        public Type ClrType { get; }


        [NotNull]
        public static NamedType From(IGraphQLTypeDefinition definition, Schema schema)
        {
            switch (definition)
            {
                case IScalarTypeDefinition __:
                    return ScalarType.From(__);
                case IUnionTypeDefinition __:
                    return UnionType.From(__, schema);
                case IObjectTypeDefinition __:
                    return ObjectType.From(__, schema);
                case IInputObjectTypeDefinition __:
                    return InputObjectType.From(__, schema);
                case IEnumTypeDefinition __:
                    return EnumType.From(__);
                case IInterfaceTypeDefinition __:
                    return InterfaceType.From(__, schema);
            }

            throw new InvalidOperationException($"Unknown type definition: {definition?.GetType()}");
        }

        public override string ToString() => Name;
    }
}