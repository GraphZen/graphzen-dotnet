// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public partial class Directive
    {
        public Argument? FindArgument(string name)
            => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var argument) ? argument : null;

        public bool HasArgument(string name)
            => Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        public Argument GetArgument(string name)
            => FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{this} does not contain a {nameof(Argument)} with name '{name}'.");


        public bool TryGetArgument(string name, [NotNullWhen(true)] out Argument? argument)
            => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }
}