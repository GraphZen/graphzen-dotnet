// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableScalarTypeDefinition :
        IScalarTypeDefinition,
        IMutableNamedTypeDefinition,
        IMutableValueParserDefinition,
        IMutableSerializerDefinition,
        IMutableLiteralParserDefinition

    {
    }

    public interface IValueParserDefinition
    {
        LeafValueParser<object>? ValueParser { get; }
    }

    public interface IValueParser : IValueParserDefinition
    {
    }

    public interface IMutableValueParserDefinition : IValueParserDefinition
    {
        ConfigurationSource? GetValueParserConfigurationSource();
        bool SetValueParser(LeafValueParser<object>? valueParser, ConfigurationSource configurationSource);
    }

    public interface ILiteralParserDefinition
    {
        LeafLiteralParser<object, ValueSyntax>? LiteralParser { get; }
    }

    public interface ILiteralParser : ILiteralParserDefinition
    {
    }

    public interface IMutableLiteralParserDefinition : ILiteralParserDefinition
    {
        ConfigurationSource? GetLiteralParserConfigurationSource();

        bool SetLiteralParser(LeafLiteralParser<object, ValueSyntax>? literalParser,
            ConfigurationSource configurationSource);
    }

    public interface ISerializerDefinition
    {
        LeafSerializer<object>? Serializer { get; }
    }

    public interface ISerializer : ISerializerDefinition
    {
    }

    public interface IMutableSerializerDefinition : ISerializerDefinition
    {
        ConfigurationSource? GetSerializerConfigurationSource();
        bool SetSerializer(LeafSerializer<object>? serializer, ConfigurationSource configurationSource);
    }
}