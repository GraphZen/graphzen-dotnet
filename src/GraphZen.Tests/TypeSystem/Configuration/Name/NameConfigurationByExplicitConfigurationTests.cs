// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using Xunit;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    public abstract class NameConfigurationByExplicitConfigurationTests : NameConfigurationByDataAnnotationTests
    {
        [Fact]
        public void name_set_by_convention_overridden_by_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberNamedByConvention(_);
                var def = GetMemberDefinitionNamedByConvention(_);
                SetNameOnMemberNamedByConvention(_, "ExplicitName");
                def.GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                def.Name.Should().Be("ExplicitName");
            });
            var item = GetMemberNamedByConvention(schema);
            item.Name.Should().Be("ExplicitName");
        }

        [Fact]
        public void name_set_by_convention_overridden_by_explicit_configuration_with_same_name()
        {
            string name = default;
            var schema = Schema.Create(_ =>
            {
                CreateMemberNamedByConvention(_);
                var def = GetMemberDefinitionNamedByConvention(_);
                name = def.Name;
                SetNameOnMemberNamedByConvention(_, name);
                def.GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                def.Name.Should().Be(name);
            });
            var item = GetMemberNamedByConvention(schema);
            item.Name.Should().Be(name);
        }


        [Fact]
        public void name_set_by_data_annotation_overridden_by_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberWithCustomNameAttribute(_);
                var def = GetMemberDefinitionWithCustomNameDataAnnotation(_);
                SetNameOnMemberNamedByDataAnnotation(_, "ExplicitName");
                def.GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                def.Name.Should().Be("ExplicitName");
            });
            var item = GetMemberNamedByDataAnnotation(schema);
            item.Name.Should().Be("ExplicitName");
        }

        [Fact]
        public void name_set_by_data_annotation_overridden_by_explicit_configuration_with_same_name()
        {
            string name = default;
            var schema = Schema.Create(_ =>
            {
                CreateMemberWithCustomNameAttribute(_);
                var def = GetMemberDefinitionWithCustomNameDataAnnotation(_);
                name = def.Name;
                SetNameOnMemberNamedByDataAnnotation(_, name);
                def.GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                def.Name.Should().Be(name);
            });
            var item = GetMemberNamedByDataAnnotation(schema);
            item.Name.Should().Be(name);
        }

        public abstract void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name);
        public abstract void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name);
    }
}