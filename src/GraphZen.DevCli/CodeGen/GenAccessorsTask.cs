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
        public GenAccessorsTask(Type targetType, MemberInfo member, GenAccessorExtensionsAttribute memberAttribute) :
            base(targetType)
        {
            Member = member;
            MemberAttribute = memberAttribute;
        }

        public MemberInfo Member { get; }
        public GenAccessorExtensionsAttribute MemberAttribute { get; }

        public static IEnumerable<GenAccessorsTask> FromTypes(IReadOnlyList<Type> types)
        {
            foreach (var sourceType in types)
            {
                foreach (var member in sourceType.GetMembers())
                {
                    var genAccessors = member.GetCustomAttribute<GenAccessorExtensionsAttribute>();
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
            csharp.AppendLine("// hello world");
        }
    }
}