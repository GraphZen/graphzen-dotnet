// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.TypeSystem
{
    public abstract class NameConfigurationByDataAnnotationTests
    {
        public abstract string ConventionalName { get; }
        public abstract void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder);
        public abstract void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder);

        public abstract IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder);
        public abstract IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder);
        public abstract INamed GetMemberNamedByConvention(Schema schema);
        public abstract INamed GetMemberNamedByDataAnnotation(Schema schema);

        [Fact]
        public void name_set_by_convention()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberNamedByConvention(_);
                var def = GetMemberDefinitionNamedByConvention(_);
                def.GetNameConfigurationSource().Should().Be(ConfigurationSource.Convention);
                def.Name.Should().Be(ConventionalName);
            });
            var item = GetMemberNamedByConvention(schema);
            item.Name.Should().Be(ConventionalName);
        }

        [Fact]
        public void name_set_by_data_annotation()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberWithCustomNameAttribute(_);
                var def = GetMemberDefinitionWithCustomNameDataAnnotation(_);
                def.GetNameConfigurationSource().Should().Be(ConfigurationSource.DataAnnotation);
                def.Name.Should().Be("CustomName");
            });
            var item = GetMemberNamedByDataAnnotation(schema);
            item.Name.Should().Be("CustomName");
        }
    }
}