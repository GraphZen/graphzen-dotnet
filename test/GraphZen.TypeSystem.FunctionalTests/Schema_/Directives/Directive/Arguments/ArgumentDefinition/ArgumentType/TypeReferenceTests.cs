// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.TypeReferenceSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Arguments.ArgumentDefinition.ArgumentType
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
                _.Directive("Bar").Argument("arg", "Foo");
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetDirective("Bar").GetArgument("arg").ArgumentType.GetNamedType().Should().Be(foo);
        }

        [Spec(nameof(it_cannot_be_created_if_type_has_io_conflict))]
        [Fact]
        public void it_cannot_be_created_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                var bar = _.Directive("Bar");
                Action add = () => bar.Argument("baz", "[Foo]!");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create directive argument Bar.baz with argument type '[Foo]!'. Directive arguments can only use input types. Object Foo is only an output type.");
            });
        }


        [Spec(nameof(it_can_be_created_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void it_can_be_created_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                _.Directive("Bar").Argument<PlainClass>("field");
            });
            var foo = schema.GetInputObject<PlainClass>();
            schema.GetDirective("Bar").GetArgument("field").ArgumentType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(it_cannot_be_created_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void it_cannot_be_created_with_clr_type_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>("Foo");
                var bar = _.Directive("Bar");
                Action add = () => bar.Argument<PlainClass>("baz");
                add.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot create directive argument Bar.baz with argument type 'Foo!'. Directive arguments can only use input types. Object Foo (CLR class: PlainClass) is only an output type.");
            });
        }


        [Spec(nameof(type_can_be_set_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_if_type_matches_own_io_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.Directive("Bar").Argument("field", "Baz", f => f.ArgumentType("Foo"));
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetDirective("Bar").GetArgument("field").ArgumentType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                _.Directive("Bar").Argument("field", "Baz", f =>
                {
                    Action set = () => f.ArgumentType("Foo");
                    set.Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot set argument type to 'Foo' on directive argument Bar.field. Directive arguments can only use input types. Object Foo is only an output type.");
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
                _.Directive("Bar").Argument("field", "Baz", f => f.ArgumentType<PlainClass>());
            });
            var foo = schema.GetInputObject<PlainClass>();
            schema.GetDirective("Bar").GetArgument("field").ArgumentType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.Directive("Bar").Argument("field", "Baz", f =>
                {
                    Action set = () => f.ArgumentType<PlainClass>();
                    set.Should().Throw<InvalidTypeException>().WithMessage(
                        "Cannot set argument type to 'PlainClass!' on directive argument Bar.field. Directive arguments can only use input types. Object PlainClass is only an output type.");
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
                _.Directive("Bar").Argument("field", "Bar");
                _.Directive("Bar").Argument("field", "Foo");
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetDirective("Bar").GetArgument("field").ArgumentType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(type_cannot_be_set_via_parent_redefinition_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_via_parent_redefinition_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                var bar = _.Directive("Bar").Argument("field", "Bar");
                Action redefine = () => bar.Argument("field", "Foo");
                redefine.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set argument type to 'Foo' on directive argument Bar.field. Directive arguments can only use input types. Object Foo is only an output type.");
            });
        }


        [Spec(nameof(type_can_be_set_via_parent_redefinition_with_clr_type_if_type_io_compatible))]
        [Fact]
        public void type_can_be_set_via_parent_redefinition_with_clr_type_if_type_io_compatible_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                _.Directive("Bar").Argument("field", "Bar");
                _.Directive("Bar").Argument<PlainClass>("field");
            });
            var typed = schema.GetInputObject<PlainClass>();
            schema.GetDirective("Bar").GetArgument("field").ArgumentType.GetNamedType().Should().Be(typed);
        }


        [Spec(nameof(type_cannot_be_set_via_parent_redefinition_with_clr_type_if_type_has_io_conflict))]
        [Fact]
        public void type_cannot_be_set_via_parent_redefinition_with_clr_type_if_type_has_io_conflict_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                var bar = _.Directive("Bar").Argument("field", "Bar");
                Action redefine = () => bar.Argument<PlainClass>("field");
                redefine.Should().Throw<InvalidTypeException>().WithMessage(
                    "Cannot set argument type to 'PlainClass!' on directive argument Bar.field. Directive arguments can only use input types. Object PlainClass is only an output type.");
            });
        }
    }
}