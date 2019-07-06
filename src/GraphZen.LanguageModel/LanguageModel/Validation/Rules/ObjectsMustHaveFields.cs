// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation.Rules
{
    public class ObjectsMustHaveFields : ValidationRuleVisitor
    {
        [NotNull] private readonly Dictionary<string, ICollection<ObjectTypeDefinitionSyntax>> _objectDefs =
            new Dictionary<string, ICollection<ObjectTypeDefinitionSyntax>>();

        [NotNull] private readonly Dictionary<string, ICollection<ObjectTypeExtensionSyntax>> _objectExts =
            new Dictionary<string, ICollection<ObjectTypeExtensionSyntax>>();

        public ObjectsMustHaveFields(ValidationContext context) : base(context)
        {
        }

        public override VisitAction EnterObjectTypeDefinition(ObjectTypeDefinitionSyntax node)
        {
            _objectDefs.AddItem(node.Name.Value, node);
            return false;
        }

        public override VisitAction EnterObjectTypeExtension(ObjectTypeExtensionSyntax node)
        {
            _objectExts.AddItem(node.Name.Value, node);
            return false;
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            foreach (var objectDef in _objectDefs)
            {
                var objectTypeName = objectDef.Key;
                Debug.Assert(objectTypeName != null, nameof(objectTypeName) + " != null");
                Debug.Assert(objectDef.Value != null, "objectDef.Value != null");
                var objectTypeNode = objectDef.Value.First();
                var extensionNodes = _objectExts.GetItems(objectTypeName).ToList();
                Debug.Assert(objectTypeNode != null, nameof(objectTypeNode) + " != null");
                if (!objectTypeNode.Fields.Any() && !extensionNodes.SelectMany(n =>
                {
                    Debug.Assert(n != null, nameof(n) + " != null");
                    return n.Fields;
                }).Any())
                {
                    var nodes = new List<SyntaxNode>
                    {
                        objectTypeNode.Name
                    };
                    // ReSharper disable once PossibleNullReferenceException
                    nodes.AddRange(extensionNodes.Select(_ => _.Name));
                    ReportError($"Type {objectTypeName} must define one or more fields.", nodes);
                }
            }

            return false;
        }
    }
}