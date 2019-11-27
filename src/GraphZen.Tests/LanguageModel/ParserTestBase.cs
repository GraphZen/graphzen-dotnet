// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.LanguageModel
{
    public abstract class ParserTestBase
    {
        private IParser Parser { get; } = new SuperpowerParser();

        protected DocumentSyntax ParseDocument(string source) => Parser.ParseDocument(source);

        protected DocumentSyntax PrintAndParse(DocumentSyntax document)
        {
            var printed = document.ToSyntaxString();
            Console.WriteLine("Printed:");
            Console.WriteLine(printed);
            return ParseDocument(printed);
        }

        protected ValueSyntax ParseValue(string source) => Parser.ParseValue(source);

        protected TypeSyntax ParseType(string source) => Parser.ParseType(source);

        protected void AssertSyntaxError(string document, string expectedMessage,
            params (int line, int column)[] locations)
        {
            var ex = Assert.Throws<GraphQLException>(() => ParseDocument(document));
            TestHelpers.AssertEqualsDynamic(new
            {
                message = expectedMessage,
                locations = locations.Select(l => new {l.line, l.column})
            }, ex.GraphQLError);
        }
    }
}