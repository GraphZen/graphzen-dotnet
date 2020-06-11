// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.TypeReferenceSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition
    .
    ArgumentType
{
    [NoReorder]
    public class TypeReferenceTests
    {
        public class PlainClass
        {
        }

        [Spec(nameof(it_can_be_created_if_type_io_compatible))]
        [Fact]
        public void it_can_be_created_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.Interface("Bar").Field("field", "String", f => { f.Argument("arg", "Foo"); });
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetInterface("Bar").GetField("field").GetArgument("arg").ArgumentType.GetNamedType().Should()
                .Be(foo);
        }

        [Spec(nameof(it_cannot_be_created_if_type_has_io_conflict))]
        [Fact]
        public void it_cannot_be_created_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.Interface("Bar").Field("field", "String", f =>
                {
                    Action add = () => f.Argument("baz", "[Foo]!");
                    add.Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot create interface field argument Bar.field.baz with argument type '[Foo]!'. Field arguments can only use input types. Interface Foo is only an output type.");
                });
            });
        }


        [Spec(nameof(it_can_be_created_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void it_can_be_created_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                _.Interface("Bar").Field("field", "String", f => { f.Argument<PlainClass>("arg"); });
            });
            var foo = schema.GetInputObject<PlainClass>();
            schema.GetInterface("Bar").GetField("field").GetArgument("arg").ArgumentType.GetNamedType().Should()
                .Be(foo);
        }


        [Spec(nameof(it_cannot_be_created_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void it_cannot_be_created_with_clr_type_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Interface<PlainClass>("Foo");
                _.Interface("Bar").Field("field", "String", f =>
                {
                    Action add = () => f.Argument<PlainClass>("baz");
                    add.Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot create interface field argument Bar.field.baz with argument type 'Foo!'. Field arguments can only use input types. Interface Foo (CLR class: PlainClass) is only an output type.");
                });
            });
        }

        [Spec(nameof(type_can_be_set_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_if_type_matches_own_io_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.Interface("Bar").Field("field", "String",
                    f => { f.Argument("arg", "Baz", a => a.ArgumentType("Foo")); });
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetInterface("Bar").GetField("field").GetArgument("arg").ArgumentType.GetNamedType().Should()
                .Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.Interface("Bar").Field("field", "String", f =>
                {
                    f.Argument("arg", "Baz", arg =>
                    {
                        Action set = () => arg.ArgumentType("Foo");
                        set.Should().Throw<InvalidTypeException>().WithMessage(
                            "Cannot set argument type to 'Foo' on interface field argument Bar.field.arg. Field arguments can only use input types. Interface Foo is only an output type.");
                    });
                });
            });
        }

        [Spec(nameof(type_can_be_set_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                _.Interface("Bar").Field("field", "String",
                    f => { f.Argument("arg", "Baz", a => a.ArgumentType<PlainClass>()); });
            });
            var foo = schema.GetInputObject<PlainClass>();
            schema.GetInterface("Bar").GetField("field").GetArgument("arg").ArgumentType.GetNamedType().Should()
                .Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.Interface("Bar")
                    .Field("field", "String", f =>
                    {
                        f.Argument("arg", "Baz", arg =>
                        {
                            Action set = () => arg.ArgumentType<PlainClass>();
                            set.Should().Throw<InvalidTypeException>().WithMessage(
                                "Cannot set argument type to 'PlainClass!' on interface field argument Bar.field.arg. Field arguments can only use input types. Object PlainClass is only an output type.");
                        });
                    });
            });
        }


        [Spec(nameof(type_can_be_set_via_parent_redefinition_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_via_parent_redefinition_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.Interface("Bar").Field("field", "String", f =>
                {
                    f.Argument("arg", "Baz");
                    f.Argument("arg", "Foo");
                });
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetInterface("Bar").GetField("field").GetArgument("arg").ArgumentType.GetNamedType().Should()
                .Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_via_parent_redefinition_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_via_parent_redefinition_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.Interface("Bar")
                    .Field("field", "String", f =>
                    {
                        f.Argument("arg", "Baz");
                        Action redefine = () => f.Argument("arg", "Foo");
                        redefine.Should().Throw<InvalidTypeException>().WithMessage(
                            "Cannot set argument type to 'Foo' on interface field argument Bar.field.arg. Field arguments can only use input types. Interface Foo is only an output type.");
                    });
            });
        }


        [Spec(nameof(type_can_be_set_via_parent_redefinition_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_via_parent_redefinition_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                _.Interface("Bar").Field("field", "String", f =>
                {
                    f.Argument("arg", "Baz");
                    f.Argument<PlainClass>("arg");
                });
            });
            var typed = schema.GetInputObject<PlainClass>();
            schema.GetInterface("Bar").GetField("field").GetArgument("arg").ArgumentType.GetNamedType().Should()
                .Be(typed);
        }


        [Spec(nameof(type_cannot_be_set_via_parent_redefinition_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_via_parent_redefinition_with_clr_type_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.Interface("Bar").Field("field", "String", f =>
                {
                    f.Argument("arg", "Baz");
                    Action redefine = () => f.Argument<PlainClass>("arg");
                    redefine.Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot set argument type to 'PlainClass!' on interface field argument Bar.field.arg. Field arguments can only use input types. Object PlainClass is only an output type.");
                });
            });
        }
    }
}