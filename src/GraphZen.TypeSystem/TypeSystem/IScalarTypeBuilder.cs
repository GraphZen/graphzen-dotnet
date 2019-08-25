// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface
        IScalarTypeBuilder<TScalar, out TValueNode> : IAnnotableBuilder<IScalarTypeBuilder<TScalar, TValueNode>>
        where TValueNode : ValueSyntax
    {
        IScalarTypeBuilder<object, TValueNode> ClrType(Type clrType);


        IScalarTypeBuilder<T, TValueNode> ClrType<T>();


        IScalarTypeBuilder<TScalar, TValueNode> Description(string? description);


        IScalarTypeBuilder<TScalar, TValueNode> Serializer(LeafSerializer serializer);


        IScalarTypeBuilder<TScalar, TValueNode> LiteralParser(LeafLiteralParser<object, TValueNode> literalParser);


        IScalarTypeBuilder<TScalar, TValueNode> ValueParser(LeafValueParser<object> valueParser);


        IScalarTypeBuilder<TScalar, TValueNode> Name(string name);
    }
}