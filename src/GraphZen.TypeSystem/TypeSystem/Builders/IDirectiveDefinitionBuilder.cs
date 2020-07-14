using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IDirectiveDefinitionBuilder :
        IClrTypeBuilder<IDirectiveDefinitionBuilder>,
        IDescriptionBuilder<IDirectiveDefinitionBuilder>,
        INameBuilder<IDirectiveDefinitionBuilder>,
        IArgumentsBuilder<IDirectiveDefinitionBuilder>,
        IInfrastructure<MutableDirectiveDefinition>,
        IInfrastructure<InternalDirectiveDefinitionBuilder>
    {
        IDirectiveDefinitionBuilder AddLocation(DirectiveLocation location);
        IDirectiveDefinitionBuilder RemoveLocation(DirectiveLocation location);

        IDirectiveDefinitionBuilder Locations(DirectiveLocation location, params DirectiveLocation[] additionalLocations);
        IDirectiveDefinitionBuilder Locations(IEnumerable<DirectiveLocation> locations);
        IDirectiveDefinitionBuilder ClearLocations();
        IDirectiveDefinitionBuilder Repeatable(bool isRepeatable);
    }
}