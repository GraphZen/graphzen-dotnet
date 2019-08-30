// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public static class CodeGenHelpers
    {

        public static void GenerateFile(string path, string contents)
        {
            Console.Write($"Generating file: {path} ");
            if (File.Exists(path))
            {
                Console.Write("(deleted existing)");
                File.Delete(path);
            }
            Console.Write(" (writing ...");
            File.AppendAllText(path, contents);
            Console.WriteLine(" done)");
        }


        public static void GenerateTypeSystemDictionaryAccessors()
        {

            var csharp = new StringBuilder();

            csharp.Append(@"
using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
// ReSharper disable PartialTypeWithSinglePart

#nullable restore

namespace GraphZen.TypeSystem {
");

            var fieldDefinitionAccessors = new List<(string containerType, string valueType)>
            {
                ("InterfaceTypeDefinition", "FieldDefinition"),
                ("ObjectTypeDefinition", "FieldDefinition"),
                ("InputObjectTypeDefinition", "InputFieldDefinition"),
                ("FieldsContainerDefinition", "FieldDefinition"),
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
                ("InputObjectType", "InputField"),
            };

            foreach (var (containerType, valueType) in fieldAccessors)
            {
                csharp.AppendDictionaryAccessor( containerType, "Fields", "name", "string", "Field", valueType);
            }




            csharp.Append("}");
            GenerateFile("../GraphZen.TypeSystem/TypeSystem/TypeSystemAccessors.Generated.cs", csharp.ToString());
        }


        public static void AppendDictionaryAccessor(this StringBuilder csharp,
            string containerType, string propertyName, string keyName, string keyType,
            string valueName, string valueType)
        {
            var thisRefName = "source";
            var valueNameCamelized = valueName.FirstCharToLower();
            var valueRefName = valueType.FirstCharToLower();
            var code = $@"

 public static partial class {containerType}{valueName}AccessorExtensions {{

        
        public static {valueType}? Find{valueName}( this {containerType} {thisRefName}, {keyType} {keyName}) 
            => {thisRefName}.{propertyName}.TryGetValue(Check.NotNull({keyName},nameof({keyName})), out var {keyName}{valueName}) ? {keyName}{valueName} : null;

        public static bool Has{valueName}( this {containerType} {thisRefName},  {keyType} {keyName}) 
            => {thisRefName}.{propertyName}.ContainsKey(Check.NotNull({keyName}, nameof({keyName})));

        
        public static {valueType} Get{valueName}( this {containerType} {thisRefName},  {keyType} {keyName}) 
            => {thisRefName}.Find{valueName}(Check.NotNull({keyName}, nameof({keyName}))) ?? throw new Exception($""{{{thisRefName}}} does not contain a {valueNameCamelized} named '{{{keyName}}}'."");

        public static bool TryGet{valueName}( this {containerType} {thisRefName},  {keyType} {keyName}, [NotNullWhen(true)] out {valueType}? {valueRefName})
             => {thisRefName}.{propertyName}.TryGetValue(Check.NotNull({keyName}, nameof({keyName})), out {valueRefName});
}}
 


";
            csharp.Append(code);
        }
    }
}