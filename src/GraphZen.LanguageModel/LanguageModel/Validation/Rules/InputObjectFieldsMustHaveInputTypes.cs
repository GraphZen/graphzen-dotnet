#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation.Rules
{
    public class InputObjectFieldsMustHaveInputTypes : ValidationRuleVisitor
    {
        public InputObjectFieldsMustHaveInputTypes(ValidationContext context) : base(context)
        {
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            var inputTypes = node.GetInputTypeDefinitions();

            var inputFields = node.Definitions.OfType<InputObjectTypeDefinitionSyntax>()
                .SelectMany(io => io.Fields.Select(field => (io.Name, field)));

            foreach (var (inputObject, field) in inputFields)
            {
                var fieldNamedType = field.Type.GetNamedType();
                var inputFieldType = inputTypes.FirstOrDefault(_ => _.Name.Equals(fieldNamedType.Name));
                if (inputFieldType == null)
                {
                    ReportError($"The type of {inputObject}.{field} must be Input Type but got: {field.Type}.",
                        field.Type);
                }
            }

            return false;
        }
    }
}