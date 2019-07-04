// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

using Xunit;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    public abstract class DefaultValueConfigurationTests
    {
        [Fact]
        public void no_default_value_set_by_convention()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMembersByConvention(_);
                var def = GetMemberDefinitionWithNoDefaultValueByConvention(_);
                def.HasDefaultValue.Should().BeFalse();
                def.DefaultValue.Should().BeNull();
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.Convention);
            });
            var inputValue = GetMemberWithNoDefaultValueByConvention(schema);
            inputValue.HasDefaultValue.Should().BeFalse();
            inputValue.DefaultValue.Should().BeNull();
        }

        [Fact]
        public void no_default_value_set_by_convention_overridden_by_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMembersByConvention(_);
                var def = GetMemberDefinitionWithNoDefaultValueByConvention(_);
                def.HasDefaultValue.Should().BeFalse();
                def.DefaultValue.Should().BeNull();
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.Convention);
                SetDefaultValueOnMemberWithNoDefaultValueByConvention(_, "explicit default value");
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                def.HasDefaultValue.Should().BeTrue();
                def.DefaultValue.Should().Be("explicit default value");
            });
            var inputValue = GetMemberWithNoDefaultValueByConvention(schema);
            inputValue.HasDefaultValue.Should().BeTrue();
            inputValue.DefaultValue.Should().Be("explicit default value");
        }


        [Fact]
        public void default_value_set_by_data_annotation()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMembersByConvention(_);
                var def = GetMemberDefinitionWithDefaultValueConfiguredByDataAnnotation(_);
                def.HasDefaultValue.Should().BeTrue();
                def.DefaultValue.Should().Be("default value set by attribute");
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.DataAnnotation);
            });
            var inputValue = GetMemberWithDefaultValueSetByDataAnnotation(schema);
            inputValue.HasDefaultValue.Should().BeTrue();
            inputValue.DefaultValue.Should().Be("default value set by attribute");
        }

        [Fact]
        public void default_value_set_by_data_annotation_configured_by_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMembersByConvention(_);
                var def = GetMemberDefinitionWithDefaultValueConfiguredByDataAnnotation(_);
                def.HasDefaultValue.Should().BeTrue();
                def.DefaultValue.Should().Be("default value set by attribute");
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.DataAnnotation);

                SetDefaultValueOnMemberWithDefaultValueSetByAttribute(_, "explicit default value");
                def.HasDefaultValue.Should().BeTrue();
                def.GetDefaultValueConfigurationSource().Should().Be(ConfigurationSource.Explicit);
            });
            var inputValue = GetMemberWithDefaultValueSetByDataAnnotation(schema);
            inputValue.HasDefaultValue.Should().BeTrue();
            inputValue.DefaultValue.Should().Be("explicit default value");
        }


        public abstract void CreateMembersByConvention(SchemaBuilder schemaBuilder);

        public abstract InputValueDefinition GetMemberDefinitionWithNoDefaultValueByConvention(
            SchemaBuilder schemaBuilder);


        public abstract InputValueDefinition GetMemberDefinitionWithDefaultValueConfiguredByDataAnnotation(
            SchemaBuilder schemaBuilder);


        public abstract void SetDefaultValueOnMemberWithNoDefaultValueByConvention(SchemaBuilder schemaBuilder,
            object defaultValue);


        public abstract void SetDefaultValueOnMemberWithDefaultValueSetByAttribute(SchemaBuilder schemaBuilder,
            object defaultValue);


        public abstract InputValue GetMemberWithNoDefaultValueByConvention(Schema schema);

        public abstract InputValue GetMemberWithDefaultValueSetByDataAnnotation(Schema schema);
    }
}