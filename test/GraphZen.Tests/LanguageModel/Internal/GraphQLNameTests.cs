// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.LanguageModel.Internal
{
    public class GraphQLNameTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("0", false)]
        [InlineData("schemaBuilder", true)]
        [InlineData("abc", true)]
        [InlineData("abc`", false)]
        [InlineData("0_aa0", false)]
        [InlineData("_aa0", true)]
        [InlineData("_aa", true)]
        [InlineData("aa", true)]
        [InlineData("a", true)]
        [InlineData("a`", false)]
        public void graphql_name_should_match_spec(string name, bool isValid)
        {
            name.IsValidGraphQLName().Should().Be(isValid);
        }


        public class Foo
        {
        }


        public class Bar
        {
        }
    }
}