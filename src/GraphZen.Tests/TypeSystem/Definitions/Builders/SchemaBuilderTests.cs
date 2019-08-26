// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.TypeSystem.Builders
{
    public class SchemaBuilderTests
    {
        public class Foo
        {
        }


        [Fact]
        public void type_with_duplicate_clr_type_throws_error()
        {
            Schema.Create(sb =>
            {
                sb.Object<Foo>();
                Action addDuplicateInterface = () =>
                    sb.Interface<Foo>();
                addDuplicateInterface.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage(
                        "Cannot add interface using CLR type 'GraphZen.TypeSystem.Builders.SchemaBuilderTests+Foo', an existing object already exists with that CLR type.");
            });
        }

        [Fact]
        public void type_with_duplicate_name_throws_error()
        {
            Schema.Create(sb =>
            {
                sb.Object("Foo");
                Action addDuplicateInterface = () => { sb.Interface("Foo"); };
                addDuplicateInterface.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage(
                        "Cannot add interface named 'Foo', an existing object already exists with that name.");
            });
        }

        [Fact]
        public void type_with_duplicate_name_via_clr_type_throws_error()
        {
            Schema.Create(sb =>
            {
                sb.Object("Foo");
                Action addDuplicateInterface = () => { sb.Interface<Foo>(); };
                addDuplicateInterface.Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Cannot add interface named 'Foo', an existing object already exists with that name.");
            });
        }
    }
}