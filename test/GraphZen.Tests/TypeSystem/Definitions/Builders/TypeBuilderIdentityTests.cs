// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using FluentAssertions;
using GraphZen.Infrastructure;

using Xunit;

namespace GraphZen.TypeSystem.Builders
{
    [NoReorder]
    public abstract class TypeBuilderIdentityTests<TGraphQLType> where TGraphQLType : NamedType
    {
        public string TypeName { get; } = "FooNamed";
        public string NewTypeName { get; } = "FooNamedNew";

        public abstract Type ClrType { get; }
        public abstract Type NewClrType { get; }

        // Create type
        public abstract void CreateTypeWithName(SchemaBuilder schemaBuilder, string name);
        public abstract void CreateTypeWithClrType(SchemaBuilder schemaBuilder, Type clrType);


        // Change Name
        public abstract void ChangeNameByName(SchemaBuilder schemaBuilder, string name, string newName);
        public abstract void ChangeNameByType(SchemaBuilder schemaBuilder, Type clrType, string newName);


        // Remove name
        public abstract void RemoveNameByName(SchemaBuilder schemaBuilder, string name);
        public abstract void RemoveNameByClrType(SchemaBuilder schemaBuilder, Type clrType);


        [Fact]
        public void TypeWithNameIsCreated()
        {
            var schema = Schema.Create(_ => CreateTypeWithName(_, TypeName));
            var fooType = schema.GetType<TGraphQLType>(TypeName);
            fooType.Name.Should().Be(TypeName);
            fooType.ClrType.Should().Be(null);
        }

        [Fact]
        public void TypeWithClrTypeIsCreated()
        {
            var schema = Schema.Create(_ => CreateTypeWithClrType(_, ClrType));
            var fooTypeByName = schema.GetType<TGraphQLType>(ClrType);
            fooTypeByName.Name.Should().Be(ClrType.Name);
            fooTypeByName.ClrType.Should().Be(ClrType);

            var fooTypeByClrType = schema.GetType<TGraphQLType>(ClrType);
            fooTypeByClrType.Should().Be(fooTypeByName);
        }


        [Fact]
        public void TypeWithName_WhenNameChanged_ShouldReflectNewName()
        {
            var schema = Schema.Create(sb =>
            {
                CreateTypeWithName(sb, TypeName);
                ChangeNameByName(sb, TypeName, NewTypeName);
            });
            schema.TryGetType(TypeName, out _).Should().BeFalse();
            schema.GetType<TGraphQLType>(NewTypeName).Name.Should().Be(NewTypeName);
        }

        [Fact]
        public void CannotRenameType_ToNameOfExistingType()
        {
            Schema.Create(sb =>
            {
                CreateTypeWithName(sb, TypeName);
                CreateTypeWithName(sb, NewTypeName);
                var ex = Assert.Throws<InvalidOperationException>(
                    () => { ChangeNameByName(sb, TypeName, NewTypeName); });

                ex.Message.Should()
                    .Be(
                        "Cannot rename type \"FooNamed\" to \"FooNamedNew\", type named \"FooNamedNew\" already exists.");
            });
        }


        [Theory(Skip = "consider frozen type definitions")]
        [InlineData("String")]
        public void CannotCreateTypeNamedAfterBuiltinType(string name)
        {
            Schema.Create(_ => { CreateTypeWithName(_, name); });
        }
    }
}