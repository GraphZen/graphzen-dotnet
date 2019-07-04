// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;

using Xunit;

namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class EnumTypeBuilderTests
    {
        private enum FooEnum
        {
            [Description("bar desc")] Bar,
            [GraphQLName("customBaz")] Baz
        }


        [Fact]
        public void EnumCreatedWithClrTypeInfersValues()
        {
            var schema = Schema.Create(sb => sb.Enum<FooEnum>());
            var values = schema.FindType<EnumType>(typeof(FooEnum)).Values;
            values.Count.Should().Be(2);
            values[0].Name.Should().Be(nameof(FooEnum.Bar));
            values[0].Description.Should().Be("bar desc");
            values[1].Name.Should().Be("customBaz");
        }
    }
}