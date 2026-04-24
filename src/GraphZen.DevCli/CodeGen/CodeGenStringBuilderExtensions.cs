// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Text;
using GraphZen.Infrastructure;

namespace GraphZen.CodeGen;

public static class CodeGenStringBuilderExtensions
{
    public static void AppendDictionaryAccessor(this StringBuilder csharp,
        string containerType, string propertyName, string keyName, string keyType,
        string valueName, string valueType)
    {
        var thisRefName = "source";
        var valueNameCamelized = valueName.FirstCharToLower();
        var valueRefName = valueType.FirstCharToLower();
        // Strip leading 'I' prefix from interface names to avoid class names that look like interfaces
        var classPrefix = containerType.Length > 1 && containerType[0] == 'I' && char.IsUpper(containerType[1])
            ? containerType.Substring(1)
            : containerType;
        var code = $@"

 public static partial class {classPrefix}{propertyName}AccessorExtensions {{

        
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
