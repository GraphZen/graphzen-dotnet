// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Language.Internal;
using GraphZen.Types.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.Infrastructure
{
    [NoReorder]
    public class ClrTypeExtensionTests
    {
        private class Foo
        {
        }

        [GraphQLName("BAZ")]
        // ReSharper disable once UnusedMember.Local
        private class Bar
        {
        }

        [Theory]
        [InlineData(typeof(int), false, null)]
        [InlineData(typeof(int?), true, typeof(int))]
        public void TryGetNullableType_ShouldGetNullableType(Type input, bool isNullable, Type expectedNullableType)
        {
            var result = input.TryGetNullableType(out var nullableClrType);
            result.Should().Be(isNullable);
            if (result)
            {
                nullableClrType.Should().Be(expectedNullableType);
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
        public void try_get_list_item_type_should_get_item_time(Type maybeListType, Type expectedItemType)
        {
            maybeListType.TryGetListItemType(out var itemType).Should().Be(expectedItemType != null);
            itemType.Should().Be(expectedItemType);
        }

        [Theory]
        [InlineData(typeof(Task<string>), typeof(string))]
        [InlineData(typeof(Task), null)]
        [InlineData(typeof(string), null)]
        [InlineData(typeof(Task<Dictionary<string, string>>), typeof(Dictionary<string, string>))]
        public void try_get_task_result_type_should_get_task_result_type(Type clrType, Type expectedTaskType)
        {
            clrType.TryGetTaskResultType(out var resultType)
                .Should().Be(expectedTaskType != null);

            resultType.Should().Be(expectedTaskType);
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
            clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType, canBeNull, itemCanBeNull).Should()
                .BeTrue();
            typeNode.Should().Be(expectedTypeNode);
            innerClrType.Should().Be(expectedInnerType);
        }

        [Theory]
        [InlineData(typeof(Dictionary<string, string>))]
        public void try_get_graphql_type_info_returns_false_for_generic_inner_type(Type clrType)
        {
            clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType).Should()
                .BeFalse();
            typeNode.Should().Be(null);
            innerClrType.Should().Be(null);
        }
    }
}