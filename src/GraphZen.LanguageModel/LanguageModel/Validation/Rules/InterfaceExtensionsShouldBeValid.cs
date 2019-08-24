#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation.Rules
{
    public class InterfaceExtensionsShouldBeValid : DocumentValidationRuleVisitor
    {
        public InterfaceExtensionsShouldBeValid(DocumentValidationContext context) : base(context)
        {
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            var objectTypes = node.Definitions.OfType<ObjectTypeDefinitionSyntax>().ToList();
            var interfaceTypes = node.Definitions.OfType<InterfaceTypeDefinitionSyntax>().ToList();
            var interfaceExensions = node.Definitions.OfType<InterfaceTypeExtensionSyntax>().ToList();
            var objectTypeExtensions = node.Definitions.OfType<ObjectTypeExtensionSyntax>().ToList();

            foreach (var objectType in objectTypes)
            {
                var objectExts = objectTypeExtensions.Where(_ => _.Name.Value == objectType.Name.Value).ToList();
                var implementedInterfaces = objectType.Interfaces.Concat(objectExts.SelectMany(_ => _.Interfaces));
                var implementedFields = objectType.Fields.Concat(objectExts.SelectMany(_ => _.Fields)).ToList();

                foreach (var implementedInterface in implementedInterfaces)
                {
                    var expectedFields = interfaceTypes.Where(_ => _.Name.Value == implementedInterface.Name.Value)
                        .SelectMany(_ => _.Fields)
                        .Concat(interfaceExensions.Where(_ => _.Name.Value == implementedInterface.Name.Value)
                            .SelectMany(_ => _.Fields)).ToList();
                    foreach (var expectedField in expectedFields)
                    {
                        var implementedField =
                            implementedFields.FirstOrDefault(_ => _.Name.Value == expectedField.Name.Value);
                        if (implementedField == null)
                        {
                            var nodes = new List<SyntaxNode> { objectType.Name, expectedField }
                                .Concat(objectExts.Select(_ => _.Name)).ToList();
                            ReportError(
                                $"Interface field {implementedInterface}.{expectedField} expected but {objectType} does not provide it.",
                                nodes);
                        }
                        else
                        {
                            // TODO: check if implemented field type is a subtype of expected field type
                            if (!implementedField.FieldType.Equals(expectedField.FieldType))

                            {
                                ReportError(
                                    $"Interface field {implementedInterface}.{expectedField} expects type {expectedField.FieldType} but {objectType}.{implementedField} is type {implementedField.FieldType}.",
                                    expectedField.FieldType, implementedField.FieldType);
                            }

                            foreach (var expectedArg in expectedField.Arguments)
                            {
                                var implementedArg = implementedField.Arguments.FirstOrDefault(_ =>
                                    // ReSharper disable once PossibleNullReferenceException
                                    _.Name.Value == expectedArg.Name.Value && _.Type.Equals(expectedArg.Type));
                                if (implementedArg == null)
                                {
                                    ReportError(
                                        $"Interface field argument {implementedInterface}.{expectedField}({expectedArg.Name}:) expected but {objectType}.{implementedField} does not provide it.",
                                        expectedArg, implementedField);
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}