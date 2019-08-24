// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Operation type
    ///     http://facebook.github.io/graphql/June2018/#OperationType
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        ///     A read-only fetch.
        /// </summary>
        Query = 10,

        /// <summary>
        ///     A write followed by a fetch.
        /// </summary>
        Mutation = 20,

        /// <summary>
        ///     A long-lived request that fetches data in response to source events.
        /// </summary>
        Subscription = 30
    }
}