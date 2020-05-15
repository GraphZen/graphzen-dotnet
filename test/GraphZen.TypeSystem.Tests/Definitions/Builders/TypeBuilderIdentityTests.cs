// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests
{
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
                    Action createObjectWithConflictingName = () => { _.Object(TypeName); };
                    createObjectWithConflictingName.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add object named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createObjectWithConflictingName = () => { _.Object(ClrType); };
                    createObjectWithConflictingName.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add object named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createObject = () => { _.Object(ClrType); };
                    createObject.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add object using CLR type '{ClrType}', an existing {ThisKind.ToDisplayStringLower()} already exists with that CLR type.");
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
                    Action createInterfaceWithConflictingName = () => { _.Interface(TypeName); };
                    createInterfaceWithConflictingName.Should().ThrowExactly<InvalidOperationException>()
                        .WithMessage(
                            $"Cannot add interface named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createInterface = () => { _.Interface(ClrType); };
                    createInterface.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add interface named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createInterface = () => { _.Interface(ClrType); };
                    createInterface.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add interface using CLR type '{ClrType}', an existing {ThisKind.ToDisplayStringLower()} already exists with that CLR type.");
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
                    Action createScalarWithConflictingName = () => { _.Scalar(TypeName); };
                    createScalarWithConflictingName.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add scalar named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createScalar = () => { _.Scalar(ClrType); };
                    createScalar.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add scalar named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createScalar = () => { _.Scalar(ClrType); };
                    createScalar.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add scalar using CLR type '{ClrType}', an existing {ThisKind.ToDisplayStringLower()} already exists with that CLR type.");
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
                    Action createUnionWithConflictingName = () => { _.Union(TypeName); };
                    createUnionWithConflictingName.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add union named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createUnion = () => { _.Union(ClrType); };
                    createUnion.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add union named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createUnion = () => { _.Union(ClrType); };
                    createUnion.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add union using CLR type '{ClrType}', an existing {ThisKind.ToDisplayStringLower()} already exists with that CLR type.");
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
                    Action createEnumWithConflictingName = () => { _.Enum(TypeName); };
                    createEnumWithConflictingName.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add enum named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createEnum = () => { _.Enum(ClrType); };
                    createEnum.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add enum named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createEnum = () => { _.Enum(ClrType); };
                    createEnum.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add enum using CLR type '{ClrType}', an existing {ThisKind.ToDisplayStringLower()} already exists with that CLR type.");
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
                    Action createInputObjectWithConflictingName = () => { _.InputObject(TypeName); };
                    createInputObjectWithConflictingName.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add input object named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createInputObject = () => { _.InputObject(ClrType); };
                    createInputObject.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add input object named '{TypeName}', an existing {ThisKind.ToDisplayStringLower()} already exists with that name.");
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
                    Action createInputObject = () => { _.InputObject(ClrType); };
                    createInputObject.Should().ThrowExactly<InvalidOperationException>().WithMessage(
                        $"Cannot add input object using CLR type '{ClrType}', an existing {ThisKind.ToDisplayStringLower()} already exists with that CLR type.");
                }
            });
        }


        [Fact]
        public void type_with_name_is_created()
        {
            var schema = Schema.Create(_ => CreateTypeWithName(_, TypeName));
            var fooType = schema.GetType<TGraphQLType>(TypeName);
            fooType.Name.Should().Be(TypeName);
            fooType.ClrType.Should().Be(null);
        }

        [Fact]
        public void type_with_clr_type_is_created()
        {
            var schema = Schema.Create(_ => CreateTypeWithClrType(_, ClrType));
            var fooTypeByName = schema.GetType<TGraphQLType>(ClrType);
            fooTypeByName.Name.Should().Be(ClrType.Name);
            fooTypeByName.ClrType.Should().Be(ClrType);

            var fooTypeByClrType = schema.GetType<TGraphQLType>(ClrType);
            fooTypeByClrType.Should().Be(fooTypeByName);
        }


        [Fact]
        public void type_created_with_name_should_reflect_new_name_when_changed()
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
        public void cannot_rename_type_to_name_of_existing_type()
        {
            Schema.Create(sb =>
            {
                CreateTypeWithName(sb, TypeName);
                CreateTypeWithName(sb, NewTypeName);
                var ex = Assert.Throws<DuplicateNameException>(
                    () => { ChangeNameByName(sb, TypeName, NewTypeName); });

                var typeDef = sb.GetDefinition().GetType<NamedTypeDefinition>(TypeName);
                var newTypeDef = sb.GetDefinition().GetType<NamedTypeDefinition>(NewTypeName);

                ex.Message.Should()
                    .Be(TypeSystemExceptionMessages.DuplicateNameException.CannotRenameType(typeDef.Identity, NewTypeName,
                        newTypeDef.Identity));
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