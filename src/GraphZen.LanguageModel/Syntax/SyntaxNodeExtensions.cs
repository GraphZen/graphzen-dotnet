// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public static class SyntaxNodeExtensions
    {
        //[NotNull]
        //[ItemNotNull]
        //private static IReadOnlyList<TypeSystemDefinitionSyntax> IntrospectionTypes { get; } = Introspection
        //    .IntrospectionTypes.ToSyntaxNodes<TypeSystemDefinitionSyntax>().ToList().AsReadOnly();

        //[NotNull]
        //[ItemNotNull]
        //private static IReadOnlyList<ScalarTypeDefinitionSyntax> SpecDefinedTypes { get; } =
        //    SpecScalars.All.ToSyntaxNodes<ScalarTypeDefinitionSyntax>().ToList().AsReadOnly();

        //[NotNull]
        //[ItemNotNull]
        //public static IReadOnlyList<DirectiveDefinitionSyntax> SpecDefinedDirectives { get; } =
        //    SpecDirectives.All.ToSyntaxNodes<DirectiveDefinitionSyntax>().ToList().AsReadOnly();

        [NotNull]
        [ItemNotNull]
        private static IReadOnlyList<TypeSystemDefinitionSyntax> IntrospectionTypes =>
            throw new NotImplementedException();

        [NotNull]
        [ItemNotNull]
        private static IReadOnlyList<ScalarTypeDefinitionSyntax> SpecDefinedTypes => throw new NotImplementedException();

        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<DirectiveDefinitionSyntax> SpecDefinedDirectives => throw new NotImplementedException();

        public static bool IsIntrospectionType(this DefinitionSyntax node)
            => node is TypeSystemDefinitionSyntax typeDef && IntrospectionTypes.Any(_ => _.Equals(typeDef));

        public static bool IsSpecDefinedDirective(this DefinitionSyntax node) =>
            node is DirectiveDefinitionSyntax dirDef && SpecDefinedDirectives.Any(_ => _.Equals(dirDef));

        public static bool IsSpecDefinedType(this DefinitionSyntax node) =>
            node is ScalarTypeDefinitionSyntax typeDef && SpecDefinedTypes.Any(_ => _.Equals(typeDef));

        public static bool IsSchemaOfCommonNames(this SchemaDefinitionSyntax schemaDefinitionNode)
        {
            Check.NotNull(schemaDefinitionNode, nameof(schemaDefinitionNode));
            return Enumerable.All<OperationTypeDefinitionSyntax>(schemaDefinitionNode.RootOperationTypes, ot => ot.Type.Name.Value == ot.OperationType.ToString());
        }
    }
}