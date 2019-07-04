// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public static class ResponsePathHelpers
    {
        [DebuggerStepThrough]
        [NotNull]
        public static ResponsePath AddPath(this ResponsePath path, object key) => new ResponsePath(path, key);
    }
}