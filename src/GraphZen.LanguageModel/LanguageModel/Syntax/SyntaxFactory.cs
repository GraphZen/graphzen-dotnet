// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public static partial class SyntaxFactory
    {
        [DebuggerStepThrough]
        public static NullValueSyntax NullValue() => NullValueSyntax.Instance;

        [DebuggerStepThrough]
        public static NamedTypeSyntax NamedType(Type clrType) =>
            new NamedTypeSyntax(Name(Check.NotNull(clrType, nameof(clrType)).GetGraphQLName()));

        [DebuggerStepThrough]
        public static BooleanValueSyntax BooleanValue(bool value) => BooleanValueSyntax.From(value);

        [DebuggerStepThrough]
        public static NameSyntax[] Names(params string[] names) => names.Select(_ => Name(_)).ToArray();
    }
}