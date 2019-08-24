// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;
using GraphZen.Internal;

namespace GraphZen.TypeSystem
{
    public delegate Maybe<object> LeafSerializer<in TScalar>(TScalar value);

    public delegate Maybe<object> LeafSerializer(object value);
}