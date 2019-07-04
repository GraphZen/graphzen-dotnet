// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.Utilities;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    public class ScalarTypeBuilderIdentityTests : TypeBuilderIdentityTests<ScalarType>
    {
        public class FooScalar
        {
        }

        public class BarScalar
        {
        }

        public override Type ClrType { get; } = typeof(FooScalar);
        public override Type NewClrType { get; } = typeof(BarScalar);

        public override void CreateTypeWithName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Scalar(name).ValueParser(Maybe.Some).LiteralParser(Maybe.Some<object>).Serializer(Maybe.Some);
        }

        public override void CreateTypeWithClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Scalar(clrType).ValueParser(Maybe.Some).LiteralParser(Maybe.Some<object>)
                .Serializer(Maybe.Some);
        }


        public override void ChangeNameByName(SchemaBuilder schemaBuilder, string name, string newName)
        {
            schemaBuilder.Scalar(name).Name(newName);
        }

        public override void ChangeNameByType(SchemaBuilder schemaBuilder, Type clrType, string newName)
        {
            schemaBuilder.Scalar(clrType).Name(newName);
        }

        public override void RemoveNameByName(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Scalar(name).Name(null);
        }

        public override void RemoveNameByClrType(SchemaBuilder schemaBuilder, Type clrType)
        {
            schemaBuilder.Scalar(clrType).Name(null);
        }
    }
}