// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.Tests.Infrastructure.Extensions;

[NoReorder]
public class ClrTypeExtensionTests
{
    private class Foo
    {
    }


    [Theory]
    [InlineData(typeof(int), false, null)]
    [InlineData(typeof(int?), true, typeof(int))]
    public void TryGetNullableType_ShouldGetNullableType(Type input, bool isNullable, Type? expectedNullableType)
    {
        var result = input.TryGetNullableType(out var nullableClrType);
        Assert.Equal(isNullable, result);
        if (result)
        {
            Assert.Equal(expectedNullableType, nullableClrType);
        }
    }

    [Theory]
    [InlineData(typeof(List<string>), typeof(string))]
    [InlineData(typeof(string[]), typeof(string))]
    [InlineData(typeof(ICollection<string>), typeof(string))]
    [InlineData(typeof(IReadOnlyList<string>), typeof(string))]
    [InlineData(typeof(IReadOnlyCollection<string>), typeof(string))]
    [InlineData(typeof(IReadOnlyList<List<string>>), typeof(List<string>))]
    [InlineData(typeof(IReadOnlyList<List<string[]>>), typeof(List<string[]>))]
    [InlineData(typeof(List<List<List<string>>>), typeof(List<List<string>>))]
    [InlineData(typeof(string), null)]
    public void try_get_list_item_type_should_get_item_time(Type maybeListType, Type? expectedItemType)
    {
        Assert.Equal(expectedItemType != null, maybeListType.TryGetListItemType(out var itemType));
        Assert.Equal(expectedItemType, itemType);
    }

    [Theory]
    [InlineData(typeof(Task<string>), typeof(string))]
    [InlineData(typeof(Task), null)]
    [InlineData(typeof(string), null)]
    [InlineData(typeof(Task<Dictionary<string, string>>), typeof(Dictionary<string, string>))]
    public void try_get_task_result_type_should_get_task_result_type(Type clrType, Type? expectedTaskType)
    {
        Assert.Equal(expectedTaskType != null, clrType.TryGetTaskResultType(out var resultType));

        Assert.Equal(expectedTaskType, resultType);
    }


    [Theory]
    [InlineData(typeof(int), typeof(int), "Int32!")]
    [InlineData(typeof(int), typeof(int), "Int32", true)]
    [InlineData(typeof(int?), typeof(int), "Int32")]
    [InlineData(typeof(List<Foo>), typeof(Foo), "[Foo!]!")]
    [InlineData(typeof(List<Foo>), typeof(Foo), "[Foo!]", true)]
    [InlineData(typeof(List<Foo>), typeof(Foo), "[Foo]", true, true)]
    [InlineData(typeof(List<string>), typeof(string), "[String!]!")]
    [InlineData(typeof(List<int>), typeof(int), "[Int32!]!")]
    public void try_get_graphql_type_info_successful_scenarios(Type clrType, Type expectedInnerType,
        string expectedType,
        bool canBeNull = false, bool itemCanBeNull = false)
    {
        var expectedTypeNode = Parser.ParseType(expectedType);
        Assert.True(clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType, canBeNull, itemCanBeNull));
        Assert.Equal(expectedTypeNode, typeNode);
        Assert.Equal(expectedInnerType, innerClrType);
    }

    [Theory]
    [InlineData(typeof(Dictionary<string, string>))]
    public void try_get_graphql_type_info_returns_false_for_generic_inner_type(Type clrType)
    {
        Assert.False(clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType));
        Assert.Null(typeNode);
        Assert.Null(innerClrType);
    }
}
