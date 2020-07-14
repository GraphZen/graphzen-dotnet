// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests
{
    public class TaxonomyTests
    {
        [Theory]
        [InlineData(typeof(IGraphQLTypeReference))]
        public void graphql_type_references(Type type)
        {
            var types = ClrTypeUtils.GetImplementedTypes(type).DumpTypes();
            var expected = new[]
            {
                typeof(ArgumentTypeReference),
                typeof(EnumType),
                typeof(MutableEnumType),
                typeof(FieldTypeReference),
                typeof(InputObjectType),
                typeof(MutableInputObjectType),
                typeof(InterfaceType),
                typeof(MutableInterfaceType),
                typeof(ListType),
                typeof(NonNullType),
                typeof(ObjectType),
                typeof(MutableObjectType),
                typeof(ScalarType),
                typeof(MutableScalarType),
                typeof(UnionType),
                typeof(MutableUnionType)
            };

            Assert.Equal(expected, types);
        }

        [Theory]
        [InlineData(typeof(INamedTypeDefinition))]
        public void graphql_type_definitions(Type type)
        {
            var types = ClrTypeUtils.GetImplementedTypes(type).DumpTypes();
            var expected = new[]
            {
                typeof(EnumType),
                typeof(MutableEnumType),
                typeof(InputObjectType),
                typeof(MutableInputObjectType),
                typeof(InterfaceType),
                typeof(MutableInterfaceType),
                typeof(ObjectType),
                typeof(MutableObjectType),
                typeof(ScalarType),
                typeof(MutableScalarType),
                typeof(UnionType),
                typeof(MutableUnionType)
            };

            Assert.Equal(expected, types);
        }

        [Theory]
        [InlineData(typeof(IGraphQLType))]
        public void graphql_types(Type type)
        {
            var types = ClrTypeUtils.GetImplementedTypes(type).DumpTypes();
            var expected = new[]
            {
                typeof(EnumType),
                typeof(InputObjectType),
                typeof(InterfaceType),
                typeof(ListType),
                typeof(NonNullType),
                typeof(ObjectType),
                typeof(ScalarType),
                typeof(UnionType)
            };

            Assert.Equal(expected, types);
        }


        [Theory]
        [InlineData(typeof(INamedTypeDefinition))]
        public void graphql_named_types(Type type)
        {
            var types = ClrTypeUtils.GetImplementedTypes(type);

            var expected = new[]
            {
                typeof(EnumType),
                typeof(InputObjectType),
                typeof(InterfaceType),
                typeof(ObjectType),
                typeof(ScalarType),
                typeof(UnionType)
            };

            Assert.Equal(expected, types);
        }

        [Theory]
        [InlineData(typeof(INamedTypeDefinition))]
        public void named_type_definitions(Type type)
        {
            var types = ClrTypeUtils.GetImplementedTypes(type);

            var expected = new[]
            {
                typeof(EnumType),
                typeof(MutableEnumType),
                typeof(InputObjectType),
                typeof(MutableInputObjectType),
                typeof(InterfaceType),
                typeof(MutableInterfaceType),
                typeof(ObjectType),
                typeof(MutableObjectType),
                typeof(ScalarType),
                typeof(MutableScalarType),
                typeof(UnionType),
                typeof(MutableUnionType)
            };

            Assert.Equal(expected, types);
        }


        [Theory(Skip = "wip")]
        [InlineData(typeof(IDescription))]
        [InlineData(typeof(IClrType))]
        // [InlineData(typeof(IMutableClrType))]
        public void ClrTypes(Type type)
        {
            var types = ClrTypeUtils.GetImplementedTypes(type);

            var expected = new[]
            {
                typeof(EnumType),
                typeof(EnumValue),
                typeof(Field),
                typeof(InputObjectType),
                typeof(InputValue),
                typeof(InterfaceType),
                typeof(ObjectType),
                typeof(ScalarType),
                typeof(UnionType)
            };

            Assert.Equal(expected, types);
        }

        [Theory(Skip = "wip")]
        [InlineData(typeof(IDescription))]
        [InlineData(typeof(IMutableDescription))]
        public void TypeDescriptions(Type type)
        {
            var types = ClrTypeUtils.GetImplementedTypes(type);

            var expected = new[]
            {
                typeof(EnumType),
                typeof(EnumValue),
                typeof(Field),
                typeof(InputObjectType),
                typeof(InputValue),
                typeof(InterfaceType),
                typeof(ObjectType),
                typeof(ScalarType),
                typeof(UnionType)
            };

            Assert.Equal(expected, types);
        }

        [Fact]
        public void AbstractTypeDefinitions()
        {
            var types = ClrTypeUtils.GetImplementedTypes(typeof(IAbstractType));

            var expected = new[]
            {
                typeof(InterfaceType),
                typeof(MutableInterfaceType),
                typeof(UnionType),
                typeof(MutableUnionType)
            };

            Assert.Equal(expected, types);
        }

        [Fact]
        public void AbstractTypes()
        {
            var types = ClrTypeUtils.GetImplementedTypes(typeof(IAbstractType));

            var expected = new[]
            {
                typeof(InterfaceType),
                typeof(UnionType)
            };

            Assert.Equal(expected, types);
        }


        [Fact(Skip = "wip")]
        public void Annotated()
        {
            var types = ClrTypeUtils.GetImplementedTypes(typeof(IDirectives));

            var expected = new[]
            {
                typeof(MutableAnnotatableMember),
                typeof(EnumType),
                typeof(EnumValue),
                typeof(Field),
                typeof(InputObjectType),
                typeof(InputValue),
                typeof(InterfaceType),
                typeof(ObjectType),
                typeof(ScalarType),
                typeof(Schema),
                typeof(UnionType)
            };

            Assert.Equal(expected, types);
        }

        [Fact]
        public void CompositeTypeDefinitions()
        {
            var types = ClrTypeUtils.GetImplementedTypes(typeof(ICompositeType));

            var expected = new[]
            {
                typeof(InterfaceType),
                typeof(MutableInterfaceType),
                typeof(ObjectType),
                typeof(MutableObjectType),
                typeof(UnionType),
                typeof(MutableUnionType)
            };

            Assert.Equal(expected, types);
        }


        [Fact]
        public void CompositeTypes()
        {
            var types = ClrTypeUtils.GetImplementedTypes(typeof(ICompositeType));

            var expected = new[]
            {
                typeof(InterfaceType),
                typeof(ObjectType),
                typeof(UnionType)
            };

            Assert.Equal(expected, types);
        }


        [Fact]
        public void LeafTypeDefinitions()
        {
            var types = ClrTypeUtils.GetImplementedTypes(typeof(ILeafTypeDefinition));

            var expected = new[]
            {
                typeof(EnumType),
                typeof(MutableEnumType),
                typeof(ScalarType),
                typeof(MutableScalarType)
            };

            Assert.Equal(expected, types);
        }

        [Fact]
        public void LeafTypes()
        {
            var types = ClrTypeUtils.GetImplementedTypes(typeof(ILeafType));

            var expected = new[]
            {
                typeof(EnumType),
                typeof(ScalarType)
            };

            Assert.Equal(expected, types);
        }


        [Fact]
        public void NullableTypes()
        {
            var types = ClrTypeUtils.GetImplementedTypes(typeof(INullableType)).DumpTypes();

            var expected = new[]
            {
                typeof(EnumType),
                typeof(InputObjectType),
                typeof(InterfaceType),
                typeof(ListType),
                typeof(ObjectType),
                typeof(ScalarType),
                typeof(UnionType)
            };

            Assert.Equal(expected, types);
        }
    }
}