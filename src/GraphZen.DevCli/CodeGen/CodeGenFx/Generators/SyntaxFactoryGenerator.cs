// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.CodeGenFx.Generators
{
    internal class SyntaxFactoryGenerator : PartialTypeGenerator
    {
        public SyntaxFactoryGenerator(ConstructorInfo constructor, GenFactoryAttribute attribute) :
            base(attribute.FactoryType)
        {
            Constructor = constructor;
            Attribute = attribute;
        }

        public ConstructorInfo Constructor { get; }

        public GenFactoryAttribute Attribute { get; }

        private static string GetParameterType(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.FullName!;
            }

            var gargs = string.Join(", ", type.GetGenericArguments().Select(_ => _.FullName));
            return
                $"{type.Namespace}.{type.Name.Remove(type.Name.LastIndexOf("`", StringComparison.Ordinal))}<{gargs}>";
        }

        private static string PrintDefaultValue(object? value) =>
            value switch
            {
                bool bv => bv ? "true" : "false",
                string sv => $"\"{sv}\"",
                null => "null",
                _ => throw new NotImplementedException($"{nameof(PrintDefaultValue)}(typeof({value.GetType()}))")
            };


        public override void Apply(StringBuilder csharp)
        {
            var name = Constructor.DeclaringType!.Name;
            var methodName = name.EndsWith("Syntax")
                ? name.Substring(0, name.LastIndexOf("Syntax", StringComparison.Ordinal))
                : name;
            var methodParameters = Constructor.GetParameters().Select(p =>
            {
                var parameterType = p.HasNullableReferenceType()
                    ? $"{GetParameterType(p.ParameterType)}?"
                    : GetParameterType(p.ParameterType);

                var pararmsArray = p.GetCustomAttribute<ParamArrayAttribute>() != null ? "params" : "";
                return
                    $"{pararmsArray} {parameterType} {p.Name} {(p.HasDefaultValue ? " = " + PrintDefaultValue(p.DefaultValue) : "")}";
            });

            var parameters = string.Join(", ", methodParameters);
            var ctorParameters = string.Join(", ", Constructor.GetParameters().Select(p => p.Name));
            var method = $"public static {name} {methodName}({parameters}) => new {name}({ctorParameters});";
            csharp.AppendLine(method);
        }

        public static IEnumerable<SyntaxFactoryGenerator> FromTypeConstructors(Type type)
        {
            foreach (var ctor in type.GetConstructors())
            {
                var attr = ctor.GetCustomAttribute<GenFactoryAttribute>();
                if (attr != null)
                {
                    yield return new SyntaxFactoryGenerator(ctor, attr);
                }
            }
        }
    }
}