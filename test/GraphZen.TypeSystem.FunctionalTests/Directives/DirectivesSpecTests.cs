// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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