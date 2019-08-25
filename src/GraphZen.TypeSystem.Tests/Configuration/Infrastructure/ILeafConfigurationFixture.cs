// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen
{
    public interface ILeafConfigurationFixture : IConfigurationFixture
    {
        ConfigurationSource GetElementConfigurationSource(MemberDefinition parent);
        bool TryGetValue(MemberDefinition parent, [NotNullWhen(true)] out object? value);
        bool TryGetValue(Member parent, [NotNullWhen(true)] out object? value);
        void ConfigureExplicitly(SchemaBuilder sb, string parentName, object value);
        void RemoveValue(SchemaBuilder sb, string parentName);

        object ValueA { get; }
        object ValueB { get; }
    }
}