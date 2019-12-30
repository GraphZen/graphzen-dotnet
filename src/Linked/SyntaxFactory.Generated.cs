// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.


using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable enable

namespace GraphZen.LanguageModel
{
    public static partial class SyntaxFactory
    {
        public static ArgumentSyntax Argument(NameSyntax name, StringValueSyntax description, ValueSyntax value,
            SyntaxLocation? location = null) => new ArgumentSyntax(name, description, value, location);
    }
}