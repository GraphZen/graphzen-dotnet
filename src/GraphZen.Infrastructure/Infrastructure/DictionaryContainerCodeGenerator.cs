// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.Infrastructure
{
    public class DictionaryContainerCodeGenerator
    {
        private readonly StringBuilder _csharp = new StringBuilder();

        private DictionaryContainerCodeGenerator()
        {
        }

        public static string GenerateDictionaryAccessorExtensions(
            Action<DictionaryContainerCodeGenerator> config)
        {
            var generator = new DictionaryContainerCodeGenerator();
            config(generator);
            return generator._csharp.ToString();
        }


        public AccessorGenerator<TContainer> ForType<TContainer>() => new AccessorGenerator<TContainer>(_csharp);


        public class AccessorGenerator<TContainer>
        {
            private readonly StringBuilder _csharp;

            public AccessorGenerator(StringBuilder csharp)
            {
                _csharp = csharp;
            }

            public void ForDictionary<TKey, TValue>(
                Expression<Func<TContainer, IEnumerable<KeyValuePair<TKey, TValue>>>> selector,
                string keyName, string valueName, string thisRefName = null, string valueRefName = null)
            {
                Check.NotNull(selector, nameof(selector));
                var containerType = typeof(TContainer);
                var containerNamespace = containerType.Namespace;
                var extensionTypeName =
                    containerType.IsInterface ? containerType.Name.Substring(1) : containerType.Name;
                thisRefName = thisRefName ?? containerType.Name.FirstCharToLower();
                var keyType = typeof(TKey);
                var valueType = typeof(TValue);
                var valueNameCamelized = valueName.FirstCharToLower();
                valueRefName = valueRefName ?? valueType.Name.FirstCharToLower();
                var property = selector.GetPropertyInfoFromExpression();
                var code = $@"


 
namespace {containerNamespace} {{
 public static partial class {extensionTypeName}{valueName}AccessorExtensions {{

        
        public static {valueType} Find{valueName}( this {containerType} {thisRefName}, {keyType} {keyName}) 
            => {thisRefName}.{property.Name}.TryGetValue(Check.NotNull({keyName},nameof({keyName})), out var {keyName}{valueName}) ? {keyName}{valueName} : null;

        public static bool Has{valueName}( this {containerType} {thisRefName},  {keyType} {keyName}) 
            => {thisRefName}.{property.Name}.ContainsKey(Check.NotNull({keyName}, nameof({keyName})));

        
        public static {valueType} Get{valueName}( this {containerType} {thisRefName},  {keyType} {keyName}) 
            => {thisRefName}.Find{valueName}(Check.NotNull({keyName}, nameof({keyName}))) ?? throw new Exception($""{{{thisRefName}}} does not contain a {valueNameCamelized} named '{{{keyName}}}'."");

        public static bool TryGet{valueName}( this {containerType} {thisRefName},  {keyType} {keyName}, out {valueType} {valueRefName})
             => {thisRefName}.{property.Name}.TryGetValue(Check.NotNull({keyName}, nameof({keyName})), out {valueRefName});
}}
 

}}

";
                _csharp.Append(code);
            }
        }
    }
}