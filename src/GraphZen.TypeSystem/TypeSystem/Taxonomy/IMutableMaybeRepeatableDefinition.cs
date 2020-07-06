using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableMaybeRepeatableDefinition : IMaybeRepeatableDefinition
    {
        bool SetRepeatable(bool repeatable, ConfigurationSource configurationSource);
        ConfigurationSource GetRepeatableConfigurationSource();
    }
}