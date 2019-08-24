// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem
{
    public delegate Maybe<TScalar> LeafLiteralParser<TScalar, in TValueNode>( TValueNode valueNode)
        where TValueNode : ValueSyntax;
}