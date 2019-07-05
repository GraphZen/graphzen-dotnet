// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem
{
    public interface
        IScalarTypeBuilder<TScalar, out TValueNode> : IAnnotableBuilder<IScalarTypeBuilder<TScalar, TValueNode>>
        where TValueNode : ValueSyntax
    {
        [NotNull]
        IScalarTypeBuilder<TScalar, TValueNode> Description([CanBeNull] string description);

        [NotNull]
        IScalarTypeBuilder<TScalar, TValueNode> Serializer(LeafSerializer serializer);

        [NotNull]
        IScalarTypeBuilder<TScalar, TValueNode> LiteralParser(LeafLiteralParser<object, TValueNode> literalParser);

        [NotNull]
        IScalarTypeBuilder<TScalar, TValueNode> ValueParser(LeafValueParser<object> valueParser);

        [NotNull]
        IScalarTypeBuilder<TScalar, TValueNode> Name(string name);
    }
}