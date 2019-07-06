// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    public interface IParser
    {
        [NotNull]
        DocumentSyntax ParseDocument(string document);

        [NotNull]
        ValueSyntax ParseValue(string value);

        [NotNull]
        TypeSyntax ParseType(string type);
    }
}