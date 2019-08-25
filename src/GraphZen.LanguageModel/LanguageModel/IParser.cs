// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    public interface IParser
    {
        DocumentSyntax ParseDocument(string document);


        ValueSyntax ParseValue(string value);


        TypeSyntax ParseType(string type);
    }
}