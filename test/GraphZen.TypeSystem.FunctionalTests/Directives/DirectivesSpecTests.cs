using System;
using System.Collections.Generic;
using System.Text;
using GraphZen.Infrastructure;

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
