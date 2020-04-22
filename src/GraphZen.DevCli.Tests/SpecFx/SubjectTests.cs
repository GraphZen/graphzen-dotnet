using System.Diagnostics.CodeAnalysis;
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
            specRef.Priority.Should().Be(SpecPriority.Default);
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
            specRef.Priority.Should().Be(SpecPriority.Default);
        }
    }
}