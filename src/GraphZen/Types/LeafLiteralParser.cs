// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Utilities;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public delegate Maybe<TScalar> LeafLiteralParser<TScalar, in TValueNode>([NotNull] TValueNode valueNode)
        where TValueNode : ValueSyntax;
}