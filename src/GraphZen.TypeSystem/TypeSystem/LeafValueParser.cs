// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.Internal;

namespace GraphZen.TypeSystem
{
    public delegate Maybe<TScalar> LeafValueParser<TScalar>(object value);

    public delegate Maybe<object> LeafValueParser(object value);
}