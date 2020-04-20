using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.FunctionalTests.Directives
{
    [SpecSubject(nameof(Schema.Directives))]
    public abstract class DirectivesSpecTests : TypeSystemSpecTests
    {
    }

    [SpecSubject(nameof(Directive))]
    public abstract class DirectiveSpecTest : DirectivesSpecTests
    {
    }
}