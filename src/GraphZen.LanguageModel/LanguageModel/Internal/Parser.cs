// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel.Internal
{
    public static class Parser
    {
        private static readonly IParser Instance = new SuperpowerParser();


        public static DocumentSyntax ParseDocument(string text)
        {
            return Instance.ParseDocument(text);
        }


        public static ValueSyntax ParseValue(string text)
        {
            return Instance.ParseValue(text);
        }


        public static TypeSyntax ParseType(string text)
        {
            return Instance.ParseType(text);
        }
    }
}