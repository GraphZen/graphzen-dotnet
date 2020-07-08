// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IDirectiveBuilder : IClrTypeBuilder, IDescriptionBuilder, INamedBuilder,
        IInfrastructure<DirectiveDefinition>,
        IInfrastructure<InternalDirectiveBuilder>

    {
        IDirectiveBuilder AddLocation(DirectiveLocation location);
        IDirectiveBuilder RemoveLocation(DirectiveLocation location);

        IDirectiveBuilder Locations(DirectiveLocation location,
            params DirectiveLocation[] additionalLocations);

        IDirectiveBuilder Locations(IEnumerable<DirectiveLocation> locations);
        IDirectiveBuilder ClearLocations();
        IDirectiveBuilder Repeatable(bool isRepeatable);
    }

    public interface IDirectiveBuilder<TDirective> : IDirectiveBuilder,
        IClrTypeBuilder<DirectiveBuilder<object>>,
        IDescriptionBuilder<DirectiveBuilder<TDirective>>,
        IArgumentsDefinitionBuilder<DirectiveBuilder<TDirective>>,
        INamedBuilder<DirectiveBuilder<TDirective>>
        where TDirective : notnull

    {
        DirectiveBuilder<T> ClrType<T>(bool inferName = false) where T : notnull;
        DirectiveBuilder<T> ClrType<T>(string name) where T : notnull;
        new DirectiveBuilder<TDirective> AddLocation(DirectiveLocation location);
        new DirectiveBuilder<TDirective> RemoveLocation(DirectiveLocation location);

        new DirectiveBuilder<TDirective> Locations(DirectiveLocation location,
            params DirectiveLocation[] additionalLocations);

        new DirectiveBuilder<TDirective> Locations(IEnumerable<DirectiveLocation> locations);
        new DirectiveBuilder<TDirective> ClearLocations();
        new DirectiveBuilder<TDirective> Repeatable(bool isRepeatable);
    }
}