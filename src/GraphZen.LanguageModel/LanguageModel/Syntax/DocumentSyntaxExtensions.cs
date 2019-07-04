// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;


namespace GraphZen.LanguageModel
{
    public static class DocumentSyntaxExtensions
    {
        [NotNull]
        public static DocumentSyntax WithSpecDefinitions(this DocumentSyntax document) =>
            throw new NotImplementedException();


        [NotNull]
        public static DocumentSyntax WithoutSpecDefinitions(this DocumentSyntax document) =>
            Check.NotNull(document, nameof(document))
                .WithFilteredDefinitions(def => !def.IsSpecDefinedType() && !def.IsSpecDefinedDirective());

        [NotNull]
        public static DocumentSyntax WithoutIntrospectionTypes(this DocumentSyntax document) => Check
            .NotNull(document, nameof(document)).WithFilteredDefinitions(_ => !_.IsIntrospectionType());

        [NotNull]
        public static DocumentSyntax WithoutBuiltInDefinitions(this DocumentSyntax document) => Check
            .NotNull(document, nameof(document)).WithoutIntrospectionTypes().WithoutSpecDefinitions();

        [NotNull]
        public static DocumentSyntax WithDefinitionsAdded(this DocumentSyntax document,
            IEnumerable<DefinitionSyntax> definitions)
        {
            Check.NotNull(document, nameof(document));
            Check.NotNull(definitions, nameof(definitions));
            return new DocumentSyntax(document.Definitions.ToReadOnlyListWithMutations(_ =>
            {
                Debug.Assert(_ != null, nameof(_) + " != null");
                _.AddRange(definitions);
            }));
        }

        public static DocumentSyntax WithDefinitionsAdded(this DocumentSyntax document,
            params DefinitionSyntax[] definitions) =>
            document.WithDefinitionsAdded(definitions.AsEnumerable());

        [NotNull]
        public static DocumentSyntax WithSortedChildren(this DocumentSyntax document)
        {
            Check.NotNull(document, nameof(document));
            var sorted = document.Definitions
                .OrderBy(_ => _.Kind)
                .ThenBy(_ => _ is INamedSyntax named ? named.Name.Value : "")
                .Select(def =>
                {
                    switch (def)
                    {
                        case FragmentDefinitionSyntax _:
                            break;
                        case OperationDefinitionSyntax _:
                            break;
                        case ExecutableDefinitionSyntax _:
                            break;
                        case EnumTypeDefinitionSyntax _:
                            break;
                        case EnumTypeExtensionSyntax _:
                            break;
                        case DirectiveDefinitionSyntax _:
                            break;
                        case InputObjectTypeDefinitionSyntax _:
                            break;
                        case InputObjectTypeExtensionSyntax _:
                            break;
                        case InterfaceTypeDefinitionSyntax node:
                            return node.WithSortedChildren();
                        case InterfaceTypeExtensionSyntax _:
                            break;
                        case ObjectTypeDefinitionSyntax node:
                            return node.WithSortedChildren();
                        case ObjectTypeExtensionSyntax _:
                            break;
                        case ScalarTypeDefinitionSyntax _:
                            break;
                        case ScalarTypeExtensionSyntax _:
                            break;
                        case SchemaDefinitionSyntax _:
                            break;
                        case SchemaExtensionSyntax _:
                            break;
                        case UnionTypeDefinitionSyntax _:
                            break;
                        case TypeDefinitionSyntax _:
                            break;
                        case UnionTypeExtensionSyntax _:
                            break;
                        case TypeExtensionSyntax _:
                            break;
                        case TypeSystemDefinitionSyntax _:
                            break;
                        case TypeSystemExtensionSyntax _:
                            break;
                    }

                    return def;
                });


            return new DocumentSyntax(sorted.ToReadOnlyList());
        }
    }
}