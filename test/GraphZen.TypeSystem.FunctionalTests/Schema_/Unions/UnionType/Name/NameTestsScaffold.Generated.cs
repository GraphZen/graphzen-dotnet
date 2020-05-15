#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Unions.UnionType.Name
{
    [NoReorder]
    public abstract class NameTestsScaffold
    {
        [Spec(nameof(NameSpecs.can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void can_be_renamed_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 17373967476932201009