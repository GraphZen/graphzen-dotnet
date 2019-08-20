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
    public class ConventionalCollectionConfigurationTests : FixtureRunner<IConventionalCollectionConfigurationFixture>
    {
        protected override IEnumerable<IConventionalCollectionConfigurationFixture> GetFixtures() =>
            ConfigurationFixtures.GetAll<IConventionalCollectionConfigurationFixture>();

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
        public void item_added_by_convention_with_conventional_name()
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
    }
}