// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.CodeGenFx.Generators
{
    internal class DictionaryAccessorGenerator : PartialTypeGenerator
    {
        private DictionaryAccessorGenerator(MemberInfo member,
            GenDictionaryAccessorsAttribute attribute) :
            base(member.DeclaringType ?? throw new NotImplementedException())
        {
            Member = member;
            Attribute = attribute;
        }

        public MemberInfo Member { get; }

        public GenDictionaryAccessorsAttribute Attribute { get; }

        public static IEnumerable<DictionaryAccessorGenerator> FromTypeProperties(Type type)
        {
            foreach (var member in type
                .GetMembers(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public |
                            BindingFlags.NonPublic).Where(_ => _ is PropertyInfo || _ is FieldInfo))
            {
                var genAccessors = member.GetCustomAttribute<GenDictionaryAccessorsAttribute>();
                if (genAccessors != null)
                {
                    yield return new DictionaryAccessorGenerator(member, genAccessors);
                }
            }
        }

        public override void Apply(StringBuilder csharp)
        {
            var propertyName = Member.Name;
            var keyNameCamelized = Attribute.KeyName.FirstCharToLower();
            var memberType = Member is PropertyInfo p ? p.PropertyType :
                Member is FieldInfo f ? f.FieldType : throw new NotImplementedException();
            var gArgs = memberType.GetGenericArguments();
            if (gArgs.Length != 2)
            {
                throw new InvalidOperationException(
                    $"{nameof(DictionaryAccessorGenerator)}: {Member.DeclaringType?.Name}.{Member.Name} does not have two genric arguments. Is it a dictionary?");
            }

            var keyType = memberType.GetGenericArguments()[0].Name;
            var valueType = memberType.GetGenericArguments()[1].Name;
            var valueName = Attribute.ValueName;
            var valueNameCamelized = valueName.FirstCharToLower();
            var valueTypeCamelized = valueType.FirstCharToLower();
            var outValueVar = valueNameCamelized == keyNameCamelized
                ? "schemaBuilder" + valueNameCamelized
                : valueNameCamelized;

            csharp.AppendLine($@"


        [GraphQLIgnore]
        public {valueType}? Find{valueName}({keyType} {keyNameCamelized}) 
            => {propertyName}.TryGetValue(Check.NotNull({keyNameCamelized},nameof({keyNameCamelized})), out var {outValueVar}) ? {outValueVar} : null;

        [GraphQLIgnore]
        public bool Has{valueName}({keyType} {keyNameCamelized}) 
            => {propertyName}.ContainsKey(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized})));

        [GraphQLIgnore]
        public {valueType} Get{valueName}({keyType} {keyNameCamelized}) 
            => Find{valueName}(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized}))) ?? throw new ItemNotFoundException($""{{this}} does not contain a {{nameof({valueType})}} with {keyNameCamelized} '{{{keyNameCamelized}}}'."");

        [GraphQLIgnore]
        public bool TryGet{valueName}({keyType} {keyNameCamelized}, [NotNullWhen(true)] out {valueType}? {valueTypeCamelized})
             => {propertyName}.TryGetValue(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized})), out {valueTypeCamelized});

");
        }
    }
}