// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class JsonDiffOptions
    {
        public bool SortBeforeCompare { get; set; }
        public StringDiffOptions StringDiffOptions { get; } = new StringDiffOptions();

        internal static JsonDiffOptions? FromOptionsAction(
            Action<JsonDiffOptions>? optionsAction)
        {
            if (optionsAction == null) return null;
            var options = new JsonDiffOptions();
            optionsAction(options);
            return options;
        }
    }
}