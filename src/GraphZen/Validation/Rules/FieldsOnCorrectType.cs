// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Validation.Rules
{
    public class FieldsOnCorrectType : QueryValidationRuleVisitor
    {
        public FieldsOnCorrectType(QueryValidationContext context) : base(context)
        {
        }

        public static string UndefinedFieldMessage(
            [NotNull] string fieldName,
            [NotNull] string type,
            [NotNull] [ItemNotNull] IEnumerable<string> suggestedTypeNames,
            [NotNull] [ItemNotNull] IEnumerable<string> suggestedFieldNames)
        {
            var typeNames = suggestedTypeNames.ToArray();
            var fieldNames = suggestedFieldNames.ToArray();
            var message = $"Cannot query field \"{fieldName}\" on type \"{type}\".";
            if (typeNames.Any())
            {
                message += $" Did you mean to use an inline fragment on {typeNames.QuotedOrList()}?";
            }
            else if (fieldNames.Any())
            {
                message += $" Did you mean {fieldNames.QuotedOrList()}?";
            }

            return message;
        }

        public override VisitAction EnterField(FieldSyntax node)
        {
            var type = Context.GetParentType();
            if (type != null)
            {
                var fieldDef = Context.GetFieldDef();
                if (fieldDef == null)
                {
                    // ReSharper disable once UnusedVariable
                    var fieldName = node.Name.Value;
                    var suggestedTypeNames = GetSuggestedTypeNames(type, fieldName);
                    var suggestedFieldNames = suggestedTypeNames.Any()
                        ? Array.Empty<string>()
                        : GetSuggestedFieldNames(type, fieldName);

                    ReportError(UndefinedFieldMessage(fieldName, type.Name, suggestedTypeNames, suggestedFieldNames),
                        node);
                }
            }

            return VisitAction.Continue;
        }

        [NotNull]
        private IReadOnlyList<string> GetSuggestedTypeNames(IGraphQLType type, [NotNull] string fieldName)
        {
            if (type is IAbstractType abstractType)
            {
                var suggestedObjectTypes = new List<string>();
                var interfaceUsageCount = new Dictionary<string, int>();
                foreach (var possibleType in Context.Schema.GetPossibleTypes(abstractType))
                {
                    if (possibleType.FindField(fieldName) == null)
                    {
                        continue;
                    }

                    suggestedObjectTypes.Add(possibleType.Name);
                    foreach (var possibleInterface in possibleType.Interfaces)
                    {
                        if (possibleInterface.FindField(fieldName) == null)
                        {
                            continue;
                        }

                        interfaceUsageCount.Increment(possibleInterface.Name);
                    }
                }

                var suggestedInterfaceTypes =
                    interfaceUsageCount.OrderByDescending(_ => _.Value).Select(_ => _.Key);

                return suggestedInterfaceTypes.Concat(suggestedObjectTypes).ToArray();
            }


            return Array.Empty<string>();
        }

        [NotNull]
        private IReadOnlyList<string> GetSuggestedFieldNames(IGraphQLType type, [NotNull] string fieldName)
        {
            if (type is IFieldsContainer fields)
            {
                // ReSharper disable once UnusedVariable
                var possibleFieldNames = fields.Fields.Keys;
                return StringUtils.GetSuggestionList(fieldName, possibleFieldNames);
            }

            return Array.Empty<string>();
        }
    }
}