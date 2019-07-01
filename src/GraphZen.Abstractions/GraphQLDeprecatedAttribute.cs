// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.ComponentModel;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    [GraphQLDirective]
    [Description("Marks an element of a GraphQL schema as no longer supported.")]
    public class GraphQLDeprecatedAttribute : Attribute
    {
        /// <summary>
        ///     Marks an element of a GraphQL schema as no longer supported.
        /// </summary>
        /// <param name="reason">
        ///     Explains why this element was deprecated, usually also including a
        ///     suggestion for how to access supported similar data. Formatted
        ///     in [Markdown](https://daringfireball.net/projects/markdown/).
        /// </param>
        public GraphQLDeprecatedAttribute(
            [Description("Explains why this element was deprecated, usually also including a " +
                         "suggestion for how to access supported similar data. Formatted " +
                         "in [Markdown](https://daringfireball.net/projects/markdown/).")]
            string reason = null)
        {
            Reason = reason;
        }

        [CanBeNull]
        [GraphQLCanBeNull]
        public string Reason { get; }
    }
}