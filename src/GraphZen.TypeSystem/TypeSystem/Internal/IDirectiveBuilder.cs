// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    internal interface IDirectiveBuilder<TDirective> :
        IClrTypeBuilder<DirectiveBuilder<object>>,
        IDescriptionBuilder<DirectiveBuilder<TDirective>>,
        IArgumentsDefinitionBuilder<DirectiveBuilder<TDirective>>,
        INamedBuilder<DirectiveBuilder<TDirective>>,
        IInfrastructure<DirectiveDefinition>,
        IInfrastructure<InternalDirectiveBuilder>

    {
        DirectiveBuilder<T> ClrType<T>();
        DirectiveBuilder<T> ClrType<T>(string name);
        DirectiveBuilder<TDirective> Locations(params DirectiveLocation[] locations);
    }
}