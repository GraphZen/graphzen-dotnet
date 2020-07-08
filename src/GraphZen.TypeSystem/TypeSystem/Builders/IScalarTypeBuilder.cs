// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IScalarTypeBuilder : INamedTypeBuilder,
        IInfrastructure<InternalScalarTypeBuilder>,
        IInfrastructure<ScalarTypeDefinition>
    {
    }


    public interface
        IScalarTypeBuilder<TScalar, TValueNode> : IScalarTypeBuilder,
            INamedTypeBuilder<ScalarTypeBuilder<TScalar, TValueNode>, ScalarTypeBuilder<object, TValueNode>>
        where TValueNode : ValueSyntax
        where TScalar : notnull
    {
        ScalarTypeBuilder<T, TValueNode> ClrType<T>(bool inferName = false) where T : notnull;
        ScalarTypeBuilder<T, TValueNode> ClrType<T>(string name) where T : notnull;
        ScalarTypeBuilder<TScalar, TValueNode> Serializer(LeafSerializer serializer);
        ScalarTypeBuilder<TScalar, TValueNode> LiteralParser(LeafLiteralParser<object, TValueNode> literalParser);
        ScalarTypeBuilder<TScalar, TValueNode> ValueParser(LeafValueParser<object> valueParser);
    }
}