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

namespace GraphZen.CodeGen
{
    internal class GenAccessorsTask : ReflectionCodeGenTask
    {
        public GenAccessorsTask(Type targetType, MemberInfo member, GenDictionaryAccessorsAttribute memberAttribute) :
            base(targetType)
        {
            Member = member;
            MemberAttribute = memberAttribute;
        }

        public MemberInfo Member { get; }
        public GenDictionaryAccessorsAttribute MemberAttribute { get; }

        public static IEnumerable<GenAccessorsTask> FromTypes(IReadOnlyList<Type> types)
        {
            foreach (var sourceType in types)
            {
                foreach (var member in sourceType.GetMembers())
                {
                    var genAccessors = member.GetCustomAttribute<GenDictionaryAccessorsAttribute>();
                    if (genAccessors != null)
                    {
                        var targetTypes = types.Where(t => sourceType.IsAssignableFrom(t));
                        foreach (var targetType in targetTypes)
                        {
                            yield return new GenAccessorsTask(targetType, member, genAccessors);
                        }
                    }
                }
            }
        }

        public override void Apply(StringBuilder csharp)
        {
            if (Member is PropertyInfo prop)
            {
                csharp.AppendLine($"// MemberType: {prop.MemberType}");
                csharp.AppendLine($"// PropertyType: {prop.PropertyType}");
            }
            else
            {
                throw new NotImplementedException(
                    $"{nameof(GenDictionaryAccessorsAttribute)} is not supported for {Member.GetType()} member types");
            }
        }
    }
}