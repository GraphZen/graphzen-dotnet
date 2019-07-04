// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;


namespace GraphZen.Internal
{
    public class DictionaryContainerCodeGenerator
    {
        [NotNull] private readonly StringBuilder _csharp = new StringBuilder();

        private DictionaryContainerCodeGenerator()
        {
        }

        public static string GenerateDictionaryAccessorExtensions(
            [NotNull] Action<DictionaryContainerCodeGenerator> config)
        {
            var generator = new DictionaryContainerCodeGenerator();
            config(generator);
            return generator._csharp.ToString();
        }


        [NotNull]
        public AccessorGenerator<TContainer> ForType<TContainer>() => new AccessorGenerator<TContainer>(_csharp);


        public class AccessorGenerator<TContainer>
        {
            [NotNull] private readonly StringBuilder _csharp;

            public AccessorGenerator([NotNull] StringBuilder csharp)
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

        [CanBeNull]
        public static {valueType} Find{valueName}([NotNull] this {containerType} {thisRefName},[NotNull] {keyType} {keyName}) 
            => {thisRefName}.{property.Name}.TryGetValue(Check.NotNull({keyName},nameof({keyName})), out var {keyName}{valueName}) ? {keyName}{valueName} : null;

        public static bool Has{valueName}([NotNull] this {containerType} {thisRefName}, [NotNull] {keyType} {keyName}) 
            => {thisRefName}.{property.Name}.ContainsKey(Check.NotNull({keyName}, nameof({keyName})));

        [NotNull]
        public static {valueType} Get{valueName}([NotNull] this {containerType} {thisRefName}, [NotNull] {keyType} {keyName}) 
            => {thisRefName}.Find{valueName}(Check.NotNull({keyName}, nameof({keyName}))) ?? throw new Exception($""{{{thisRefName}}} does not contain a {valueNameCamelized} named '{{{keyName}}}'."");

        public static bool TryGet{valueName}([NotNull] this {containerType} {thisRefName}, [NotNull] {keyType} {keyName}, out {valueType} {valueRefName})
             => {thisRefName}.{property.Name}.TryGetValue(Check.NotNull({keyName}, nameof({keyName})), out {valueRefName});
}}
 

}}

";
                _csharp.Append(code);
            }
        }
    }
}