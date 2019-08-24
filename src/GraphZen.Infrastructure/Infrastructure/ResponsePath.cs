#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ResponsePath
    {
        internal ResponsePath(ResponsePath previous, object key)
        {
            Previous = previous;
            Key = Check.NotNull(key, nameof(key));
        }

        public ResponsePath(object key) : this(null, key)
        {
        }

        [CanBeNull]
        public ResponsePath Previous { get; }

        [NotNull]
        public object Key { get; }

        private string DebuggerDisplay => ToString();


        [NotNull]
        [DebuggerStepThrough]
        public IReadOnlyList<object> AsReadOnlyList()
        {
            var flattened = new List<object>();
            var curr = this;
            while (curr != null)
            {
                flattened.Add(curr.Key);
                curr = curr.Previous;
            }

            return Enumerable.Reverse(flattened).ToArray();
        }

        [DebuggerStepThrough]
        public override string ToString()
        {
            var sb = new StringBuilder();
            var currentPath = this;
            while (currentPath != null)
            {
                sb.Insert(0, Key is string ? $".{currentPath.Key}" : $"[{currentPath.Key}]");

                currentPath = currentPath.Previous;
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, "value");
            }

            return sb.ToString();
        }
    }
}