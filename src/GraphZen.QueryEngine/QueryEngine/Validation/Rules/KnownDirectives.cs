// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation.Rules
{
    public class KnownDirectives : QueryValidationRuleVisitor
    {
        private readonly Lazy<IReadOnlyDictionary<string, IReadOnlyCollection<DirectiveLocation>>> _lazyLocationsMap;


        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public KnownDirectives(QueryValidationContext context) : base(context)
        {
            _lazyLocationsMap = new Lazy<IReadOnlyDictionary<string, IReadOnlyCollection<DirectiveLocation>>>(() =>
                Context.Schema.Directives.ToReadOnlyDictionary(_ => _.Name, _ => _.Locations));
        }


        private IReadOnlyDictionary<string, IReadOnlyCollection<DirectiveLocation>> LocationsMap =>
            _lazyLocationsMap.Value;


        public static string UnknownDirectiveMessage(string directiveName) => $"Unknown directive \"{directiveName}\".";

        public static string MisplacedDirectiveMessage(string directiveName, string location) =>
            $"Directive \"{directiveName}\" may not be used on {location}.";

        public override VisitAction EnterDirective(DirectiveSyntax node)
        {
            var name = node.Name.Value;
            if (!LocationsMap.TryGetValue(name, out var locations))
            {
                ReportError(UnknownDirectiveMessage(name), node);
                return VisitAction.Continue;
            }

            var candidateLocation = GetDirectiveLocationForAstPath(Context.Ancestors);
            if (candidateLocation != null && !locations.Contains(candidateLocation.Value))
                ReportError(MisplacedDirectiveMessage(name, candidateLocation.Value.ToStringValue()), node);

            return VisitAction.Continue;
        }

        private static DirectiveLocation? GetDirectiveLocationForAstPath(
            IReadOnlyCollection<SyntaxNode> ancestors)
        {
            var appliedTo = ancestors.ElementAt(0);
            switch (appliedTo)
            {
                case OperationDefinitionSyntax op:
                    {
                        switch (op.OperationType)
                        {
                            case OperationType.Query:
                                return DirectiveLocation.Query;
                            case OperationType.Mutation:
                                return DirectiveLocation.Mutation;
                            case OperationType.Subscription:
                                return DirectiveLocation.Subscription;
                        }

                        break;
                    }
                case FieldSyntax _:
                    return DirectiveLocation.Field;
                case FragmentSpreadSyntax _:
                    return DirectiveLocation.FragmentSpread;
                case InlineFragmentSyntax _:
                    return DirectiveLocation.InlineFragment;
                case FragmentDefinitionSyntax _:
                    return DirectiveLocation.FragmentDefinition;

                case SchemaDefinitionSyntax _:
                case SchemaExtensionSyntax _:
                    return DirectiveLocation.Schema;
                case ScalarTypeDefinitionSyntax _:
                case ScalarTypeExtensionSyntax _:
                    return DirectiveLocation.Scalar;
                case ObjectTypeDefinitionSyntax _:
                case ObjectTypeExtensionSyntax _:
                    return DirectiveLocation.Object;
                case FieldDefinitionSyntax _:
                    return DirectiveLocation.FieldDefinition;
                case InterfaceTypeDefinitionSyntax _:
                case InterfaceTypeExtensionSyntax _:
                    return DirectiveLocation.Interface;
                case UnionTypeDefinitionSyntax _:
                case UnionTypeExtensionSyntax _:
                    return DirectiveLocation.Union;
                case EnumTypeDefinitionSyntax _:
                case EnumTypeExtensionSyntax _:
                    return DirectiveLocation.Enum;
                case EnumValueSyntax _:
                    return DirectiveLocation.EnumValue;
                case InputObjectTypeDefinitionSyntax _:
                case InputObjectTypeExtensionSyntax _:
                    return DirectiveLocation.InputObject;
                case InputValueDefinitionSyntax _:
                    {
                        var parentNode = ancestors.ElementAt(2);
                        return parentNode is InputObjectTypeDefinitionSyntax
                            ? DirectiveLocation.InputFieldDefinition
                            : DirectiveLocation.ArgumentDefinition;
                    }
            }

            return null;
        }
    }
}