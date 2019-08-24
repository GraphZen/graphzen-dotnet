#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    public class InputTypeBuilderIdentityTests : TypeBuilderIdentityTests<InputObjectType>
    {
        public class FooInputClr
        {
        }

        public class BazInputClr
        {
        }

        public override Type ClrType { get; } = typeof(FooInputClr);
        public override Type NewClrType { get; } = typeof(BazInputClr);

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