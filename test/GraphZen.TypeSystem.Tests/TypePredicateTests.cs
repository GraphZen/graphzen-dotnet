// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem.Tests;

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


    [Fact]
    public void IsGraphQLTypeReturnsTrueForUnwrappedTypes()
    {
        Assert.True(SpecScalars.String is IGraphQLType);
        Assert.True(ObjectType is IGraphQLType);
    }

    [Fact]
    public void IsGraphQLTypeReturnsTrueForWrappedTypes()
    {
        Assert.True(NonNullType.Of(SpecScalars.String) is IGraphQLType);
        Assert.True(ListType.Of(SpecScalars.String) is IGraphQLType);
    }

    [Fact]
    public void IsScalarType_SpecDefined()
    {
        Assert.True(SpecScalars.String is ScalarType);
        Assert.True(SpecScalars.String is IScalarType);
    }

    [Fact]
    public void IsScalarType_Custom()
    {
        Assert.True(ScalarType is ScalarType);
        Assert.True(ScalarType is IScalarType);
    }

    [Fact]
    public void IsScalar_FalseForWrapped()
    {
        Assert.False((object)ListType.Of(SpecScalars.String) is ScalarType);
        Assert.False((object)ListType.Of(SpecScalars.String) is IScalarType);
    }

    [Fact]
    public void IsScalar_FalseForNonScalar()
    {
        Assert.False((object)EnumType is ScalarType);
        Assert.False((object)EnumType is IScalarType);
    }

    [Fact]
    public void IsObjectType_TrueForObjectType()
    {
        Assert.True(ObjectType is ObjectType);
        Assert.True(ObjectType is IObjectType);
    }

    [Fact]
    public void IsObjectType_FalseForWrapped()
    {
        Assert.False((object)NonNullType.Of(ObjectType) is ObjectType);
        Assert.False((object)NonNullType.Of(ObjectType) is IObjectType);
    }

    [Fact]
    public void IsObjectType_FalseForNonObjectType()
    {
        Assert.False((object)InterfaceType is ObjectType);
        Assert.False((object)InterfaceType is IObjectType);
    }

    [Fact]
    public void IsInterfaceType_TrueForInterfaceType()
    {
        Assert.True(InterfaceType is InterfaceType);
        Assert.True(InterfaceType is IInterfaceType);
    }

    [Fact]
    public void IsInterfaceType_FalseForWrapped()
    {
        Assert.False((object)NonNullType.Of(InterfaceType) is InterfaceType);
        Assert.False((object)NonNullType.Of(InterfaceType) is IInterfaceType);
    }

    [Fact]
    public void IsInterfaceType_FalseForNonInterfaceType()
    {
        Assert.False((object)ObjectType is InterfaceType);
        Assert.False((object)ObjectType is IInterfaceType);
    }

    [Fact]
    public void IsUnionType_TrueForUnionType()
    {
        Assert.True(UnionType is UnionType);
        Assert.True(UnionType is IUnionType);
    }

    [Fact]
    public void IsUnionType_FalseForWrapped()
    {
        Assert.False((object)NonNullType.Of(UnionType) is UnionType);
        Assert.False((object)NonNullType.Of(UnionType) is IUnionType);
    }

    [Fact]
    public void IsUnionType_FalseForNonUnionType()
    {
        Assert.False((object)ObjectType is UnionType);
        Assert.False((object)ObjectType is IUnionType);
    }


    [Fact]
    public void IsEnumType_TrueForEnumType()
    {
        Assert.True(EnumType is EnumType);
        Assert.True(EnumType is IEnumType);
    }

    [Fact]
    public void IsEnumType_FalseForWrapped()
    {
        Assert.False((object)NonNullType.Of(EnumType) is EnumType);
        Assert.False((object)NonNullType.Of(EnumType) is IEnumType);
    }

    [Fact]
    public void IsEnumType_FalseForNonEnumType()
    {
        Assert.False((object)ObjectType is EnumType);
        Assert.False((object)ObjectType is IEnumType);
    }


    [Fact]
    public void IsInputObjectType_TrueForInputObjectType()
    {
        Assert.True(InputObjectType is InputObjectType);
        Assert.True(InputObjectType is IInputObjectType);
    }

    [Fact]
    public void IsInputObjectType_FalseForWrapped()
    {
        Assert.False((object)NonNullType.Of(InputObjectType) is InputObjectType);
        Assert.False((object)NonNullType.Of(InputObjectType) is IInputObjectType);
    }

    [Fact]
    public void IsInputObjectType_FalseForNonInputObjectType()
    {
        Assert.False((object)ObjectType is InputObjectType);
        Assert.False((object)ObjectType is IInputObjectType);
    }

    [Fact]
    public void IsList_TrueForWrappedInputType()
    {
        Assert.True(ListType.Of(SpecScalars.String) is ListType);
        Assert.True(ListType.Of(SpecScalars.String) is IListType);
        Assert.True(ListType.Of(SpecScalars.String) is ListType);
        Assert.True(ListType.Of(SpecScalars.String) is IListType);
    }

    [Fact]
    public void IsList_TrueForWrappedOutputType()
    {
        Assert.True(ListType.Of(SpecScalars.String) is ListType);
        Assert.True(ListType.Of(SpecScalars.String) is IListType);
        Assert.True(ListType.Of(SpecScalars.String) is ListType);
        Assert.True(ListType.Of(SpecScalars.String) is IListType);
    }

    [Fact]
    public void IsList_FalseForUnwrappedType()
    {
        Assert.False((object)ObjectType is ListType);
        Assert.False((object)ObjectType is IListType);
        Assert.False((object)ObjectType is ListType);
        Assert.False((object)ObjectType is IListType);
        Assert.False((object)ObjectType is ListType);
        Assert.False((object)ObjectType is IListType);
    }

    [Fact]
    public void IsNonNull_TrueForWrappedInputType()
    {
        Assert.True(NonNullType.Of(SpecScalars.String) is NonNullType);
        Assert.True(NonNullType.Of(SpecScalars.String) is INonNullType);
        Assert.True(NonNullType.Of(SpecScalars.String) is NonNullType);
        Assert.True(NonNullType.Of(SpecScalars.String) is INonNullType);
    }

    [Fact]
    public void IsNonNull_TrueForWrappedOutputType()
    {
        Assert.True(NonNullType.Of(SpecScalars.String) is NonNullType);
        Assert.True(NonNullType.Of(SpecScalars.String) is INonNullType);
        Assert.True(NonNullType.Of(SpecScalars.String) is NonNullType);
        Assert.True(NonNullType.Of(SpecScalars.String) is INonNullType);
    }

    [Fact]
    public void IsNonNull_FalseForUnwrappedType()
    {
        Assert.False((object)ObjectType is NonNullType);
        Assert.False((object)ObjectType is INonNullType);
        Assert.False((object)ObjectType is NonNullType);
        Assert.False((object)ObjectType is INonNullType);
        Assert.False((object)ObjectType is NonNullType);
        Assert.False((object)ObjectType is INonNullType);
    }

    [Fact]
    public void IsInputType_TrueForInputType()
    {
        Assert.True(InputObjectType.IsInputType());
    }

    [Fact]
    public void IsInputType_TrueForWrappedInputType()
    {
        Assert.True(ListType.Of(InputObjectType).IsInputType());
        Assert.True(NonNullType.Of(InputObjectType).IsInputType());
    }

    [Fact]
    public void IsInputType_FalseForOutputType()
    {
        Assert.False(ObjectType.IsInputType());
    }

    [Fact]
    public void IsInputType_FalseForWrappedOutputType()
    {
        Assert.False(NonNullType.Of(ObjectType).IsInputType());
        Assert.False(ListType.Of(ObjectType).IsInputType());
    }


    [Fact]
    public void IsOutputType_TrueForOutputType()
    {
        Assert.True(ObjectType.IsOutputType());
    }

    [Fact]
    public void IsOutputType_TrueForWrappedOutputType()
    {
        Assert.True(ListType.Of(ObjectType).IsOutputType());
        Assert.True(NonNullType.Of(ObjectType).IsOutputType());
    }

    [Fact]
    public void IsOutputType_FalseForInputType()
    {
        Assert.False(InputObjectType.IsOutputType());
    }

    [Fact]
    public void IsOutputType_FalseForWrappedInputType()
    {
        Assert.False(NonNullType.Of(InputObjectType).IsOutputType());
        Assert.False(ListType.Of(InputObjectType).IsOutputType());
    }

    [Fact]
    public void IsLeafType_TrueForScalarsAndEnums()
    {
        Assert.True(ScalarType is ILeafType);
        Assert.True(EnumType is ILeafType);
    }

    [Fact]
    public void IsLeafType_FalseForWrappedLeafType()
    {
        Assert.False((object)NonNullType.Of(ScalarType) is ILeafType);
    }

    [Fact]
    public void IsLeafType_FalseForNonLeafTypes()
    {
        Assert.False((object)ObjectType is ILeafType);
    }

    [Fact]
    public void IsLeafType_FalseForWrappedNonLeafTypes()
    {
        Assert.False((object)NonNullType.Of(InputObjectType) is ILeafType);
    }

    [Fact]
    public void IsCompositeType_TrueForObjectInterfaceAndUnionTypes()
    {
        Assert.True(ObjectType is ICompositeType);
        Assert.True(InterfaceType is ICompositeType);
        Assert.True(UnionType is ICompositeType);
    }

    [Fact]
    public void IsCompositeType_FalseForWrappedCompositeType()
    {
        Assert.False((object)ListType.Of(ObjectType) is ICompositeType);
    }

    [Fact]
    public void IsCompositeType_FalseForNonCompositeType()
    {
        Assert.False((object)InputObjectType is ICompositeType);
    }

    [Fact]
    public void IsCompositeType_FalseForWrappedNonCompositeType()
    {
        Assert.False((object)ListType.Of(InputObjectType) is ICompositeType);
    }


    [Fact]
    public void IsAbstractType_TrueForInterfaceAndUnionTypes()
    {
        Assert.True(InterfaceType is IAbstractType);
        Assert.True(UnionType is IAbstractType);
    }

    [Fact]
    public void IsAbstractType_FalseForWrappedAbstractType()
    {
        Assert.False((object)ListType.Of(UnionType) is IAbstractType);
    }

    [Fact]
    public void IsAbstractType_FalseForNonAbstractType()
    {
        Assert.False((object)InputObjectType is IAbstractType);
    }

    [Fact]
    public void IsAbstractType_FalseForWrappedNonAbstractType()
    {
        Assert.False((object)ListType.Of(InputObjectType) is IAbstractType);
    }

    [Fact]
    public void IsWrappingType_TrueForListAndNonNullTypes()
    {
        Assert.True(ListType.Of(InputObjectType) is IWrappingType);
        Assert.True(NonNullType.Of(InputObjectType) is IWrappingType);
        Assert.True(ListType.Of(ObjectType) is IWrappingType);
        Assert.True(NonNullType.Of(ObjectType) is IWrappingType);
    }

    [Fact]
    public void IsNullable_TrueForUnwrappedTypes()
    {
        Assert.True(ObjectType is INullableType);
    }

    [Fact]
    public void IsNullable_TrueListOfNonNullTYpes()
    {
        Assert.True(ListType.Of(NonNullType.Of(ObjectType)) is INullableType);
    }

    [Fact]
    public void IsNullable_FalseForNonNullTypes()
    {
        Assert.False((object)NonNullType.Of(ObjectType) is INullableType);
        Assert.False((object)NonNullType.Of(InputObjectType) is INullableType);
    }

    [Fact]
    public void GetNullableType_ReturnsNullForNoType()
    {
        // ReSharper disable once ExpressionIsAlwaysNull
        Assert.Null(((IGraphQLType)null!).GetNullableType());
    }

    [Fact]
    public void GetNullableType_ReturnsSelfForNullableType()
    {
        Assert.Equal(ObjectType, ObjectType.GetNullableType().GetNullableType());
        var listOfObject = ListType.Of(ObjectType);
        Assert.Equal(listOfObject, listOfObject.GetNullableType());
    }

    [Fact]
    public void GetNullableType_UnwrapsNonNullType()
    {
        Assert.Equal(ObjectType, NonNullType.Of(ObjectType).GetNullableType());
    }

    [Fact]
    public void IsNamedType_TrueForUnwrappedTypes()
    {
        Assert.True(ObjectType is INamedType);
    }

    [Fact]
    public void IsNamedType_FalseForListAndNonNullTypes()
    {
        Assert.False((object)NonNullType.Of(ObjectType) is INamedType);
        Assert.False((object)NonNullType.Of(InputObjectType) is INamedType);
        Assert.False((object)ListType.Of(ObjectType) is INamedType);
        Assert.False((object)ListType.Of(InputObjectType) is INamedType);
    }

    [Fact]
    public void GetNamedType_ReturnsNullForNoType()
    {
        // ReSharper disable once ExpressionIsAlwaysNull
        Assert.Null(((IGraphQLType)null!).GetNamedType());
    }

    [Fact]
    public void GetNamedType_ReturnsSelfForAnUnwrappedType()
    {
        Assert.Equal(ObjectType, ObjectType.GetNamedType());
    }

    [Fact]
    public void GetNamedType_UnwrapsWrapperTypes()
    {
        Assert.Equal(ObjectType, NonNullType.Of(ObjectType).GetNamedType());
        Assert.Equal(ObjectType, ListType.Of(ObjectType).GetNamedType());
    }

    [Fact]
    public void GetNamedType_UnwrapsDeeplyWrapperTypes()
    {
        Assert.Equal(InputObjectType,
            NonNullType.Of(ListType.Of(NonNullType.Of(InputObjectType))).GetNamedType());
    }
}
