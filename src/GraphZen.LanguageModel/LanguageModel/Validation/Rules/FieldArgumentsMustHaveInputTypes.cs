// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel.Validation.Rules
{
    public class FieldArgumentsMustHaveInputTypes : DocumentValidationRuleVisitor
    {
        public FieldArgumentsMustHaveInputTypes(DocumentValidationContext context) : base(context)
        {
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            var inputTypes = node.GetInputTypeDefinitions();
            var interfaceFields = node.Definitions.OfType<InterfaceTypeDefinitionSyntax>()
                .SelectMany(_ => _.Fields.Select(f => (_.Name, f)));
            var interfaceExtFields = node.Definitions.OfType<InterfaceTypeExtensionSyntax>()
                .SelectMany(_ => _.Fields.Select(f => (_.Name, f)));
            var objectFields = node.Definitions.OfType<ObjectTypeDefinitionSyntax>()
                .SelectMany(_ => _.Fields.Select(f => (_.Name, f)));
            var objectExtFields = node.Definitions.OfType<ObjectTypeExtensionSyntax>()
                .SelectMany(_ => _.Fields.Select(f => (_.Name, f)));
            var args = interfaceFields.Concat(interfaceExtFields).Concat(objectFields).Concat(objectExtFields)
                .SelectMany(_ => _.f.Arguments.Select(arg => (_.Name, _.f, arg)));
            foreach (var (typeName, field, arg) in args)
            {
                var argNamedType = arg.Type.GetNamedType();
                var inputType = inputTypes.FirstOrDefault(_ => _.Name.Equals(argNamedType.Name));
                if (inputType == null)
                    ReportError(
                        $"The type of {typeName}.{field}({arg}:) must be Input Type but got: {arg.Type}.",
                        arg);
            }


            return false;
        }
    }
}