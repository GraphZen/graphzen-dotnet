// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel.Validation.Rules
{
    public class ObjectFieldsMustHaveOutputTypes : DocumentValidationRuleVisitor
    {
        public ObjectFieldsMustHaveOutputTypes(DocumentValidationContext context) : base(context)
        {
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            var outputTypes = node.GetOutputTypeDefinitions();
            foreach (var objectType in outputTypes.OfType<ObjectTypeDefinitionSyntax>())
            foreach (var field in objectType.Fields)
            {
                var innerFieldType = field.FieldType.GetNamedType();
                var isOutputType = outputTypes.Any(_ => _.Name.Value == innerFieldType.Name.Value);
                if (!isOutputType)
                    ReportError(
                        $"The type of {objectType}.{field} must be Output Type but got: {field.FieldType}.",
                        field.FieldType);
            }

            return false;
        }
    }
}