// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem.Taxonomy;

public interface IMutableSubscriptionTypeDefinition : ISubscriptionTypeDefinition
{
    new ObjectTypeDefinition? SubscriptionType { get; }
    bool SetSubscriptionType(ObjectTypeDefinition? type, ConfigurationSource configurationSource);
    ConfigurationSource? GetSubscriptionTypeConfigurationSource();
}
