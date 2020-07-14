// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IScalarType : ILeafType, IOutputMember, IInputMember
    {

        LeafValueParser<object?>? ValueParser { get; }
        LeafSerializer<object?>? Serializer { get; }
        LeafLiteralParser<object?, ValueSyntax>? LiteralParser { get; }
    }
}