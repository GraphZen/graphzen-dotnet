// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;

namespace GraphZen
{
    public class LeafConventionContext
    {
        public string ParentName { get; set; }

        public object DataAnnotationValue { get; set; }
    }
}