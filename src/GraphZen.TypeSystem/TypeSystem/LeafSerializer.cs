// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public delegate Maybe<object> LeafSerializer<in TScalar>(TScalar value);

    public delegate Maybe<object> LeafSerializer(object value);
}