// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    [UsedImplicitly]
    public static class InputValueDefinitionSyntaxExtensions
    {
        public static bool IsRequiredArgument(this InputValueDefinitionSyntax arg) =>
            arg.Type is NonNullTypeSyntax && arg.DefaultValue == null;
    }
}