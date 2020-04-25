using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.FunctionalTests.Directives
{
    [Subject(nameof(Schema.Directives))]
    public abstract class DirectivesSpecTests : SchemaTests
    {
    }

    [Subject(nameof(Directive))]
    public abstract class DirectiveSpecTest : DirectivesSpecTests
    {
    }
}