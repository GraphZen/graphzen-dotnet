// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Objects
{
    [Subject(nameof(Schema), nameof(Schema.Objects))]
    [NoReorder]
    public class ObjectTypesCollectionTests
    {
        [Fact]
        public void object_may_be_added_via_object_type_builder()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.GetDefinition().GetObject("Foo").GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
            });
            schema.GetObject("Foo").Should().NotBeNull();
        }

        [Fact]
        public void object_may_be_added_via_sdl()
        {
            var schema = Schema.Create(_ =>
            {
                _.FromSchema(@"type Foo");
                _.GetDefinition().GetObject("Foo").GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
            });
            schema.GetObject("Foo").Should().NotBeNull();
        }

        [Fact]
        public void added_object_may_be_ignored_via_explicit_configuration()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.IgnoreType("Foo");
                _.GetDefinition().HasObject("Foo").Should().BeFalse();
                _.GetDefinition().FindIgnoredTypeConfigurationSource("Foo").Should().Be(ConfigurationSource.Explicit);
            });
            schema.HasObject("Foo").Should().BeFalse();
        }
    }
}