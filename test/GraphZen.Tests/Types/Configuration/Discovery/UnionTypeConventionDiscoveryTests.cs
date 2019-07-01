﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [NoReorder]
    public class UnionTypeConventionDiscoveryTests
    {
        public class UnionBase
        {
        }

        public class UnionChildA : UnionBase
        {
        }

        public class UnionChildB : UnionBase
        {
        }
    }
}