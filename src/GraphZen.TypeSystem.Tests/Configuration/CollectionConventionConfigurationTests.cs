// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
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
                CollectionConventionContext ctx = default;

                var schema = Schema.Create(sb =>
                {
                    ctx = fixture.ConfigureViaConvention(sb);
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
                CollectionConventionContext ctx = default;

                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    ctx = fixture.ConfigureViaConvention(sb);
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
                    defCollection[explicitName].GetConfigurationSource().Should().Be(ConfigurationSource.Convention);
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
                CollectionConventionContext ctx = default;


                var schema = Schema.Create(sb =>
                {
                    ctx = fixture.ConfigureViaConvention(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation);
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
                CollectionConventionContext ctx = default;
                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    ctx = fixture.ConfigureViaConvention(sb);
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
                    defCollection[explicitName].GetConfigurationSource().Should().Be(ConfigurationSource.Convention);
                    defCollection[explicitName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[explicitName].Name.Should().Be(explicitName);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[explicitName].Should().NotBeNull();
                collection[explicitName].Name.Should().Be(explicitName);
            });
        }
    }
}