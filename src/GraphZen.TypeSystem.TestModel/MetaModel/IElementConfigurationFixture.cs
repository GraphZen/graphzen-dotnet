// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.MetaModel
{
    public interface IElementConfigurationFixture
    {
        void DefineParent(SchemaBuilder sb, string parentName);
        Member GetParent(Schema schema, string parentName);
        MemberDefinition GetParent(SchemaBuilder schemBuilder, string parentName);
    }
}