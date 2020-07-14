// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Infrastructure
{
    public abstract class
        DirectiveAnnotationsConfigurationFixture<TMemberDefinition, TMember> : ConfigurationFixture<
            IDirectives, IDirectives, IMutableAnnotationsDefinition,
            TMemberDefinition, TMember> where TMemberDefinition : MutableAnnotatableMember
        where TMember : AnnotatableMember
    {
    }
}