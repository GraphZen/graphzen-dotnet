// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Infrastructure
{
    public interface IConfigurationFixture

    {
        Type ParentMemberType { get; }
        Type ParentMemberDefinitionType { get; }
        void ConfigureParentExplicitly(SchemaBuilder sb, string parentName);
        MutableMember GetParent(Schema schema, string parentName);
        MutableMember GetParent(SchemaBuilder sb, string parentName);
    }
}