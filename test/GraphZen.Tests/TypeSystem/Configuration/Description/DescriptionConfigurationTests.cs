// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

using Xunit;

namespace GraphZen.TypeSystem
{
    public abstract class DescriptionConfigurationTests
    {
        public abstract void CreateMemberWithoutDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder);
        public abstract void CreateMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder);
        public abstract MemberDefinition GetMemberDefinitionWithoutDataAnnotation(SchemaDefinition schemaDef);
        public abstract MemberDefinition GetMemberDefinitionWithDataAnnotation(SchemaDefinition schemaDef);
        public abstract Member GetMemberWithoutDataAnnotation(Schema schema);
        public abstract Member GetMemberWithDataAnnotation(Schema schema);

        public abstract void SetDescriptionOnMemberWithDataAnnotation(ISchemaBuilder<GraphQLContext> schemaBuilder,
            string description);

        [Fact]
        public void description_set_by_data_annotation()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberWithDataAnnotation(_);
                GetMemberDefinitionWithDataAnnotation(_.GetDefinition()).GetDescriptionConfigurationSource()
                    .Should()
                    .Be(ConfigurationSource.DataAnnotation);
            });
            GetMemberWithDataAnnotation(schema)
                .Description
                .Should().Be("set by data annotation");
        }

        [Fact]
        public void description_set_by_data_annotation_overridden_by_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberWithDataAnnotation(_);
                GetMemberDefinitionWithDataAnnotation(_.GetDefinition()).GetDescriptionConfigurationSource()
                    .Should()
                    .Be(ConfigurationSource.DataAnnotation);
                SetDescriptionOnMemberWithDataAnnotation(_, "set by explicit configuration");
                GetMemberDefinitionWithDataAnnotation(_.GetDefinition()).GetDescriptionConfigurationSource()
                    .Should()
                    .Be(ConfigurationSource.Explicit);
            });

            GetMemberWithDataAnnotation(schema).Description
                .Should().Be("set by explicit configuration");
        }


        [Fact]
        public void description_set_by_data_annotation_removed_by_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberWithDataAnnotation(_);
                GetMemberDefinitionWithDataAnnotation(_.GetDefinition())
                    .GetDescriptionConfigurationSource()
                    .Should()
                    .Be(ConfigurationSource.DataAnnotation);
                SetDescriptionOnMemberWithDataAnnotation(_, null);
                GetMemberDefinitionWithDataAnnotation(_.GetDefinition()).GetDescriptionConfigurationSource()
                    .Should()
                    .Be(ConfigurationSource.Explicit);
            });

            GetMemberWithDataAnnotation(schema).Description.Should().Be(null);
        }

        [Fact]
        public void no_description_by_default()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberWithoutDataAnnotation(_);
                GetMemberDefinitionWithoutDataAnnotation(_.GetDefinition()).GetDescriptionConfigurationSource()
                    .Should()
                    .BeNull();
            });
            GetMemberWithoutDataAnnotation(schema).Description.Should().BeNull();
        }
    }
}