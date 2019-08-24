// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;

namespace GraphZen
{
    public interface ILeafConfigurationFixture : IConfigurationFixture
    {
        ConfigurationSource GetElementConfigurationSource(MemberDefinition parent);
        bool TryGetValue(MemberDefinition parent, out object value);
        bool TryGetValue(Member parent, out object value);
        void ConfigureExplicitly(SchemaBuilder sb, string parentName, object value);
        void RemoveValue(SchemaBuilder sb,  string parentName);

        object ValueA { get; }
        object ValueB { get; }
    }
}