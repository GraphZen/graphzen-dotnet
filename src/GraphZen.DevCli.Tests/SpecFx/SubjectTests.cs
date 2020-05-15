// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.SpecFx
{
    public class SubjectTests
    {
        public class SpecClass
        {
            public const string FooSpec = nameof(FooSpec);
        }

        [Fact]
        public void WithSpecs_priority_and_string_params_should_add_spec_ref()
        {
            var sut = new Subject("sut");
            var sutWithSpec = sut.WithSpecs(SpecPriority.High, "spec");
            var specRef = sutWithSpec.Specs["spec"];
            specRef.SpecId.Should().Be("spec");
            specRef.Priority.Should().Be(SpecPriority.High);
        }

        [Fact]
        public void WithSpecs_priority_and_string_params_should_update_priority()
        {
            var sut = new Subject("sut");
            var withSpec = sut.WithSpecs(SpecPriority.High, "spec");
            withSpec.Specs["spec"].Priority.Should().Be(SpecPriority.High);
            var withUpdatedPriority = withSpec.WithSpecs(SpecPriority.Medium, "spec");
            withUpdatedPriority.Specs["spec"].Priority.Should().Be(SpecPriority.Medium);
        }

        [Fact]
        public void WithSpecs_string_params_should_add_spec_ref()
        {
            var sut = new Subject("sut");
            var sutWithSpec = sut.WithSpecs("spec");
            var specRef = sutWithSpec.Specs["spec"];
            specRef.SpecId.Should().Be("spec");
            specRef.Priority.Should().Be(SpecPriority.Low);
        }


        [Fact]
        public void WithSpecs_type_param_and_priority_should_add_spec_ref()
        {
            var sut = new Subject("sut");
            var sutWithSpec = sut.WithSpecs<SpecClass>(SpecPriority.High);
            var specRef = sutWithSpec.Specs[nameof(SpecClass.FooSpec)];
            specRef.SpecId.Should().Be(nameof(SpecClass.FooSpec));
            specRef.Priority.Should().Be(SpecPriority.High);
        }

        [Fact]
        public void WithSpecs_type_param_and_priority_should_update_priority()
        {
            var sut = new Subject("sut");
            var original = sut.WithSpecs<SpecClass>(SpecPriority.High);
            original.Specs[nameof(SpecClass.FooSpec)].Priority.Should().Be(SpecPriority.High);
            var withUpdated = sut.WithSpecs<SpecClass>(SpecPriority.Low);
            withUpdated.Specs[nameof(SpecClass.FooSpec)].Priority.Should().Be(SpecPriority.Low);
        }

        [Fact]
        public void WithSpecs_type_param_should_add_spec_ref()
        {
            var sut = new Subject("sut");
            var sutWithSpec = sut.WithSpecs<SpecClass>();
            var specRef = sutWithSpec.Specs[nameof(SpecClass.FooSpec)];
            specRef.SpecId.Should().Be(nameof(SpecClass.FooSpec));
            specRef.Priority.Should().Be(SpecPriority.Low);
        }

        [Fact]
        public void WithoutSpecs_should_remove_specs()
        {
            var sut = new Subject("sut").WithSpecs<SpecClass>();
            sut.Specs.ContainsKey(SpecClass.FooSpec).Should().BeTrue();
            var withoutSpecs = sut.WithoutSpecs<SpecClass>();
            withoutSpecs.Specs.ContainsKey(SpecClass.FooSpec).Should().BeFalse();

        }


        [Fact]
        public void WithoutSpecs_deep_should_remove_specs_deep()
        {
            var sut = new Subject("sut").WithChild(new Subject("child").WithSpecs<SpecClass>());
            sut.Children.Any(_ => _.Specs.ContainsKey(SpecClass.FooSpec)).Should().BeTrue();
            var withoutSpecs = sut.WithoutSpecs<SpecClass>(true);
            withoutSpecs.Children.Any(_ => _.Specs.ContainsKey(SpecClass.FooSpec)).Should().BeFalse();
        }
    }
}