// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class ResultComparisonOptions
    {
        public bool ShowExpected { get; set; } = true;
        public bool ShowActual { get; set; } = true;
        public bool ShowDiffs { get; set; } = true;
        public bool SortBeforeCompare { get; set; }

        internal static ResultComparisonOptions? FromOptionsAction(
            Action<ResultComparisonOptions>? comparisonOptionsAction)
        {
            if (comparisonOptionsAction == null) return null;
            var options = new ResultComparisonOptions();
            comparisonOptionsAction(options);
            return options;
        }
    }
}
