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
    public interface IScalarTypeBuilder : INamedTypeDefinitionBuilder<IScalarTypeBuilder, IScalarTypeBuilder>,
        IInfrastructure<InternalScalarTypeBuilder>,
        IInfrastructure<MutableScalarType>
    {
        IScalarTypeBuilder Serializer(LeafSerializer serializer);
        IScalarTypeBuilder LiteralParser(LeafLiteralParser<object, ValueSyntax> literalParser);
        IScalarTypeBuilder ValueParser(LeafValueParser<object> valueParser);
    }
}