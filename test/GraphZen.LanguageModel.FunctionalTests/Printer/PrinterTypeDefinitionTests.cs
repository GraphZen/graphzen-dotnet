// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.LanguageModel.FunctionalTests.Printer
{
    public class PrinterTypeDefinitionTests
    {
        private readonly IPrinter sut = new Internal.Printer();

        [Fact]
        public void it_should_print_object_type_definition()
        {
            var syntax = ObjectTypeDefinition(
                Name("Foo"),
                StringValue("Description", true), new[]
                {
                    NamedType(Name("IFoo")),
                    NamedType(Name("IBar"))
                }, new[]
                {
                    Directive(Name("dir1"), new[]
                    {
                        Argument(Name("foo"), NullValue())
                    }),
                    Directive(Name("dir2"))
                }, new[]
                {
                    FieldDefinition(Name("abc"), NonNullType(NamedType(Name("String"))))
                });
            sut.Print(syntax).Should().Be(@"""""""
Description
""""""
type Foo implements IFoo & IBar @dir1(foo: null) @dir2 {
  abc: String!
}");
        }
    }
}