// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests
{
    /// <summary>
    ///     Most of these tests are not required for type safety because of static typing,
    ///     but are provided as examples of how type checking is designed to work with the object model
    /// </summary>
    [NoReorder]
    public class TypePredicateTests
    {
        private Schema Schema { get; } = Schema.Create(_ =>
        {
            _.Object(nameof(ObjectType));
            _.Interface(nameof(InterfaceType));
            _.Union(nameof(UnionType)).OfTypes(nameof(ObjectType));
            _.Enum(nameof(EnumType)).Value("foo");
            _.InputObject(nameof(InputObjectType));
            _.Scalar(nameof(ScalarType));
        });

        private ObjectType ObjectType => Schema.GetObject(nameof(ObjectType));
        private InterfaceType InterfaceType => Schema.GetInterface(nameof(InterfaceType));
        private UnionType UnionType => Schema.GetUnion(nameof(UnionType));
        private EnumType EnumType => Schema.GetEnum(nameof(EnumType));
        private InputObjectType InputObjectType => Schema.GetInputObject(nameof(InputObjectType));
        private ScalarType ScalarType => Schema.GetScalar(nameof(ScalarType));
        private ScalarType StringScalar => Schema.GetScalar<string>();


        [Fact]
        public void IsGraphQLTypeReturnsTrueForUnwrappedTypes()
        {
            (StringScalar.As<object>() is IGraphQLType).Should().BeTrue();
            (ObjectType.As<object>() is IGraphQLType).Should().BeTrue();
        }

        [Fact]
        public void IsGraphQLTypeReturnsTrueForWrappedTypes()
        {
            (NonNullType.Of(StringScalar).As<object>() is IGraphQLType).Should().BeTrue();
            (ListType.Of(StringScalar).As<object>() is IGraphQLType).Should().BeTrue();
        }

        [Fact]
        public void IsScalarType_SpecDefined()
        {
            (StringScalar.As<object>() is ScalarType).Should().BeTrue();
            (StringScalar.As<object>() is IScalarType).Should().BeTrue();
        }

        [Fact]
        public void IsScalarType_Custom()
        {
            (ScalarType.As<object>() is ScalarType).Should().BeTrue();
            (ScalarType.As<object>() is IScalarType).Should().BeTrue();
        }

        [Fact]
        public void IsScalar_FalseForWrapped()
        {
            (ListType.Of(StringScalar).As<object>() is ScalarType).Should().BeFalse();
            (ListType.Of(StringScalar).As<object>() is IScalarType).Should().BeFalse();
        }

        [Fact]
        public void IsScalar_FalseForNonScalar()
        {
            (EnumType.As<object>() is ScalarType).Should().BeFalse();
            (EnumType.As<object>() is IScalarType).Should().BeFalse();
        }

        [Fact]
        public void IsObjectType_TrueForObjectType()
        {
            (ObjectType.As<object>() is ObjectType).Should().BeTrue();
            (ObjectType.As<object>() is IObjectType).Should().BeTrue();
        }

        [Fact]
        public void IsObjectType_FalseForWrapped()
        {
            (NonNullType.Of(ObjectType).As<object>() is ObjectType).Should().BeFalse();
            (NonNullType.Of(ObjectType).As<object>() is IObjectType).Should().BeFalse();
        }

        [Fact]
        public void IsObjectType_FalseForNonObjectType()
        {
            (InterfaceType.As<object>() is ObjectType).Should().BeFalse();
            (InterfaceType.As<object>() is IObjectType).Should().BeFalse();
        }

        [Fact]
        public void IsInterfaceType_TrueForInterfaceType()
        {
            (InterfaceType.As<object>() is InterfaceType).Should().BeTrue();
            (InterfaceType.As<object>() is IInterfaceType).Should().BeTrue();
        }

        [Fact]
        public void IsInterfaceType_FalseForWrapped()
        {
            (NonNullType.Of(InterfaceType).As<object>() is InterfaceType).Should().BeFalse();
            (NonNullType.Of(InterfaceType).As<object>() is IInterfaceType).Should().BeFalse();
        }

        [Fact]
        public void IsInterfaceType_FalseForNonInterfaceType()
        {
            (ObjectType.As<object>() is InterfaceType).Should().BeFalse();
            (ObjectType.As<object>() is IInterfaceType).Should().BeFalse();
        }

        [Fact]
        public void IsUnionType_TrueForUnionType()
        {
            (UnionType.As<object>() is UnionType).Should().BeTrue();
            (UnionType.As<object>() is IUnionType).Should().BeTrue();
        }

        [Fact]
        public void IsUnionType_FalseForWrapped()
        {
            (NonNullType.Of(UnionType).As<object>() is UnionType).Should().BeFalse();
            (NonNullType.Of(UnionType).As<object>() is IUnionType).Should().BeFalse();
        }

        [Fact]
        public void IsUnionType_FalseForNonUnionType()
        {
            (ObjectType.As<object>() is UnionType).Should().BeFalse();
            (ObjectType.As<object>() is IUnionType).Should().BeFalse();
        }


        [Fact]
        public void IsEnumType_TrueForEnumType()
        {
            (EnumType.As<object>() is EnumType).Should().BeTrue();
            (EnumType.As<object>() is IEnumType).Should().BeTrue();
        }

        [Fact]
        public void IsEnumType_FalseForWrapped()
        {
            (NonNullType.Of(EnumType).As<object>() is EnumType).Should().BeFalse();
            (NonNullType.Of(EnumType).As<object>() is IEnumType).Should().BeFalse();
        }

        [Fact]
        public void IsEnumType_FalseForNonEnumType()
        {
            (ObjectType.As<object>() is EnumType).Should().BeFalse();
            (ObjectType.As<object>() is IEnumType).Should().BeFalse();
        }


        [Fact]
        public void IsInputObjectType_TrueForInputObjectType()
        {
            (InputObjectType.As<object>() is InputObjectType).Should().BeTrue();
            (InputObjectType.As<object>() is IInputObjectType).Should().BeTrue();
        }

        [Fact]
        public void IsInputObjectType_FalseForWrapped()
        {
            (NonNullType.Of(InputObjectType).As<object>() is InputObjectType).Should()
                .BeFalse();
            (NonNullType.Of(InputObjectType).As<object>() is IInputObjectType).Should()
                .BeFalse();
        }

        [Fact]
        public void IsInputObjectType_FalseForNonInputObjectType()
        {
            (ObjectType.As<object>() is InputObjectType).Should().BeFalse();
            (ObjectType.As<object>() is IInputObjectType).Should().BeFalse();
        }

        [Fact]
        public void IsList_TrueForWrappedInputType()
        {
            (ListType.Of(StringScalar).As<object>() is ListType).Should().BeTrue();
            (ListType.Of(StringScalar).As<object>() is IListType).Should().BeTrue();
            (ListType.Of(StringScalar).As<object>() is ListType).Should().BeTrue();
            (ListType.Of(StringScalar).As<object>() is IListType).Should().BeTrue();
        }

        [Fact]
        public void IsList_TrueForWrappedOutputType()
        {
            (ListType.Of(StringScalar).As<object>() is ListType).Should().BeTrue();
            (ListType.Of(StringScalar).As<object>() is IListType).Should().BeTrue();
            (ListType.Of(StringScalar).As<object>() is ListType).Should().BeTrue();
            (ListType.Of(StringScalar).As<object>() is IListType).Should().BeTrue();
        }

        [Fact]
        public void IsList_FalseForUnwrappedType()
        {
            (ObjectType.As<object>() is ListType).Should().BeFalse();
            (ObjectType.As<object>() is IListType).Should().BeFalse();
            (ObjectType.As<object>() is ListType).Should().BeFalse();
            (ObjectType.As<object>() is IListType).Should().BeFalse();
            (ObjectType.As<object>() is ListType).Should().BeFalse();
            (ObjectType.As<object>() is IListType).Should().BeFalse();
        }

        [Fact]
        public void IsNonNull_TrueForWrappedInputType()
        {
            (NonNullType.Of(StringScalar).As<object>() is NonNullType).Should().BeTrue();
            (NonNullType.Of(StringScalar).As<object>() is INonNullType).Should().BeTrue();
            (NonNullType.Of(StringScalar).As<object>() is NonNullType).Should().BeTrue();
            (NonNullType.Of(StringScalar).As<object>() is INonNullType).Should().BeTrue();
        }

        [Fact]
        public void IsNonNull_TrueForWrappedOutputType()
        {
            (NonNullType.Of(StringScalar).As<object>() is NonNullType).Should().BeTrue();
            (NonNullType.Of(StringScalar).As<object>() is INonNullType).Should().BeTrue();
            (NonNullType.Of(StringScalar).As<object>() is NonNullType).Should().BeTrue();
            (NonNullType.Of(StringScalar).As<object>() is INonNullType).Should().BeTrue();
        }

        [Fact]
        public void IsNonNull_FalseForUnwrappedType()
        {
            (ObjectType.As<object>() is NonNullType).Should().BeFalse();
            (ObjectType.As<object>() is INonNullType).Should().BeFalse();
            (ObjectType.As<object>() is NonNullType).Should().BeFalse();
            (ObjectType.As<object>() is INonNullType).Should().BeFalse();
            (ObjectType.As<object>() is NonNullType).Should().BeFalse();
            (ObjectType.As<object>() is INonNullType).Should().BeFalse();
        }

        [Fact]
        public void IsInputType_TrueForInputType()
        {
            InputObjectType.IsInputType().Should().BeTrue();
        }

        [Fact]
        public void IsInputType_TrueForWrappedInputType()
        {
            ListType.Of(InputObjectType).IsInputType().Should().BeTrue();
            NonNullType.Of(InputObjectType).IsInputType().Should().BeTrue();
        }

        [Fact]
        public void IsInputType_FalseForOutputType()
        {
            ObjectType.IsInputType().Should().BeFalse();
        }

        [Fact]
        public void IsInputType_FalseForWrappedOutputType()
        {
            NonNullType.Of(ObjectType).IsInputType().Should().BeFalse();
            ListType.Of(ObjectType).IsInputType().Should().BeFalse();
        }


        [Fact]
        public void IsOutputType_TrueForOutputType()
        {
            ObjectType.IsOutputType().Should().BeTrue();
        }

        [Fact]
        public void IsOutputType_TrueForWrappedOutputType()
        {
            ListType.Of(ObjectType).IsOutputType().Should().BeTrue();
            NonNullType.Of(ObjectType).IsOutputType().Should().BeTrue();
        }

        [Fact]
        public void IsOutputType_FalseForInputType()
        {
            InputObjectType.IsOutputType().Should().BeFalse();
        }

        [Fact]
        public void IsOutputType_FalseForWrappedInputType()
        {
            NonNullType.Of(InputObjectType).IsOutputType().Should().BeFalse();
            ListType.Of(InputObjectType).IsOutputType().Should().BeFalse();
        }

        [Fact]
        public void IsLeafType_TrueForScalarsAndEnums()
        {
            (ScalarType.As<object>() is ILeafType).Should().BeTrue();
            (EnumType.As<object>() is ILeafType).Should().BeTrue();
        }

        [Fact]
        public void IsLeafType_FalseForWrappedLeafType()
        {
            (NonNullType.Of(ScalarType).As<object>() is ILeafType).Should().BeFalse();
        }

        [Fact]
        public void IsLeafType_FalseForNonLeafTypes()
        {
            (ObjectType.As<object>() is ILeafType).Should().BeFalse();
        }

        [Fact]
        public void IsLeafType_FalseForWrappedNonLeafTypes()
        {
            (NonNullType.Of(InputObjectType).As<object>() is ILeafType).Should().BeFalse();
        }

        [Fact]
        public void IsCompositeType_TrueForObjectInterfaceAndUnionTypes()
        {
            (ObjectType.As<object>() is ICompositeType).Should().BeTrue();
            (InterfaceType.As<object>() is ICompositeType).Should().BeTrue();
            (UnionType.As<object>() is ICompositeType).Should().BeTrue();
        }

        [Fact]
        public void IsCompositeType_FalseForWrappedCompositeType()
        {
            (ListType.Of(ObjectType).As<object>() is ICompositeType).Should().BeFalse();
        }

        [Fact]
        public void IsCompositeType_FalseForNonCompositeType()
        {
            (InputObjectType.As<object>() is ICompositeType).Should().BeFalse();
        }

        [Fact]
        public void IsCompositeType_FalseForWrappedNonCompositeType()
        {
            (ListType.Of(InputObjectType).As<object>() is ICompositeType).Should().BeFalse();
        }


        [Fact]
        public void IsAbstractType_TrueForInterfaceAndUnionTypes()
        {
            (InterfaceType.As<object>() is IAbstractType).Should().BeTrue();
            (UnionType.As<object>() is IAbstractType).Should().BeTrue();
        }

        [Fact]
        public void IsAbstractType_FalseForWrappedAbstractType()
        {
            (ListType.Of(UnionType).As<object>() is IAbstractType).Should().BeFalse();
        }

        [Fact]
        public void IsAbstractType_FalseForNonAbstractType()
        {
            (InputObjectType.As<object>() is IAbstractType).Should().BeFalse();
        }

        [Fact]
        public void IsAbstractType_FalseForWrappedNonAbstractType()
        {
            (ListType.Of(InputObjectType).As<object>() is IAbstractType).Should().BeFalse();
        }

        [Fact]
        public void IsWrappingType_TrueForListAndNonNullTypes()
        {
            (ListType.Of(InputObjectType).As<object>() is IWrappingType).Should().BeTrue();
            (NonNullType.Of(InputObjectType).As<object>() is IWrappingType).Should().BeTrue();
            (ListType.Of(ObjectType).As<object>() is IWrappingType).Should().BeTrue();
            (NonNullType.Of(ObjectType).As<object>() is IWrappingType).Should().BeTrue();
        }

        [Fact]
        public void IsNullable_TrueForUnwrappedTypes()
        {
            (ObjectType.As<object>() is INullableType).Should().BeTrue();
        }

        [Fact]
        public void IsNullable_TrueListOfNonNullTYpes()
        {
            (ListType.Of(NonNullType.Of(ObjectType)).As<object>() is INullableType).Should().BeTrue();
        }

        [Fact]
        public void IsNullable_FalseForNonNullTypes()
        {
            (NonNullType.Of(ObjectType).As<object>() is INullableType).Should().BeFalse();
            (NonNullType.Of(InputObjectType).As<object>() is INullableType).Should().BeFalse();
        }

        [Fact]
        public void GetNullableType_ReturnsNullForNoType()
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            ((IGraphQLType)null!).GetNullableType().Should().BeNull();
        }

        [Fact]
        public void GetNullableType_ReturnsSelfForNullableType()
        {
            ObjectType.GetNullableType().GetNullableType().Should().Be(ObjectType);
            var listOfObject = ListType.Of(ObjectType);
            listOfObject.GetNullableType().Should().Be(listOfObject);
        }

        [Fact]
        public void GetNullableType_UnwrapsNonNullType()
        {
            NonNullType.Of(ObjectType).GetNullableType().Should().Be(ObjectType);
        }

        [Fact]
        public void IsNamedType_TrueForUnwrappedTypes()
        {
            (ObjectType.As<object>() is INamedType).Should().BeTrue();
        }

        [Fact]
        public void IsNamedType_FalseForListAndNonNullTypes()
        {
            (NonNullType.Of(ObjectType).As<object>() is INamedType).Should().BeFalse();
            (NonNullType.Of(InputObjectType).As<object>() is INamedType).Should().BeFalse();
            (ListType.Of(ObjectType).As<object>() is INamedType).Should().BeFalse();
            (ListType.Of(InputObjectType).As<object>() is INamedType).Should().BeFalse();
        }

        [Fact]
        public void GetNamedType_ReturnsNullForNoType()
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            ((IGraphQLType)null!).GetNamedType().Should().BeNull();
        }

        [Fact]
        public void GetNamedType_ReturnsSelfForAnUnwrappedType()
        {
            ObjectType.GetNamedType().Should().Be(ObjectType);
        }

        [Fact]
        public void GetNamedType_UnwrapsWrapperTypes()
        {
            NonNullType.Of(ObjectType).GetNamedType().Should().Be(ObjectType);
            ListType.Of(ObjectType).GetNamedType().Should().Be(ObjectType);
        }

        [Fact]
        public void GetNamedType_UnwrapsDeeplyWrapperTypes()
        {
            NonNullType.Of(ListType.Of(NonNullType.Of(InputObjectType))).GetNamedType().Should()
                .Be(InputObjectType);
        }
    }
}