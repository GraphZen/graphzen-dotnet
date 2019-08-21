// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Objects.Fields;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using Xunit;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace GraphZen
{
    [NoReorder]
    public class CollectionConventionConfigurationTests : FixtureRunner<ICollectionConventionConfigurationFixture>
    {
        protected override IEnumerable<ICollectionConventionConfigurationFixture> GetFixtures() =>
            ConfigurationFixtures.GetAll<ICollectionConventionConfigurationFixture>();

        [Fact]
        public void item_added_by_convention_with_name_configured_by_convention()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();

                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
                    defCollection[ctx.ItemNamedByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByConvention].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByConvention].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemNamedByConvention].Should().NotBeNull();
                collection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
            });
        }

        [Fact]
        public void item_added_by_convention_with_name_configured_by_convention_renamed_explicitly()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();

                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
                    defCollection[ctx.ItemNamedByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByConvention].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByConvention].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    fixture.RenameItem(sb, ctx.ParentName, ctx.ItemNamedByConvention, explicitName);
                    defCollection.ContainsKey(ctx.ItemNamedByConvention).Should().BeFalse();
                    defCollection[explicitName].Should().NotBeNull();
                    defCollection[explicitName].GetConfigurationSource().Should().Be(ConfigurationSource.Explicit, $"items explicitly re-named will be considered explicitly configured.");
                    defCollection[explicitName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[explicitName].Name.Should().Be(explicitName);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[explicitName].Should().NotBeNull();
                collection[explicitName].Name.Should().Be(explicitName);
            });
        }

        [Fact]
        public void item_added_by_convention_with_name_configured_by_data_annotation()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();

                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation, $"these are the items in the collection: {defCollection}");
                    defCollection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByDataAnnotation].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByDataAnnotation].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.DataAnnotation);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                collection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation);
            });
        }

        [Fact]
        public void item_added_by_convention_with_name_configured_by_data_annotation_renamed_explicitly()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation);
                    defCollection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByDataAnnotation].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByDataAnnotation].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.DataAnnotation);
                    fixture.RenameItem(sb, ctx.ParentName, ctx.ItemNamedByConvention, explicitName);
                    defCollection.ContainsKey(ctx.ItemNamedByConvention).Should().BeFalse();
                    defCollection[explicitName].Should().NotBeNull();
                    defCollection[explicitName].GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[explicitName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[explicitName].Name.Should().Be(explicitName);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[explicitName].Should().NotBeNull();
                collection[explicitName].Name.Should().Be(explicitName);
            });
        }

        [Fact]
        public void item_ignored_by_convention()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();

                    //fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName,
                    //    ctx.ItemIgnoredByConvention).Should().Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();
            });
        }

        [Fact]
        public void item_ignored_by_convention_added_by_explicit_configuration()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();
                    fixture.AddItem(sb, ctx.ParentName, ctx.ItemIgnoredByConvention);
                    defCollection[ctx.ItemIgnoredByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemIgnoredByConvention].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection[ctx.ItemIgnoredByConvention].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemIgnoredByConvention].Should().NotBeNull();
                collection[ctx.ItemIgnoredByConvention].Name.Should().Be(ctx.ItemIgnoredByConvention);
            });
        }

        [Fact]
        public void item_ignored_by_data_annotation()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
                    fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName,
                        ctx.ItemIgnoredByDataAnnotation).Should().Be(ConfigurationSource.DataAnnotation);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
            });
        }

        [Fact]
        public void item_ignored_by_data_annotation_added_by_explicit_configuration()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
                    fixture.AddItem(sb, ctx.ParentName, ctx.ItemIgnoredByDataAnnotation);
                    defCollection[ctx.ItemIgnoredByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemIgnoredByDataAnnotation].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection[ctx.ItemIgnoredByDataAnnotation].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemIgnoredByDataAnnotation].Should().NotBeNull();
                collection[ctx.ItemIgnoredByDataAnnotation].Name.Should().Be(ctx.ItemIgnoredByDataAnnotation);
            });
        }

        [Fact]
        public void when_parent_configured_explicitly_then_clr_member_set_items_added_by_convention()
        {
            TestFixtures(fixture =>
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