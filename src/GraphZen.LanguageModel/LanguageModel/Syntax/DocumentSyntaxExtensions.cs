// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    public static class DocumentSyntaxExtensions
    {
        public static DocumentSyntax WithSpecDefinitions(this DocumentSyntax document)
        {
            Check.NotNull(document, nameof(document));
            var scalars = document.Definitions.OfType<ScalarTypeDefinitionSyntax>().ToList();
            var missingSpecScalars = SpecScalarSyntaxNodes.All
                .Where(specScalar =>
                {
                    return scalars.All(scalar =>
                    {
                        Debug.Assert(scalar != null, nameof(scalar) + " != null");
                        Debug.Assert(specScalar != null, nameof(specScalar) + " != null");
                        return scalar.Name.Value != specScalar.Name.Value;
                    });
                });

            return document.WithDefinitionsAdded(missingSpecScalars);
        }


        public static DocumentSyntax WithoutSpecDefinitions(this DocumentSyntax document)
        {
            return Check.NotNull(document, nameof(document))
                .WithFilteredDefinitions(def => !def.IsSpecDefinedType() && !def.IsSpecDefinedDirective());
        }


        public static DocumentSyntax WithoutIntrospectionTypes(this DocumentSyntax document)
        {
            return Check
                .NotNull(document, nameof(document)).WithFilteredDefinitions(_ => !_.IsIntrospectionType());
        }


        public static DocumentSyntax WithoutBuiltInDefinitions(this DocumentSyntax document) =>
            Check
                .NotNull(document, nameof(document)).WithoutIntrospectionTypes().WithoutSpecDefinitions();


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