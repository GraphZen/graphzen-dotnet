// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation.Rules
{
    public class InterfaceFieldsMustHaveOutputTypes : DocumentValidationRuleVisitor
    {
        public InterfaceFieldsMustHaveOutputTypes(DocumentValidationContext context) : base(context)
        {
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            var outputTypes = node.GetOutputTypeDefinitions();
            var interfaceFields = node.Definitions.OfType<InterfaceTypeDefinitionSyntax>()
                .SelectMany(_ => _.Fields.Select(f => (_.Name, f)));
            var interfaceExtFields = node.Definitions.OfType<InterfaceTypeExtensionSyntax>()
                .SelectMany(_ => _.Fields.Select(f => (_.Name, f)));
            var objectFields = node.Definitions.OfType<ObjectTypeDefinitionSyntax>()
                .SelectMany(_ => _.Fields.Select(f => (_.Name, f)));
            var objectExtFields = node.Definitions.OfType<ObjectTypeExtensionSyntax>()
                .SelectMany(_ => _.Fields.Select(f => (_.Name, f)));
            var fields = interfaceFields.Concat(interfaceExtFields).Concat(objectFields).Concat(objectExtFields);

            foreach (var (typeName, field) in fields)
            {
                var fieldNamedType = field.FieldType.GetNamedType();

                var outputType = outputTypes.FirstOrDefault(_ => _.Name.Equals(fieldNamedType.Name));
                if (outputType == null)
                {
                    ReportError(
                        $"The type of {typeName}.{field} must be Output Type but got: {field.FieldType}.",
                        field.FieldType);
                }
            }

            return false;
        }
    }
}