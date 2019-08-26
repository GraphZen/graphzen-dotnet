// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using Xunit;
#nullable disable


namespace GraphZen.TypeSystem
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
                typeof(EnumType),
                typeof(EnumTypeDefinition),
                typeof(InputObjectType),
                typeof(InputObjectTypeDefinition),
                typeof(InterfaceType),
                typeof(InterfaceTypeDefinition),
                typeof(ListType),
                typeof(NonNullType),
                typeof(ObjectType),
                typeof(ObjectTypeDefinition),
                typeof(ScalarType),
                typeof(ScalarTypeDefinition),
                typeof(TypeReference),
                typeof(UnionType),
                typeof(UnionTypeDefinition)
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
                typeof(EnumTypeDefinition),
                typeof(InputObjectType),
                typeof(InputObjectTypeDefinition),
                typeof(InterfaceType),
                typeof(InterfaceTypeDefinition),
                typeof(ObjectType),
                typeof(ObjectTypeDefinition),
                typeof(ScalarType),
                typeof(ScalarTypeDefinition),
                typeof(UnionType),
                typeof(UnionTypeDefinition)
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
        [InlineData(typeof(INamedType))]
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
                typeof(EnumTypeDefinition),
                typeof(InputObjectType),
                typeof(InputObjectTypeDefinition),
                typeof(InterfaceType),
                typeof(InterfaceTypeDefinition),
                typeof(ObjectType),
                typeof(ObjectTypeDefinition),
                typeof(ScalarType),
                typeof(ScalarTypeDefinition),
                typeof(UnionType),
                typeof(UnionTypeDefinition)
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
            var types = ClrTypeUtils.GetImplementedTypes(typeof(IAbstractTypeDefinition));

            var expected = new[]
            {
                typeof(InterfaceType),
                typeof(InterfaceTypeDefinition),
                typeof(UnionType),
                typeof(UnionTypeDefinition)
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
                typeof(AnnotatableMemberDefinition),
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
            var types = ClrTypeUtils.GetImplementedTypes(typeof(ICompositeTypeDefinition));

            var expected = new[]
            {
                typeof(InterfaceType),
                typeof(InterfaceTypeDefinition),
                typeof(ObjectType),
                typeof(ObjectTypeDefinition),
                typeof(UnionType),
                typeof(UnionTypeDefinition)
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
                typeof(EnumTypeDefinition),
                typeof(ScalarType),
                typeof(ScalarTypeDefinition)
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