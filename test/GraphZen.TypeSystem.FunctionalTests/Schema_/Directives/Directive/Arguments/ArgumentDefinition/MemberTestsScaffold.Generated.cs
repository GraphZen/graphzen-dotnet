#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.MemberSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Arguments.ArgumentDefinition
{
    [NoReorder]
    public abstract class MemberTests
    {
        [Spec(nameof(should_be_included_in_schema_descendants))]
        [Fact(Skip = "TODO")]
        public void should_be_included_in_schema_descendants_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move MemberTests into a separate file to start writing tests
    [NoReorder]
    public class MemberTestsScaffold
    {
    }
}
// Source Hash Code: 4990403096685396579