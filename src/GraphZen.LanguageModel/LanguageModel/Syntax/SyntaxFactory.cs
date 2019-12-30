// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

#nullable disable


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
        public static ListValueSyntax ListValue(params ValueSyntax[] values) => new ListValueSyntax(values);


        [DebuggerStepThrough]
        public static ObjectValueSyntax ObjectValue(params ObjectFieldSyntax[] fields) => new ObjectValueSyntax(fields);


        [DebuggerStepThrough]
        public static BooleanValueSyntax BooleanValue(bool value) => BooleanValueSyntax.Create(value);


        [DebuggerStepThrough]
        public static DocumentSyntax Document(params DefinitionSyntax[] definitions) => new DocumentSyntax(definitions);


        [DebuggerStepThrough]
        public static NameSyntax[] Names(params string[] names) =>
            Check.NotNull(names, nameof(names)).Select(_ => Name(_)).ToArray();
    }
}