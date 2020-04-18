// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class SDLSchemaConfigurator
    {
        private readonly DocumentSyntax _document;

        public SDLSchemaConfigurator(DocumentSyntax document)
        {
            _document = Check.NotNull(document, nameof(document));
        }

        public void Configure(SchemaBuilder schemaBuilder)
        {
            Check.NotNull(schemaBuilder, nameof(schemaBuilder));
            var schemaDef = _document.Definitions.OfType<SchemaDefinitionSyntax>().FirstOrDefault();
            var types = _document.Definitions.OfType<TypeDefinitionSyntax>().ToList();
            if (_document.Definitions.OfType<TypeDefinitionSyntax>()
                .TryGetDuplicateValueBy(_ =>
                {
                    Debug.Assert(_ != null, nameof(_) + " != null");
                    return _.Name.Value;
                }, out var duplicateType))
                throw new GraphQLException($"Type \"{duplicateType.Name.Value}\" was defined more than once");

            var operationTypes = schemaDef != null
                ? GetOperationTypes(schemaDef)
                : new Dictionary<OperationType, TypeDefinitionSyntax>
                {
                    {OperationType.Query, types.FindByName("Query")},
                    {OperationType.Mutation, types.FindByName("Mutation")},
                    {OperationType.Subscription, types.FindByName("Subscription")}
                };


            foreach (var def in types.Where(_ => _.IsInputType))
            {
                ConfigureType(schemaBuilder, def);
            }

            foreach (var def in _document.Definitions.OfType<DirectiveDefinitionSyntax>())
            {
                ConfigureDirective(schemaBuilder, def);
            }

            foreach (var def in types.Where(_ => !_.IsInputType && _.IsOutputType))
            {
                ConfigureType(schemaBuilder, def);
            }

            Debug.Assert(operationTypes != null, nameof(operationTypes) + " != null");
            foreach (var ot in operationTypes.Where(_ => _.Value != null))
            {
                Debug.Assert(ot.Value != null, "ot.Value != null");
                var typeName = ot.Value.Name.Value;
                switch (ot.Key)
                {
                    case OperationType.Query:
                        schemaBuilder.QueryType(typeName);
                        break;
                    case OperationType.Mutation:
                        schemaBuilder.MutationType(typeName);
                        break;
                    case OperationType.Subscription:
                        schemaBuilder.SubscriptionType(typeName);
                        break;
                }
            }

            IReadOnlyDictionary<OperationType, TypeDefinitionSyntax> GetOperationTypes(
                SchemaDefinitionSyntax schemaDefinition)
            {
                Check.NotNull(schemaDefinition, nameof(schemaDefinition));
                if (schemaDefinition.RootOperationTypes.TryGetDuplicateKeyBy(_ =>
                    {
                        Debug.Assert(_ != null, nameof(_) + " != null");
                        return _.OperationType;
                    },
                    out var duplicateOpType))
                    throw new Exception($"Must provide only one {duplicateOpType} type in schema.");

                return schemaDefinition.RootOperationTypes.ToDictionary(_ => _.OperationType, _ =>
                {
                    var typeName = _.Type.Name.Value;
#pragma warning disable 8601
                    if (!types.TryFindByName(typeName, out var type))
#pragma warning restore 8601
                        throw new Exception($"Specified {_.OperationType} type \"{typeName}\" not found in document.");

                    return type;
                });
            }
        }

        private static void ConfigureDirective(SchemaBuilder schemaBuilder,
            DirectiveDefinitionSyntax def)
        {
            var directive = schemaBuilder.Directive(def.Name.Value);
            if (def.Description != null) directive.Description(def.Description.Value);

            foreach (var arg in def.Arguments)
            {
                directive.Argument(arg.Name.Value, arg.Type.ToSyntaxString(), _ =>
                {
                    Debug.Assert(_ != null, nameof(_) + " != null");
                    // TODO - how to get default value?
                    if (arg.Description != null) _.Description(arg.Description.Value);
                });
            }

            var locations = def.Locations.Select(_ =>
            {
                Debug.Assert(_ != null, nameof(_) + " != null");
                return DirectiveLocationHelper.Parse(_.Value);
            }).ToArray();
            directive.Locations(locations);
        }

        private static void ConfigureType(SchemaBuilder schemaBuilder, TypeDefinitionSyntax def)
        {
            switch (def)
            {
                case EnumTypeDefinitionSyntax node:
                    {
                        var type = schemaBuilder.Enum(node.Name.Value);
                        if (node.Description != null) type.Description(node.Description.Value);

                        foreach (var valueNode in node.Values)
                        {
                            var enumValue = type.Value(valueNode.Value.Value);
                            if (valueNode.Description != null) enumValue.Description(valueNode.Description.Value);
                        }

                        break;
                    }

                case InputObjectTypeDefinitionSyntax node:
                    {
                        var type = schemaBuilder.InputObject(node.Name.Value);
                        if (node.Description != null) type.Description(node.Description.Value);

                        foreach (var fieldNode in node.Fields)
                        {
                            var field = type.Field(fieldNode.Name.Value, fieldNode.Type.ToSyntaxString());
                            if (fieldNode.Description != null) field.Description(fieldNode.Description.Value);

                            if (fieldNode.DefaultValue != null)
                            {
                            }
                        }

                        break;
                    }

                case InterfaceTypeDefinitionSyntax node:
                    {
                        var type = schemaBuilder.Interface(node.Name.Value);
                        if (node.Description != null) type.Description(node.Description.Value);

                        foreach (var fieldNode in node.Fields)
                        {
                            type.Field(fieldNode.Name.Value, fieldNode.FieldType.ToSyntaxString(), field =>
                            {
                                Debug.Assert(field != null, nameof(field) + " != null");
                                if (fieldNode.Description != null) field.Description(fieldNode.Description.Value);


                                foreach (var argumentNode in fieldNode.Arguments)
                                {
                                    var argument = field.Argument(argumentNode.Name.Value,
                                        argumentNode.Type.ToSyntaxString());
                                    if (argumentNode.Description != null)
                                        argument.Description(argumentNode.Description.Value);
                                }
                            });
                        }

                        break;
                    }

                case ObjectTypeDefinitionSyntax node:
                    {
                        var type = schemaBuilder.Object(node.Name.Value);
                        if (node.Description != null) type.Description(node.Description.Value);

                        foreach (var directive in node.Directives)
                        {
                            type.DirectiveAnnotation(directive.Name.Value, directive);
                        }

                        foreach (var iface in node.Interfaces)
                        {
                            type.ImplementsInterface(iface.Name.Value);
                        }

                        foreach (var fieldNode in node.Fields)
                        {
                            type.Field(fieldNode.Name.Value, fieldNode.FieldType.ToSyntaxString(), field =>
                            {
                                if (fieldNode.Description != null) field.Description(fieldNode.Description.Value);

                                foreach (var directiveNode in fieldNode.Directives)
                                {
                                    field.DirectiveAnnotation(directiveNode.Name.Value, directiveNode);
                                }

                                foreach (var argumentNode in fieldNode.Arguments)
                                {
                                    var argument = field.Argument(argumentNode.Name.Value,
                                        argumentNode.Type.ToSyntaxString());

                                    if (argumentNode.Description != null)
                                        argument.Description(argumentNode.Description.Value);
                                }
                            });
                        }

                        break;
                    }

                case ScalarTypeDefinitionSyntax node:
                    {
                        var type = schemaBuilder.Scalar(node.Name.Value);
                        if (node.Description != null) type.Description(node.Description.Value);

                        break;
                    }

                case UnionTypeDefinitionSyntax node:
                    {
                        var type = schemaBuilder.Union(node.Name.Value);
                        if (node.Description != null) type.Description(node.Description.Value);

                        type.OfTypes(node.MemberTypes.Select(_ =>
                        {
                            Debug.Assert(_ != null, nameof(_) + " != null");
                            return _.Name.Value;
                        }).ToArray());

                        break;
                    }
            }
        }
    }
}