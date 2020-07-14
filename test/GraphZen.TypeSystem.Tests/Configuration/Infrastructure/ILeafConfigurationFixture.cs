// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Infrastructure
{
    public interface ILeafConfigurationFixture : IConfigurationFixture
    {
        ConfigurationSource GetElementConfigurationSource(MutableMember parent);
        bool TryGetValue(MutableMember parent, [NotNullWhen(true)] out object? value);
        bool TryGetValue(MutableMember parent, [NotNullWhen(true)] out object? value);
        void ConfigureExplicitly(SchemaBuilder sb, string parentName, object value);
        void RemoveValue(SchemaBuilder sb, string parentName);

        object ValueA { get; }
        object ValueB { get; }
    }
}