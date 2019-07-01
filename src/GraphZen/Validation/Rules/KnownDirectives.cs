// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.Language;
using GraphZen.Language.Internal;
using JetBrains.Annotations;
using static GraphZen.Language.DirectiveLocation;

namespace GraphZen.Validation.Rules
{
    public class KnownDirectives : QueryValidationRuleVisitor
    {
        [NotNull]
        private readonly Lazy<IReadOnlyDictionary<string, IReadOnlyList<DirectiveLocation>>> _lazyLocationsMap;


        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public KnownDirectives(QueryValidationContext context) : base(context)
        {
            _lazyLocationsMap = new Lazy<IReadOnlyDictionary<string, IReadOnlyList<DirectiveLocation>>>(() =>
                Context.Schema.Directives.ToReadOnlyDictionary(_ => _.Name, _ => _.Locations));
        }

        [NotNull]
        // ReSharper disable once AssignNullToNotNullAttribute
        private IReadOnlyDictionary<string, IReadOnlyList<DirectiveLocation>> LocationsMap => _lazyLocationsMap.Value;


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
            {
                ReportError(MisplacedDirectiveMessage(name, candidateLocation.Value.ToStringValue()), node);
            }

            return VisitAction.Continue;
        }

        private static DirectiveLocation? GetDirectiveLocationForAstPath(
            [NotNull] IReadOnlyCollection<SyntaxNode> ancestors)
        {
            var appliedTo = ancestors.ElementAt(0);
            switch (appliedTo)
            {
                case OperationDefinitionSyntax op:
                {
                    switch (op.OperationType)
                    {
                        case OperationType.Query:
                            return Query;
                        case OperationType.Mutation:
                            return Mutation;
                        case OperationType.Subscription:
                            return Subscription;
                    }

                    break;
                }
                case FieldSyntax _:
                    return Field;
                case FragmentSpreadSyntax _:
                    return FragmentSpread;
                case InlineFragmentSyntax _:
                    return InlineFragment;
                case FragmentDefinitionSyntax _:
                    return FragmentDefinition;

                case SchemaDefinitionSyntax _:
                case SchemaExtensionSyntax _:
                    return Schema;
                case ScalarTypeDefinitionSyntax _:
                case ScalarTypeExtensionSyntax _:
                    return Scalar;
                case ObjectTypeDefinitionSyntax _:
                case ObjectTypeExtensionSyntax _:
                    return DirectiveLocation.Object;
                case FieldDefinitionSyntax _:
                    return FieldDefinition;
                case InterfaceTypeDefinitionSyntax _:
                case InterfaceTypeExtensionSyntax _:
                    return Interface;
                case UnionTypeDefinitionSyntax _:
                case UnionTypeExtensionSyntax _:
                    return Union;
                case EnumTypeDefinitionSyntax _:
                case EnumTypeExtensionSyntax _:
                    return DirectiveLocation.Enum;
                case EnumValueSyntax _:
                    return EnumValue;
                case InputObjectTypeDefinitionSyntax _:
                case InputObjectTypeExtensionSyntax _:
                    return InputObject;
                case InputValueDefinitionSyntax _:
                {
                    var parentNode = ancestors.ElementAt(2);
                    return parentNode is InputObjectTypeDefinitionSyntax
                        ? InputFieldDefinition
                        : ArgumentDefinition;
                }
            }

            return null;
        }
    }
}