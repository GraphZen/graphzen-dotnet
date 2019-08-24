// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Diagnostics;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public static class ResponsePathHelpers
    {
        [DebuggerStepThrough]
        
        public static ResponsePath AddPath(this ResponsePath path, object key) => new ResponsePath(path, key);
    }
}