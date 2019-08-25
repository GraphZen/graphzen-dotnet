// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
#nullable disable


namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    public class ObjectTypeBuilderIdentityTests : TypeBuilderIdentityTests<ObjectType>
    {
        public class FooObjectClr
        {
        }

        public class BarObjectClr
        {
        }


        public override Type ClrType { get; } = typeof(FooObjectClr);
        public override Type NewClrType { get; } = typeof(BarObjectClr);

        public override void CreateTypeWithName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Object(name);
        }

        public override void CreateTypeWithClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Object(clrType);
        }

        public override void ChangeNameByName(SchemaBuilder schemaBuilder, string name, string newName)
        {
            schemaBuilder.Object(name).Name(newName);
        }

        public override void ChangeNameByType(SchemaBuilder schemaBuilder, Type clrType, string newName)
        {
            schemaBuilder.Object(clrType).Name(newName);
        }

        public override void RemoveNameByName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Object(name).Name(null);
        }

        public override void RemoveNameByClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Object(clrType).Name(null);
        }
    }
}