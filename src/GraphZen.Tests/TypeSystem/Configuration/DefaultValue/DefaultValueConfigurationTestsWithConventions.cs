#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using Xunit;

namespace GraphZen.TypeSystem
{
    public abstract class DefaultValueConfigurationTestsWithConventions : DefaultValueConfigurationTests
    {
        public abstract InputValueDefinition GetMemberDefinitionWithDefaultValueConfiguredByConvention(
            SchemaBuilder schemaBuilder);

        public abstract void SetDefaultValueOnMemberWithDefaultValueSetByConvention(SchemaBuilder schemaBuilder,
            object defaultValue);

        public abstract InputValue GetMemberWithDefaultValueSetByConvention(Schema schema);

        [Fact]
        public void default_value_set_by_convention()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMembersByConvention(_);
                var def = GetMemberDefinitionWithDefaultValueConfiguredByConvention(_);
                def.HasDefaultValue.Should().BeTrue();
                def.DefaultValue.Should().Be("default value set by convention");
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.Convention);
            });
            var inputValue = GetMemberWithDefaultValueSetByConvention(schema);
            inputValue.HasDefaultValue.Should().BeTrue();
            inputValue.DefaultValue.Should().Be("default value set by convention");
        }

        [Fact]
        public void default_value_set_by_convention_overridden_by_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMembersByConvention(_);
                var def = GetMemberDefinitionWithDefaultValueConfiguredByConvention(_);
                def.HasDefaultValue.Should().BeTrue();
                def.DefaultValue.Should().Be("default value set by convention");
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.Convention);
                SetDefaultValueOnMemberWithDefaultValueSetByConvention(_, "explicit default value");
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                def.HasDefaultValue.Should().BeTrue();
                def.DefaultValue.Should().Be("explicit default value");
            });
            var inputValue = GetMemberWithDefaultValueSetByConvention(schema);
            inputValue.HasDefaultValue.Should().BeTrue();
            inputValue.DefaultValue.Should().Be("explicit default value");
        }
    }
}