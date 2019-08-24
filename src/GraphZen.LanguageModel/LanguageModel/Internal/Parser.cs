// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Internal
{
    public static class Parser
    {
         private static readonly IParser Instance = new SuperpowerParser();

        
        public static DocumentSyntax ParseDocument(string text) =>
            Instance.ParseDocument(text);

        
        public static ValueSyntax ParseValue(string text) =>
            Instance.ParseValue(text);

        
        public static TypeSyntax ParseType(string text) =>
            Instance.ParseType(text);
    }
}