// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation.Rules
{
    public class InputObjectsMustHaveFields : DocumentValidationRuleVisitor
    {
        
        private readonly Dictionary<string, ICollection<InputObjectTypeDefinitionSyntax>>
            _inputDefs = new Dictionary<string, ICollection<InputObjectTypeDefinitionSyntax>>();

        
        private readonly Dictionary<string, ICollection<InputObjectTypeExtensionSyntax>> _inputExts =
            new Dictionary<string, ICollection<InputObjectTypeExtensionSyntax>>();

        public InputObjectsMustHaveFields(DocumentValidationContext context) : base(context)
        {
        }

        public override VisitAction EnterInputObjectTypeDefinition(InputObjectTypeDefinitionSyntax node)
        {
            _inputDefs.AddItem(node.Name.Value, node);
            return false;
        }

        public override VisitAction EnterInputObjectTypeExtension(InputObjectTypeExtensionSyntax node)
        {
            _inputExts.AddItem(node.Name.Value, node);
            return false;
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            var inputTypes = node.GetInputTypeDefinitions();
            foreach (var input in _inputDefs)
            {
                var inputExts = _inputExts.GetItems(input.Key);
                Debug.Assert(input.Value != null, "input.Value != null");
                var inputFields = input.Value
                    // ReSharper disable once PossibleNullReferenceException
                    .SelectMany(_ => _.Fields)
                    // ReSharper disable once PossibleNullReferenceException
                    .Concat(inputExts.SelectMany(_ => _.Fields))
                    .ToArray();
                if (!inputFields.Any())
                {
                    ReportError($"Input Object type {input.Key} must define one or more fields.",
                        // ReSharper disable twice PossibleNullReferenceException
                        input.Value.Select(_ => _.Name).Concat(inputExts.Select(_ => _.Name)).ToArray<SyntaxNode>());
                }

                foreach (var inputField in inputFields)
                {
                    var inputFieldNamedType = inputField.Type.GetNamedType();
                    var inputType = inputTypes.FirstOrDefault(_ => _.Name.Value == inputFieldNamedType.Name.Value);
                    if (inputType == null)
                    {
                        ReportError(
                            $"The type of {input.Key}.{inputField} must be Input Type but got: {inputField.Type}.",
                            inputField.Type);
                    }
                }
            }

            return false;
        }
    }
}