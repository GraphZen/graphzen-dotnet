// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using GraphZen.Infrastructure;

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


        public static bool IsIntrospectionType(this DefinitionSyntax node)
            => node is TypeDefinitionSyntax typeDef &&
               SpecReservedNames.IntrospectionTypeNames.Contains(typeDef.Name.Value);

        public static bool IsSpecDefinedDirective(this DefinitionSyntax node) =>
            node is DirectiveDefinitionSyntax dirDef && SpecReservedNames.DirectiveNames.Contains(dirDef.Name.Value);

        public static bool IsSpecDefinedType(this DefinitionSyntax node) =>
            node is ScalarTypeDefinitionSyntax typeDef &&
            SpecReservedNames.ScalarTypeNames.Contains(typeDef.Name.Value);

        public static bool IsSchemaOfCommonNames(this SchemaDefinitionSyntax schemaDefinitionNode)
        {
            Check.NotNull(schemaDefinitionNode, nameof(schemaDefinitionNode));
            return schemaDefinitionNode.RootOperationTypes.All(ot => ot.Type.Name.Value == ot.OperationType.ToString());
        }
    }
}