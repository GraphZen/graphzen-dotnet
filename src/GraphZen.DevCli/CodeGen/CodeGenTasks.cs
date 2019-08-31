// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class CodeGenTasks
    {

        public static void RunCodeGen()
        {
            GenerateTypeSystemDictionaryAccessors();
        }


        public static void GenerateTypeSystemDictionaryAccessors()
        {
            var csharp = new StringBuilder();

            csharp.Append(@"
using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

#nullable restore

namespace GraphZen.TypeSystem {
");


            var fieldDefinitionAccessors = new List<(string containerType, string valueType)>
            {
                ("InterfaceTypeDefinition", "FieldDefinition"),
                ("ObjectTypeDefinition", "FieldDefinition"),
                ("InputObjectTypeDefinition", "InputFieldDefinition"),
                ("FieldsContainerDefinition", "FieldDefinition")
            };

            foreach (var (containerType, valueType) in fieldDefinitionAccessors)
            {
                csharp.AppendDictionaryAccessor(
                    containerType, "Fields", "name", "string", "Field", valueType);
            }

            var fieldAccessors = new List<(string containerType, string valueType)>
            {
                ("InterfaceType", "Field"),
                ("ObjectType", "Field"),
                ("InputObjectType", "InputField")
            };

            foreach (var (containerType, valueType) in fieldAccessors)
            {
                csharp.AppendDictionaryAccessor(containerType, "Fields", "name", "string", "Field", valueType);
            }

            csharp.AppendDictionaryAccessor("EnumTypeDefinition", "Values", "name", "string", "Value", "EnumValueDefinition");
            csharp.AppendDictionaryAccessor("EnumType", "Values", "name", "string", "Value", "EnumValue");
            csharp.AppendDictionaryAccessor("EnumType", "ValuesByValue", "value", "object", "Value", "EnumValue");


            var argumentDefinitionAccessors = new List<(string containerType, string valueType)>
            {
                ("FieldDefinition", "ArgumentDefinition"),
                ("DirectiveDefinition", "ArgumentDefinition"),
            };

            foreach (var (containerType, valueType) in argumentDefinitionAccessors)
            {
                csharp.AppendDictionaryAccessor(
                    containerType, "Arguments", "name", "string", "Argument", valueType);
            }

            var argumentAccessors = new List<(string containerType, string valueType)>
            {
                ("Field", "Argument"),
                ("IArgumentsContainer", "Argument"),
            };

            foreach (var (containerType, valueType) in argumentAccessors)
            {
                csharp.AppendDictionaryAccessor(
                                    containerType, "Arguments", "name", "string", "Argument", valueType);

            }

            csharp.Append("}");
            CodeGenHelpers.WriteFile("../Linked/TypeSystem/TypeSystemAccessors.Generated.cs", csharp.ToString());
        }
    }
}