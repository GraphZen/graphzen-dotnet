// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation.Rules
{
    public class ObjectsMustAdhereToInterfaceTheyImplement : DocumentValidationRuleVisitor
    {
        public ObjectsMustAdhereToInterfaceTheyImplement(DocumentValidationContext context) : base(context)
        {
        }

        public override VisitAction LeaveDocument(DocumentSyntax schema)
        {
            var objectDefs = schema.Definitions.OfType<ObjectTypeDefinitionSyntax>().ToList();
            var objectExts = schema.Definitions.OfType<ObjectTypeExtensionSyntax>().ToList();

            // ReSharper disable once PossibleNullReferenceException
            var objectFieldMap = objectDefs.Select(_ => (_.Name, _.Fields)).Concat(objectExts
                // ReSharper disable once PossibleNullReferenceException
                .Select(_ => (_.Name, _.Fields))).ToDictionary(_ => _.Name, _ => _.Fields);
            var interfaceFieldMap = schema.Definitions.OfType<InterfaceTypeDefinitionSyntax>()
                .Select(_ => (_.Name, _.Fields)).Concat(schema.Definitions.OfType<InterfaceTypeExtensionSyntax>()
                    .Select(_ => (_.Name, _.Fields))).ToDictionary(_ => _.Name, _ => _.Fields);

            foreach (var objectType in objectDefs)
            {
                // ReSharper disable once PossibleNullReferenceException
                var exts = objectExts.Where(_ => _.Name.Equals(objectType.Name)).ToList();
                // ReSharper disable twice PossibleNullReferenceException
                var interfaces = objectType.Interfaces.Concat(exts.SelectMany(_ => _.Interfaces)).Select(_ => _.Name)
                    .ToList();

                // ReSharper disable once PossibleNullReferenceException
                // ReSharper disable once AssignNullToNotNullAttribute
                var fieldMap = objectFieldMap[objectType.Name].ToDictionary(_ => _.Name);

                foreach (var @interface in interfaces)
                {
                    if (interfaceFieldMap.TryGetValue(@interface, out var interfaceFields))
                    {
                        foreach (var interfaceField in interfaceFields)
                        {
                            if (fieldMap.TryGetValue(interfaceField.Name, out var objectField))
                            {
                                if (!schema.IsTypeSubTypeOf(objectField.FieldType, interfaceField.FieldType))
                                {
                                    ReportError(
                                        $"Interface field {@interface}.{interfaceField} expects type {interfaceField.FieldType} but {objectType}.{objectField} is type {objectField.FieldType}.",
                                        objectField.FieldType, interfaceField.FieldType);
                                }

                                foreach (var interfaceArg in interfaceField.Arguments)
                                {
                                    var objectArg =
                                        objectField.Arguments.FirstOrDefault(_ => _.Name.Equals(interfaceArg.Name));
                                    if (objectArg == null)
                                    {
                                        ReportError(
                                            $"Interface field argument {@interface}.{interfaceField}({interfaceArg}:) expected but {objectType}.{objectField} does not provide it.",
                                            interfaceArg, objectField);
                                    }
                                    else if (!objectArg.Type.Equals(interfaceArg.Type))
                                    {
                                        ReportError(
                                            $"Interface field argument {@interface}.{interfaceField}({interfaceArg}:) expects type {interfaceArg.Type} but {objectType}.{objectField}({objectArg}:) is type {objectArg.Type}.",
                                            interfaceArg.Type, objectArg.Type);
                                    }
                                }

                                foreach (var objectArg in objectField.Arguments)
                                {
                                    var interfaceArg =
                                        interfaceField.Arguments.FirstOrDefault(_ => _.Name.Equals(objectArg.Name));
                                    if (interfaceArg == null && objectArg.IsRequiredArgument())
                                    {
                                        ReportError(
                                            $"Object field {objectType}.{objectField} includes required argument {objectArg} that is missing from the Interface field {@interface}.{interfaceField}.",
                                            objectArg, interfaceField);
                                    }
                                }
                            }
                            else
                            {
                                ReportError(
                                    $"Interface field {@interface}.{interfaceField} expected but {objectType} does not provide it.",
                                    interfaceField, objectType.Name);
                            }
                        }
                    }
                }

                // Validate object impelments interfaces
            }


            return false;
        }
    }
}