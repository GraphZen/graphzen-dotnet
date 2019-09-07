using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableDirectiveLocationsDefinition : IDirectiveLocationsDefinition
    {
        bool AddLocation(DirectiveLocation location, ConfigurationSource configurationSource);
        bool IgnoreLocation(DirectiveLocation location, ConfigurationSource configurationSource);
        bool UnignoreLocation(DirectiveLocation location, ConfigurationSource configurationSource);
        ConfigurationSource? FindDirectiveLocationConfigurationSource(DirectiveLocation location);
        ConfigurationSource? FindIgnoredDirectiveLocationConfigurationSource(DirectiveLocation location);
    }
}