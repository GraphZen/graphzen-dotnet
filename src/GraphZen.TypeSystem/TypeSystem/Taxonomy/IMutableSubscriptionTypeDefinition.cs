using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableSubscriptionTypeDefinition : ISubscriptionTypeDefinition
    {
        new ObjectTypeDefinition? SubscriptionType { get; }
        bool SetSubscriptionType(ObjectTypeDefinition? type, ConfigurationSource configurationSource);
        ConfigurationSource? GetSubscriptionTypeConfigurationSource();
    }
}