#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Description
{
    public class SchemaBuilderDescriptionTests
    {
        // SpecId: it_can_be_updated
        // Priority: Low
        [Spec("it")]
        [Fact(Skip = "")]
        public void it()
        {
        }


        // SpecId: parent_can_be_created_without
        // Priority: Low


        // SpecId: it_can_be_removed
        // Priority: Low
    }
}