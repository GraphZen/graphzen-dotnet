// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface
        IScalarTypeBuilder<TScalar, TValueNode> :
            IInfrastructure<InternalScalarTypeBuilder>,
            IDescriptionBuilder<ScalarTypeBuilder<TScalar, TValueNode>>,
            IAnnotableBuilder<ScalarTypeBuilder<TScalar, TValueNode>>,
            INamedBuilder<ScalarTypeBuilder<TScalar, TValueNode>>,
            IClrTypeBuilder<ScalarTypeBuilder<object, TValueNode>> where TValueNode : ValueSyntax
    {
        ScalarTypeBuilder<T, TValueNode> ClrType<T>();
        ScalarTypeBuilder<T, TValueNode> ClrType<T>(string name);
        ScalarTypeBuilder<TScalar, TValueNode> Serializer(LeafSerializer serializer);
        ScalarTypeBuilder<TScalar, TValueNode> LiteralParser(LeafLiteralParser<object, TValueNode> literalParser);
        ScalarTypeBuilder<TScalar, TValueNode> ValueParser(LeafValueParser<object> valueParser);
    }
}