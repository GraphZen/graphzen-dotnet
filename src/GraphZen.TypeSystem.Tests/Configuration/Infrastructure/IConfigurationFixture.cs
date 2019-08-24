// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public interface IConfigurationFixture 

    {
        Type ParentMemberType { get; }
        Type ParentMemberDefinitionType { get; }
        void ConfigureParentExplicitly(SchemaBuilder sb,  string parentName);
        Member GetParent(Schema schema,  string parentName);
        MemberDefinition GetParent(SchemaBuilder sb, string parentName);
    }
}