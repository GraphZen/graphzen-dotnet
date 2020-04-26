// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class SpecAttribute : Attribute
    {
        public SpecAttribute(string specId)
        {
            SpecId = specId;
        }

        public string SpecId { get; }
        public string? Subject { get; set; }
    }
}