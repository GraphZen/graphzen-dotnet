#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation.Rules
{
    public class UnionTypesMustBeValid : DocumentValidationRuleVisitor
    {
        [NotNull]
        private readonly Dictionary<string, ICollection<UnionTypeDefinitionSyntax>> _unionDefs =
            new Dictionary<string, ICollection<UnionTypeDefinitionSyntax>>();

        [NotNull]
        private readonly Dictionary<string, ICollection<UnionTypeExtensionSyntax>> _unionExts =
            new Dictionary<string, ICollection<UnionTypeExtensionSyntax>>();

        public UnionTypesMustBeValid(DocumentValidationContext context) : base(context)
        {
        }

        public override VisitAction EnterUnionTypeDefinition(UnionTypeDefinitionSyntax node)
        {
            _unionDefs.AddItem(node.Name.Value, node);
            return false;
        }

        public override VisitAction EnterUnionTypeExtension(UnionTypeExtensionSyntax node)
        {
            _unionExts.AddItem(node.Name.Value, node);
            return false;
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            foreach (var union in _unionDefs.Values.Select(_ =>
            {
                Debug.Assert(_ != null, nameof(_) + " != null");
                return _.First();
            }))
            {
                Debug.Assert(union != null, nameof(union) + " != null");
                var unionTypeName = union.Name.Value;
                var unionExtensions = _unionExts.GetItems(union.Name.Value).ToList();
                var nodes = new List<SyntaxNode> { union };
                nodes.AddRange(unionExtensions);
                // ReSharper disable once PossibleNullReferenceException
                var types = union.MemberTypes.Concat(unionExtensions.SelectMany(_ => _.Types)).ToList();
                if (!types.Any())
                {
                    ReportError($"Union type {unionTypeName} must define one or more member types.", nodes);
                }

                // ReSharper disable once PossibleNullReferenceException
                foreach (var duplicateTypes in types
                    .GroupBy(_ => _.Name.Value)
                    .Where(_ => _.Count() > 1))
                {
                    ReportError($"Union type {unionTypeName} can only include type {duplicateTypes.Key} once.",
                        duplicateTypes.ToList());
                }

                var objectTypes = node.Definitions.OfType<ObjectTypeDefinitionSyntax>().ToList();
                foreach (var type in types)
                {
                    var objectType = objectTypes.FirstOrDefault(_ =>
                    {
                        Debug.Assert(_ != null, nameof(_) + " != null");
                        return _.Name.Value == type.Name.Value;
                    });
                    if (objectType == null)
                    {
                        ReportError(
                            $"Union type {unionTypeName} can only include Object types, it cannot include {type.Name.Value}.",
                            type);
                    }
                }
            }

            return false;
        }
    }
}