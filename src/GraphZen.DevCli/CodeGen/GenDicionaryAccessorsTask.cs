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
    internal class GenDicionaryAccessorsTask : ReflectionCodeGenTask
    {
        public GenDicionaryAccessorsTask(Type targetType, MemberInfo member,
            GenDictionaryAccessorsAttribute memberAttribute) :
            base(targetType)
        {
            Member = member;
            MemberAttribute = memberAttribute;
        }

        public MemberInfo Member { get; }
        public GenDictionaryAccessorsAttribute MemberAttribute { get; }

        public static IEnumerable<GenDicionaryAccessorsTask> FromTypes(IReadOnlyList<Type> types)
        {
            foreach (var sourceType in types)
            {
                foreach (var member in sourceType.GetMembers())
                {
                    var genAccessors = member.GetCustomAttribute<GenDictionaryAccessorsAttribute>();
                    if (genAccessors != null)
                        yield return new GenDicionaryAccessorsTask(sourceType, member, genAccessors);
                }
            }
        }

        public override void Apply(StringBuilder csharp)
        {
            switch (Member)
            {
                case PropertyInfo prop:
                {
                    var propertyName = prop.Name;
                    var keyName = MemberAttribute.KeyName ?? "name";
                    var keyType = prop.PropertyType.GetGenericArguments()[0].Name;
                    var valueType = prop.PropertyType.GetGenericArguments()[1].Name;
                    var valueName = MemberAttribute.ValueName;
                    var valueNameCamelized = valueName.FirstCharToLower();
                    var valueRefName = valueType.FirstCharToLower();

                    csharp.AppendLine($@"
      public {valueType}? Find{valueName}({keyType} {keyName}) 
            => {propertyName}.TryGetValue(Check.NotNull({keyName},nameof({keyName})), out var {keyName}{valueName}) ? {keyName}{valueName} : null;

        public bool Has{valueName}({keyType} {keyName}) 
            => {propertyName}.ContainsKey(Check.NotNull({keyName}, nameof({keyName})));

        
        public {valueType} Get{valueName}({keyType} {keyName}) 
            => Find{valueName}(Check.NotNull({keyName}, nameof({keyName}))) ?? throw new Exception($""{{this}} does not contain a {valueNameCamelized} named '{{{keyName}}}'."");


        public bool TryGet{valueName}({keyType} {keyName}, [NotNullWhen(true)] out {valueType}? {valueRefName})
             => {propertyName}.TryGetValue(Check.NotNull({keyName}, nameof({keyName})), out {valueRefName});
 

");
                    break;
                }
                default:
                    throw new NotImplementedException(
                        $"{nameof(GenDictionaryAccessorsAttribute)} is not supported for {Member.GetType()} member types");
            }
        }
    }
}