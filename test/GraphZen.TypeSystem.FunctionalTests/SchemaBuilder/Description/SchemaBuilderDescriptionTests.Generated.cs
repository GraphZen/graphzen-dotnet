#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable UnusedType.Global

// ReSharper disable PartialTypeWithSinglePart
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Description
{
    public class SchemaBuilderDescriptionTests
    {
        public void Hell()
        {
        }
    }

    public partial class SchemaBuilderDescriptionTestsScaffold
    {
// Priority: Low
// Subject Name: Description
        [Spec(nameof(UpdateableSpecs.it_can_be_updated))]
        [Fact(Skip = "generated")]
        public void it_can_be_updated()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: Description
        [Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
        [Fact(Skip = "generated")]
        public void optional_item_can_be_removed()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: Description
        [Spec(nameof(OptionalSpecs.parent_can_be_created_without))]
        [Fact(Skip = "generated")]
        public void parent_can_be_created_without()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}