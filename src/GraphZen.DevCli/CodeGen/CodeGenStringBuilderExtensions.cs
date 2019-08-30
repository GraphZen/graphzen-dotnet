using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class CodeGenStringBuilderExtensions
    {
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