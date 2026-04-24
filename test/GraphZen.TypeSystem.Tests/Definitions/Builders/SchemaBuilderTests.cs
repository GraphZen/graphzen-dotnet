// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem.Tests;

public class SchemaBuilderTests
{
    [Fact]
    public void type_with_duplicate_clr_type_throws_error()
    {
        Schema.Create(sb =>
        {
            sb.Object<Foo>();
            Action addDuplicateInterface = () =>
                sb.Interface<Foo>();
            var ex = Assert.Throws<InvalidOperationException>(addDuplicateInterface);
            Assert.Contains(
                $"Cannot add interface using CLR type '{typeof(Foo)}', an existing object already exists with that CLR type.",
                ex.Message);
        });
    }

    [Fact]
    public void type_with_duplicate_name_throws_error()
    {
        Schema.Create(sb =>
        {
            sb.Object("Foo");
            var addDuplicateInterface = () => { sb.Interface("Foo"); };
            var ex = Assert.Throws<InvalidOperationException>(addDuplicateInterface);
            Assert.Contains(
                "Cannot add interface named 'Foo', an existing object already exists with that name.",
                ex.Message);
        });
    }

    [Fact]
    public void type_with_duplicate_name_via_clr_type_throws_error()
    {
        Schema.Create(sb =>
        {
            sb.Object("Foo");
            var addDuplicateInterface = () => { sb.Interface<Foo>(); };
            var ex = Assert.Throws<InvalidOperationException>(addDuplicateInterface);
            Assert.Contains("Cannot add interface named 'Foo', an existing object already exists with that name.",
                ex.Message);
        });
    }

    public class Foo
    {
    }
}
