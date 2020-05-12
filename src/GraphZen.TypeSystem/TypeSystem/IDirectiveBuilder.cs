// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IDirectiveBuilder<TDirective> : IArgumentsDefinitionBuilder<IDirectiveBuilder<TDirective>>,
        IInfrastructure<IDirectiveDefinition>, IInfrastructure<InternalDirectiveBuilder>

    {
        IDirectiveBuilder<TDirective> Description(string description);
        IDirectiveBuilder<TDirective> Name(string name);

        IDirectiveBuilder<TDirective> Locations(params DirectiveLocation[] locations);
    }
}