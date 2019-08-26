// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel.Validation.Rules
{
    public class EnumTypesMustBeWellDefined : DocumentValidationRuleVisitor
    {
        private readonly Dictionary<string, ICollection<EnumTypeDefinitionSyntax>> _enumDefs =
            new Dictionary<string, ICollection<EnumTypeDefinitionSyntax>>();


        private readonly Dictionary<string, ICollection<EnumTypeExtensionSyntax>> _enumExts =
            new Dictionary<string, ICollection<EnumTypeExtensionSyntax>>();


        public EnumTypesMustBeWellDefined(DocumentValidationContext context) : base(context)
        {
        }

        public override VisitAction EnterEnumTypeDefinition(EnumTypeDefinitionSyntax node)
        {
            _enumDefs.AddItem(node.Name.Value, node);
            return false;
        }

        public override VisitAction EnterEnumTypeExtension(EnumTypeExtensionSyntax node)
        {
            _enumExts.AddItem(node.Name.Value, node);
            return false;
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            foreach (var enums in _enumDefs)
            {
                var enumTypeName = enums.Key;
                var enumExts = _enumExts.GetItems(enumTypeName);
                // ReSharper disable once PossibleNullReferenceException

                var values = enums.Value.SelectMany(_ => _.Values)
                    // ReSharper disable once PossibleNullReferenceException
                    .Concat(enumExts.SelectMany(_ => _.Values)).ToArray();

                if (!values.Any())
                {
                    // ReSharper disable twice PossibleNullReferenceException
                    var nodes = enums.Value.Select(_ => _.Name).Concat(enumExts.Select(_ => _.Name))
                        .ToArray<SyntaxNode>();
                    ReportError($"Enum type {enumTypeName} must define one or more values.", nodes);
                }

                var duplicateValuesByName = values.GroupBy(_ => _.Value.Value).Where(_ => _.Count() > 1);
                foreach (var duplicates in duplicateValuesByName)
                {
                    var nodes = duplicates.Select(_ => _.Value).ToArray<SyntaxNode>();
                    ReportError($"Enum type {enumTypeName} can include value {duplicates.First()} only once.",
                        nodes);
                }
            }

            return false;
        }
    }
}