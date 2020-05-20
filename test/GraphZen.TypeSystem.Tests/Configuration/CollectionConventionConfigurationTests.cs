// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests.Configuration
{
    [NoReorder]
    public class
        CollectionConventionConfigurationTests : TestDataFixtureRunner<ICollectionConventionConfigurationFixture>
    {
        public static IEnumerable<object[]> FixtureData =
            ConfigurationFixtures.GetAll<ICollectionConventionConfigurationFixture>().ToTestData();


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void conventional_parent_should_be_of_expected_type(ICollectionConventionConfigurationFixture fixture)
        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    fixture.GetParent(sb, context.ParentName).Should().BeOfType(fixture.ParentMemberDefinitionType);
                });
                fixture.GetParent(schema, context.ParentName).Should().BeOfType(fixture.ParentMemberType);
            });
        }


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void conventional_item_added_explicitly_via_clr_member_should_have_clr_member(
            ICollectionConventionConfigurationFixture fixture)
        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, context.ParentName);
                    defCollection[context.ItemNamedByConvention].Should()
                        .BeOfType(fixture.CollectionItemMemberDefinitionType);
                    defCollection[context.ItemNamedByDataAnnotation].Should()
                        .BeOfType(fixture.CollectionItemMemberDefinitionType);
                });
                fixture.GetParent(schema, context.ParentName).Should().BeOfType(fixture.ParentMemberType);
                var collection = fixture.GetCollection(schema, context.ParentName);
                collection[context.ItemNamedByConvention].Should()
                    .BeOfType(fixture.CollectionItemMemberType);
                collection[context.ItemNamedByDataAnnotation].Should()
                    .BeOfType(fixture.CollectionItemMemberType);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void conventional_item_should_be_of_expected_type(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, context.ParentName);
                    defCollection[context.ItemNamedByConvention].Should()
                        .BeOfType(fixture.CollectionItemMemberDefinitionType);
                    defCollection[context.ItemNamedByDataAnnotation].Should()
                        .BeOfType(fixture.CollectionItemMemberDefinitionType);
                });
                fixture.GetParent(schema, context.ParentName).Should().BeOfType(fixture.ParentMemberType);
                var collection = fixture.GetCollection(schema, context.ParentName);
                collection[context.ItemNamedByConvention].Should()
                    .BeOfType(fixture.CollectionItemMemberType);
                collection[context.ItemNamedByDataAnnotation].Should()
                    .BeOfType(fixture.CollectionItemMemberType);
            });
        }


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void parent_should_be_of_expected_type(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    fixture.GetParent(sb, parentName).Should().BeOfType(fixture.ParentMemberDefinitionType);
                });
                fixture.GetParent(schema, parentName).Should().BeOfType(fixture.ParentMemberType);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_added_by_conventional_name(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();

                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    //fixture.GetParent(sb, ctx.ParentName).GetType().Should().Be(fixture.Pa)
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
                    defCollection[ctx.ItemNamedByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByConvention].As<IMutableDefinition>().GetConfigurationSource().Should().Be(ctx.DefaultItemConfigurationSource ?? ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByConvention].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemNamedByConvention].Should().NotBeNull();
                collection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
            });
        }


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_added_by_convention_with_name_configured_by_convention_renamed_explicitly(
            ICollectionConventionConfigurationFixture fixture)
        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
                    defCollection[ctx.ItemNamedByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByConvention].As<IMutableDefinition>().GetConfigurationSource().Should().Be(ctx.DefaultItemConfigurationSource ?? ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByConvention].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    fixture.RenameItem(sb, ctx.ParentName, ctx.ItemNamedByConvention, explicitName);
                    defCollection.ContainsKey(ctx.ItemNamedByConvention).Should().BeFalse();
                    defCollection[explicitName].Should().NotBeNull();
                    defCollection[explicitName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[explicitName].Name.Should().Be(explicitName);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[explicitName].Should().NotBeNull();
                collection[explicitName].Name.Should().Be(explicitName);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_added_by_convention_with_name_configured_by_data_annotation(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();

                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation,
                        $"these are the items in the collection: {defCollection}");
                    defCollection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByDataAnnotation].As<IMutableDefinition>().GetConfigurationSource().Should().Be(ctx.DefaultItemConfigurationSource ?? ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByDataAnnotation].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.DataAnnotation);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                collection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_added_by_convention_with_name_configured_by_data_annotation_renamed_explicitly(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation);
                    defCollection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByDataAnnotation].As<IMutableDefinition>().GetConfigurationSource().Should().Be(ctx.DefaultItemConfigurationSource ?? ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByDataAnnotation].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.DataAnnotation);
                    fixture.RenameItem(sb, ctx.ParentName, ctx.ItemNamedByConvention, explicitName);
                    defCollection.ContainsKey(ctx.ItemNamedByConvention).Should().BeFalse();
                    defCollection[explicitName].Should().NotBeNull();
                    defCollection[explicitName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[explicitName].Name.Should().Be(explicitName);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[explicitName].Should().NotBeNull();
                collection[explicitName].Name.Should().Be(explicitName);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_ignored_by_convention(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();

                    //fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName,
                    //    ctx.ItemIgnoredByConvention).Should().Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_ignored_by_convention_added_by_explicit_configuration(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();
                    fixture.AddItem(sb, ctx.ParentName, ctx.ItemIgnoredByConvention);
                    defCollection[ctx.ItemIgnoredByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemIgnoredByConvention].As<IMutableDefinition>().GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[ctx.ItemIgnoredByConvention].As<IMutableDefinition>().GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemIgnoredByConvention].Should().NotBeNull();
                collection[ctx.ItemIgnoredByConvention].Name.Should().Be(ctx.ItemIgnoredByConvention);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_ignored_by_data_annotation(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
                    fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName,
                        ctx.ItemIgnoredByDataAnnotation).Should().Be(ConfigurationSource.DataAnnotation);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_ignored_by_data_annotation_added_by_explicit_configuration(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
                    fixture.AddItem(sb, ctx.ParentName, ctx.ItemIgnoredByDataAnnotation);
                    defCollection[ctx.ItemIgnoredByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemIgnoredByDataAnnotation].As<IMutableDefinition>().GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[ctx.ItemIgnoredByDataAnnotation].As<IMutableDefinition>().GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemIgnoredByDataAnnotation].Should().NotBeNull();
                collection[ctx.ItemIgnoredByDataAnnotation].Name.Should().Be(ctx.ItemIgnoredByDataAnnotation);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void when_parent_configured_explicitly_then_clr_member_set_items_added_by_convention(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, ctx.ParentName);
                    fixture.ConfigureClrContext(sb, ctx.ParentName);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
                    defCollection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();
                    defCollection[ctx.ItemNamedByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                    fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName, ctx.ItemIgnoredByDataAnnotation)
                        .Should()
                        .Be(ConfigurationSource.DataAnnotation);
                    //fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName, ctx.ItemIgnoredByConvention).Should()
                    //                        .Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
                collection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();
                collection[ctx.ItemNamedByConvention].Should().NotBeNull();
                collection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
            });
        }
    }
}