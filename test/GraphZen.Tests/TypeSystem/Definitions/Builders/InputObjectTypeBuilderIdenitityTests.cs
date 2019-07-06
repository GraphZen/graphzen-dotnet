// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    public class InputObjectTypeBuilderIdenitityTests : TypeBuilderIdentityTests<InputObjectType>
    {
        public class FooInput
        {
        }

        public class BarInput
        {
        }


        public override Type ClrType { get; } = typeof(FooInput);
        public override Type NewClrType { get; } = typeof(BarInput);

        public override void CreateTypeWithName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.InputObject(name);
        }

        public override void CreateTypeWithClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.InputObject(clrType);
        }


        public override void ChangeNameByName(SchemaBuilder schemaBuilder, string name, string newName)
        {
            schemaBuilder.InputObject(name).Name(newName);
        }

        public override void ChangeNameByType(SchemaBuilder schemaBuilder, Type clrType, string newName)
        {
            schemaBuilder.InputObject(clrType).Name(newName);
        }

        public override void RemoveNameByName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.InputObject(name).Name(null);
        }

        public override void RemoveNameByClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.InputObject(clrType).Name(null);
        }
    }
}