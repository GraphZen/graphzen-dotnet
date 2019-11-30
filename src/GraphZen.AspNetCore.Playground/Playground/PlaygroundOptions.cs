// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Playground
{
    public class PlaygroundOptions
    {
        public string? Endpoint { get; set; }
        public string? SubscriptionEndpoint { get; set; }
        public string? WorkspaceName { get; set; }
        public PlaygroundSettings Settings { get; set; } = new PlaygroundSettings();
    }
}