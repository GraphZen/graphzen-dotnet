// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    internal class DictionaryAccessorGenerator : PartialTypeGenerator
    {
        public DictionaryAccessorGenerator(PropertyInfo property,
            GenDictionaryAccessorsAttribute attribute) :
            base(property.DeclaringType ?? throw new NotImplementedException())
        {
            Property = property;
            Attribute = attribute;
        }

        public PropertyInfo Property { get; }
        public GenDictionaryAccessorsAttribute Attribute { get; }

        public static IEnumerable<DictionaryAccessorGenerator> FromTypeProperties(Type type)
        {
            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly |
                                                        BindingFlags.Public))
            {
                var genAccessors = property.GetCustomAttribute<GenDictionaryAccessorsAttribute>();
                if (genAccessors != null) yield return new DictionaryAccessorGenerator(property, genAccessors);
            }
        }

        public override void Apply(StringBuilder csharp)
        {
            var propertyName = Property.Name;
            var keyNameCamelized = Attribute.KeyName.FirstCharToLower();
            var keyType = Property.PropertyType.GetGenericArguments()[0].Name;
            var valueType = Property.PropertyType.GetGenericArguments()[1].Name;
            var valueName = Attribute.ValueName;
            var valueNameCamelized = valueName.FirstCharToLower();
            var valueTypeCamelized = valueType.FirstCharToLower();
            var outValueVar = valueNameCamelized == keyNameCamelized ? "_" + valueNameCamelized : valueNameCamelized;

            csharp.AppendLine($@"
        public {valueType}? Find{valueName}({keyType} {keyNameCamelized}) 
            => {propertyName}.TryGetValue(Check.NotNull({keyNameCamelized},nameof({keyNameCamelized})), out var {outValueVar}) ? {outValueVar} : null;

        public bool Has{valueName}({keyType} {keyNameCamelized}) 
            => {propertyName}.ContainsKey(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized})));
        
        public {valueType} Get{valueName}({keyType} {keyNameCamelized}) 
            => Find{valueName}(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized}))) ?? throw new Exception($""{{this}} does not contain a {{nameof({valueType})}} with {keyNameCamelized} '{{{keyNameCamelized}}}'."");


        public bool TryGet{valueName}({keyType} {keyNameCamelized}, [NotNullWhen(true)] out {valueType}? {valueTypeCamelized})
             => {propertyName}.TryGetValue(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized})), out {valueTypeCamelized});
");
        }
    }
}