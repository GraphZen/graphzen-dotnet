﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    [UsedImplicitly]
    public class UnionTypeBuilderIdenitityTests : TypeBuilderIdentityTests<UnionType>
    {
        public class FooUnionClr
        {
        }

        public class BarUnionClr
        {
        }

        public override Type ClrType { get; } = typeof(FooUnionClr);
        public override Type NewClrType { get; } = typeof(BarUnionClr);

        public override void CreateTypeWithName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Union(name);
        }

        public override void CreateTypeWithClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Union(clrType);
        }


        public override void ChangeNameByName(SchemaBuilder schemaBuilder, string name, string newName)
        {
            schemaBuilder.Union(name).Name(newName);
        }

        public override void ChangeNameByType(SchemaBuilder schemaBuilder, Type clrType, string newName)
        {
            schemaBuilder.Union(clrType).Name(newName);
        }

        public override void RemoveNameByName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Union(name).Name(null);
        }

        public override void RemoveNameByClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Union(clrType).Name(null);
        }
    }
}