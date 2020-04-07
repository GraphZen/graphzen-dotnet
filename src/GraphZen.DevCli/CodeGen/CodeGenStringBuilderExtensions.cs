// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class CodeGenStringBuilderExtensions
    {
        public static void AddCommonUsings(this StringBuilder csharp) =>
            csharp.AppendLine(CodeGenConstants.CommonUsings);

        public static void Namespace(this StringBuilder csharp, string name, Action<StringBuilder> @namespace) =>
            csharp.Block($"namespace {name} {{", "}", @namespace);

        public static void Class(this StringBuilder csharp, string qualifiers, string name,
            Action<StringBuilder> @class) => csharp.Block($"public {qualifiers} class {name} {{", "}", @class);

        public static void AbstractPartialClass(this StringBuilder csharp, string name, Action<StringBuilder> @class) =>
            csharp.Class("abstract partial", name, @class);

        public static void PartialClass(this StringBuilder csharp, string name, Action<StringBuilder> @class) =>
            csharp.Class("partial", name, @class);

        public static void StaticClass(this StringBuilder csharp, string name, Action<StringBuilder> @class) =>
            csharp.Class("static", name, @class);

        public static void Region(this StringBuilder csharp, string name, Action<StringBuilder> region) =>
            csharp.Block($"#region {name}", "#endregion", region);

        public static void Block(this StringBuilder csharp, string open, string close,
            Action<StringBuilder> content)
        {
            csharp.Append(open);
            content(csharp);
            csharp.Append(close);
        }

        public static void WriteToFile(this StringBuilder csharp, string project, string name) =>
            CodeGenHelpers.WriteFile($"./src/Linked/{project}/{name}.Generated.cs", csharp.ToString());


        public static void AppendDictionaryAccessor(this StringBuilder csharp,
            string containerType, string propertyName, string keyName, string keyType,
            string valueName, string valueType)
        {
            var thisRefName = "source";
            var valueNameCamelized = valueName.FirstCharToLower();
            var valueRefName = valueType.FirstCharToLower();
            var code = $@"

 public static partial class {containerType}{propertyName}AccessorExtensions {{

        
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