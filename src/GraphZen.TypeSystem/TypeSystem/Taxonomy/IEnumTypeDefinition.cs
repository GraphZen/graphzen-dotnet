// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IEnumTypeDefinition : IEnumValuesContainerDefinition,
        ILeafTypeDefinition, IInputDefinition, IOutputDefinition
    {
    }

    [GraphQLIgnore]
    public interface IEnumValuesContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IEnumValueDefinition> GetValues();
    }

    [GraphQLIgnore]
    public interface IEnumValuesContainer : IEnumValuesContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, EnumValue> Values { get; }

        [NotNull]
        IReadOnlyDictionary<object, EnumValue> ValuesByValue { get; }

        [NotNull]
        [ItemNotNull]
        new IEnumerable<EnumValue> GetValues();
    }

    [GraphQLIgnore]
    public interface IMutableEnumValuesContainerDefinition : IEnumValuesContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, EnumValueDefinition> Values { get; }
        ConfigurationSource? FindIgnoredValueConfigurationSource(string name);
        [NotNull]
        [ItemNotNull]
        new IEnumerable<EnumValueDefinition> GetValues();

    }

}