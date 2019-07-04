// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using GraphZen.Infrastructure;

using Superpower;

namespace GraphZen.LanguageModel.Internal
{
    public class SuperpowerParser : IParser
    {
        public DocumentSyntax ParseDocument(string document) =>
            Parse(Check.NotNull(document, nameof(document)), Grammar.Grammar.Document);

        public ValueSyntax ParseValue(string value) =>
            Parse(Check.NotNull(value, nameof(value)), Grammar.Grammar.Value);

        public TypeSyntax ParseType(string type) =>
            Parse(Check.NotNull(type, nameof(type)), Grammar.Grammar.Type);

        [NotNull]
        private static T Parse<T>([NotNull] string text, TokenListParser<TokenKind, T> parser)
        {
            var source = new Source(text);


            var tokens = SuperPowerTokenizer.Instance.Tokenize(source.Body);
            Debug.Assert(parser != null, nameof(parser) + " != null");
            var result = parser(tokens);
            if (!result.HasValue)
            {
                var error = new GraphQLError(result.ToString(), null, source, new[] {result.ErrorPosition.Absolute});
                error.Throw();
            }

            Debug.Assert(result.Value != null, "result.Value != null");
            return result.Value;
        }
    }
}