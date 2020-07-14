// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMutableScalarType :
        IBuildableScalarType,
        IMutableNamedTypeDefinition

    {
        new IScalarTypeBuilder Builder { get; }
        ConfigurationSource? GetLiteralParserConfigurationSource();

        bool SetLiteralParser(LeafLiteralParser<object, ValueSyntax>? literalParser,
            ConfigurationSource configurationSource);

        ConfigurationSource? GetValueParserConfigurationSource();
        bool SetValueParser(LeafValueParser<object>? valueParser, ConfigurationSource configurationSource);

        ConfigurationSource? GetSerializerConfigurationSource();
        bool SetSerializer(LeafSerializer<object>? serializer, ConfigurationSource configurationSource);
    }
}