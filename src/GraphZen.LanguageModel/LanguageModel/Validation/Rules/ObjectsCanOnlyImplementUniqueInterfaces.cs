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
    public class ObjectsCanOnlyImplementUniqueInterfaces : DocumentValidationRuleVisitor
    {
        public ObjectsCanOnlyImplementUniqueInterfaces(DocumentValidationContext context) : base(context)
        {
        }

        public override VisitAction LeaveDocument(DocumentSyntax node)
        {
            var interfaces = new HashSet<string>(
                node.Definitions.OfType<InterfaceTypeDefinitionSyntax>().Select(_ => _.Name.Value)
            );
            var objectTypeExtensions = node.Definitions.OfType<ObjectTypeExtensionSyntax>().ToList();
            foreach (var objectType in node.Definitions.OfType<ObjectTypeDefinitionSyntax>())
            {
                var objectName = objectType.Name.Value;
                // ReSharper disable once PossibleNullReferenceException
                var objectExtInterfaces = objectTypeExtensions.Where(_ => _.Name.Value == objectName)
                    .SelectMany(_ => _.Interfaces);
                var implementedInterfaces = objectType.Interfaces.Concat(objectExtInterfaces);
                // ReSharper disable once PossibleNullReferenceException
                foreach (var interfacesByName in implementedInterfaces.GroupBy(_ => _.Name.Value)
                    .Select(_ => _.ToList()))
                {
                    var @interface = interfacesByName[0];
                    // ReSharper disable once PossibleNullReferenceException
                    var interfaceName = @interface.Name.Value;
                    if (!interfaces.Contains(interfaceName))
                        ReportError(
                            $"Type {objectName} must only implement Interface types, it cannot implement {interfaceName}.",
                            @interface);

                    if (interfacesByName.Count > 1)
                        ReportError($"Type {objectName} can only implement {interfaceName} once.", interfacesByName);
                }
            }

            return false;
        }
    }
}