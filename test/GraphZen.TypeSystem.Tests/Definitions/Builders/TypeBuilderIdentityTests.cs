// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests;

[NoReorder]
public abstract class TypeBuilderIdentityTests<TGraphQLType> where TGraphQLType : NamedType
{
    public string TypeName => ClrType.Name;
    public string NewTypeName => NewClrType.Name;

    public abstract Type ClrType { get; }
    public abstract Type NewClrType { get; }

    public TypeKind ThisKind => TypeKindHelpers.TryGetTypeKindFromType<TGraphQLType>(out var kind)
        ? kind
        : throw new InvalidOperationException();

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
    public void cannot_create_object_with_conflicting_name()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Object)
            {
                var createObjectWithConflictingName = () => { _.Object(TypeName); };
                var ex = Assert.Throws<InvalidOperationException>(createObjectWithConflictingName);
                Assert.Contains(
                    $"Cannot add object named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_object_with_conflicting_name_via_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Object)
            {
                var createObjectWithConflictingName = () => { _.Object(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createObjectWithConflictingName);
                Assert.Contains(
                    $"Cannot add object named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_object_with_conflicting_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithClrType(_, ClrType);

            if (ThisKind != TypeKind.Object)
            {
                var createObject = () => { _.Object(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createObject);
                Assert.Contains(
                    $"Cannot add object using CLR type '{ClrType}', an existing {ThisKind.ToDisplayString()} already exists with that CLR type.",
                    ex.Message);
            }
        });
    }


    [Fact]
    public void cannot_create_interface_with_conflicting_name()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Interface)
            {
                var createInterfaceWithConflictingName = () => { _.Interface(TypeName); };
                var ex = Assert.Throws<InvalidOperationException>(createInterfaceWithConflictingName);
                Assert.Contains(
                    $"Cannot add interface named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_interface_with_conflicting_name_via_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Interface)
            {
                var createInterface = () => { _.Interface(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createInterface);
                Assert.Contains(
                    $"Cannot add interface named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_interface_with_conflicting_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithClrType(_, ClrType);

            if (ThisKind != TypeKind.Interface)
            {
                var createInterface = () => { _.Interface(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createInterface);
                Assert.Contains(
                    $"Cannot add interface using CLR type '{ClrType}', an existing {ThisKind.ToDisplayString()} already exists with that CLR type.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_scalar_with_conflicting_name()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Scalar)
            {
                var createScalarWithConflictingName = () => { _.Scalar(TypeName); };
                var ex = Assert.Throws<InvalidOperationException>(createScalarWithConflictingName);
                Assert.Contains(
                    $"Cannot add scalar named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_scalar_with_conflicting_name_via_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Scalar)
            {
                var createScalar = () => { _.Scalar(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createScalar);
                Assert.Contains(
                    $"Cannot add scalar named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_scalar_with_conflicting_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithClrType(_, ClrType);

            if (ThisKind != TypeKind.Scalar)
            {
                var createScalar = () => { _.Scalar(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createScalar);
                Assert.Contains(
                    $"Cannot add scalar using CLR type '{ClrType}', an existing {ThisKind.ToDisplayString()} already exists with that CLR type.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_union_with_conflicting_name()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Union)
            {
                var createUnionWithConflictingName = () => { _.Union(TypeName); };
                var ex = Assert.Throws<InvalidOperationException>(createUnionWithConflictingName);
                Assert.Contains(
                    $"Cannot add union named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_union_with_conflicting_name_via_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Union)
            {
                var createUnion = () => { _.Union(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createUnion);
                Assert.Contains(
                    $"Cannot add union named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_union_with_conflicting_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithClrType(_, ClrType);

            if (ThisKind != TypeKind.Union)
            {
                var createUnion = () => { _.Union(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createUnion);
                Assert.Contains(
                    $"Cannot add union using CLR type '{ClrType}', an existing {ThisKind.ToDisplayString()} already exists with that CLR type.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_enum_with_conflicting_name()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Enum)
            {
                var createEnumWithConflictingName = () => { _.Enum(TypeName); };
                var ex = Assert.Throws<InvalidOperationException>(createEnumWithConflictingName);
                Assert.Contains(
                    $"Cannot add enum named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_enum_with_conflicting_name_via_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.Enum)
            {
                var createEnum = () => { _.Enum(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createEnum);
                Assert.Contains(
                    $"Cannot add enum named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_enum_object_with_conflicting_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithClrType(_, ClrType);

            if (ThisKind != TypeKind.Enum)
            {
                var createEnum = () => { _.Enum(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createEnum);
                Assert.Contains(
                    $"Cannot add enum using CLR type '{ClrType}', an existing {ThisKind.ToDisplayString()} already exists with that CLR type.",
                    ex.Message);
            }
        });
    }


    [Fact]
    public void cannot_create_input_object_with_conflicting_name()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.InputObject)
            {
                var createInputObjectWithConflictingName = () => { _.InputObject(TypeName); };
                var ex = Assert.Throws<InvalidOperationException>(createInputObjectWithConflictingName);
                Assert.Contains(
                    $"Cannot add input object named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_input_object_with_conflicting_name_via_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithName(_, TypeName);


            if (ThisKind != TypeKind.InputObject)
            {
                var createInputObject = () => { _.InputObject(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createInputObject);
                Assert.Contains(
                    $"Cannot add input object named '{TypeName}', an existing {ThisKind.ToDisplayString()} already exists with that name.",
                    ex.Message);
            }
        });
    }

    [Fact]
    public void cannot_create_input_object_with_conflicting_clr_type()
    {
        Schema.Create(_ =>
        {
            CreateTypeWithClrType(_, ClrType);

            if (ThisKind != TypeKind.InputObject)
            {
                var createInputObject = () => { _.InputObject(ClrType); };
                var ex = Assert.Throws<InvalidOperationException>(createInputObject);
                Assert.Contains(
                    $"Cannot add input object using CLR type '{ClrType}', an existing {ThisKind.ToDisplayString()} already exists with that CLR type.",
                    ex.Message);
            }
        });
    }


    [Fact]
    public void type_with_name_is_created()
    {
        var schema = Schema.Create(_ => CreateTypeWithName(_, TypeName));
        var fooType = schema.GetType<TGraphQLType>(TypeName);
        Assert.Equal(TypeName, fooType.Name);
        Assert.Null(fooType.ClrType);
    }

    [Fact]
    public void type_with_clr_type_is_created()
    {
        var schema = Schema.Create(_ => CreateTypeWithClrType(_, ClrType));
        var fooTypeByName = schema.GetType<TGraphQLType>(ClrType);
        Assert.Equal(ClrType.Name, fooTypeByName.Name);
        Assert.Equal(ClrType, fooTypeByName.ClrType);

        var fooTypeByClrType = schema.GetType<TGraphQLType>(ClrType);
        Assert.Equal(fooTypeByName, fooTypeByClrType);
    }


    [Fact]
    public void type_created_with_name_should_reflect_new_name_when_changed()
    {
        var schema = Schema.Create(sb =>
        {
            CreateTypeWithName(sb, TypeName);
            ChangeNameByName(sb, TypeName, NewTypeName);
        });
        Assert.False(schema.TryGetType(TypeName, out _));
        Assert.Equal(NewTypeName, schema.GetType<TGraphQLType>(NewTypeName).Name);
    }

    [Fact]
    public void cannot_rename_type_to_name_of_existing_type()
    {
        Schema.Create(sb =>
        {
            CreateTypeWithName(sb, TypeName);
            CreateTypeWithName(sb, NewTypeName);
            var ex = Assert.Throws<InvalidOperationException>(() => { ChangeNameByName(sb, TypeName, NewTypeName); });

            Assert.Equal(
                $"Cannot rename type \"{TypeName}\" to \"{NewTypeName}\", type named \"{NewTypeName}\" already exists.",
                ex.Message);
        });
    }


    [Theory(Skip = "consider frozen type definitions")]
    [InlineData("String")]
    public void CannotCreateTypeNamedAfterBuiltinType(string name)
    {
        Schema.Create(_ => { CreateTypeWithName(_, name); });
    }
}