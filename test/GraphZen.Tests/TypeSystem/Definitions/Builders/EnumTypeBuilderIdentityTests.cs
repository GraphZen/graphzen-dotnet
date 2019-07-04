// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;


namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    public class EnumTypeBuilderIdentityTests : TypeBuilderIdentityTests<EnumType>
    {
        public enum FooEnum
        {
        }

        public enum BarEnum
        {
        }

        public override Type ClrType { get; } = typeof(FooEnum);
        public override Type NewClrType { get; } = typeof(BarEnum);

        public override void CreateTypeWithName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Enum(name);
        }

        public override void CreateTypeWithClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Enum(clrType);
        }


        public override void ChangeNameByName(SchemaBuilder schemaBuilder, string name, string newName)
        {
            schemaBuilder.Enum(name).Name(newName);
        }

        public override void ChangeNameByType(SchemaBuilder schemaBuilder, Type clrType, string newName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveNameByName(SchemaBuilder schemaBuilder, string name)
        {
            throw new NotImplementedException();
        }

        public override void RemoveNameByClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            throw new NotImplementedException();
        }
    }
}