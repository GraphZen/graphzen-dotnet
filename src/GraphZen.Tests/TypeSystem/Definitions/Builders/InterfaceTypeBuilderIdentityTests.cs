// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    public class InterfaceTypeBuilderIdentityTests : TypeBuilderIdentityTests<InterfaceType>
    {
        public interface IFooInterface
        {
        }

        public interface IBarInterface
        {
        }

        public override Type ClrType { get; } = typeof(IFooInterface);
        public override Type NewClrType { get; } = typeof(IBarInterface);

        public override void CreateTypeWithName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Interface(name);
        }

        public override void CreateTypeWithClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Interface(clrType);
        }


        public override void ChangeNameByName(SchemaBuilder schemaBuilder, string name, string newName)
        {
            schemaBuilder.Interface(name).Name(newName);
        }

        public override void ChangeNameByType(SchemaBuilder schemaBuilder, Type clrType, string newName)
        {
            schemaBuilder.Interface(clrType).Name(newName);
        }

        public override void RemoveNameByName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Interface(name).Name(null);
        }

        public override void RemoveNameByClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Interface(clrType).Name(null);
        }
    }
}