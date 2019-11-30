// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower.Model;

namespace GraphZen.Infrastructure
{
    public static class SuperpowerTestExtensions
    {
        public static void ThrowOnParserError<TKind, T>(this TokenListParserResult<TKind, T> result)
        {
            if (!result.HasValue) throw new Exception(result.ToString());
        }
    }
}