// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class FactoryGenerator
    {
        public static IEnumerable<Type> GetConstructableSyntaxTypes()
            => typeof(SyntaxNode).Assembly.GetTypes()
                .Where(_ => _.IsSubclassOf(typeof(SyntaxNode)) && !_.IsAbstract);


        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(SyntaxFactory).Assembly;

        }

        public static void GenerateFactoryMethods()
        {
            var assemblies = GetAssemblies();
            var types = assemblies.SelectMany(_ => _.GetTypes()).Where(_ =>
                _.GetConstructors().Any(ctor => ctor.GetCustomAttribute<GenFactory>() != null));

            var classes = types.SelectMany(GetFactoryMethods)
                .GroupBy(_ => new { _.ClassName, _.Namespace })
                .Select(g => (
                g.Key.ClassName,
                g.Key.Namespace,
                Methods: g.Select(_ => _.Method)
            )).ToList();
            foreach (var (className, @namespace, methods) in classes)
            {
                var csharp = CreateCSharpClass(className, @namespace, methods);
                CodeGenHelpers.WriteFile($"./src/Linked/{className}.Generated.cs", csharp);
            }
        }

        private static string CreateCSharpClass(string className, string @namespace, IEnumerable<string> methods)
        {
            return @$"

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;


#nullable enable

namespace {@namespace} {{
    public static partial class {className} {{
        {string.Join(Environment.NewLine, methods)}
    }}
}}
";
        }

        public static IEnumerable<(string ClassName, string Namespace, string Method)> GetFactoryMethods(Type type)
        {
            var name = type.Name;
            var methodName = name.Substring(0, name.LastIndexOf("Syntax", StringComparison.Ordinal));
            foreach (var ctor in type.GetConstructors())
            {
                var genFactory = ctor.GetCustomAttribute<GenFactory>();
                if (genFactory != null)
                {
                    var paramValues = ctor.GetParameters().Select(p =>
                    {
                        var parameterType = p.HasNullableReferenceType()
                            ? $"{p.ParameterType.FullName}?"
                            : p.ParameterType.FullName;
                        return
                            $"{parameterType} {p.Name} {(p.HasDefaultValue ? " = " + PrintDefaultValue(p.DefaultValue) : "")}";
                    });

                    var paramDisplay = string.Join(", ", paramValues);
                    var method =
                        $"public static {type.Name} {methodName}({paramDisplay}) => new {name}({paramDisplay});";
                    yield return (genFactory.FactoryClassName, type.Namespace!, method);
                }
            }
        }


        private static string PrintDefaultValue(object? value) =>
            value switch
            {
                bool bv => bv ? "true" : "false",
                string sv => $"\"{sv}\"",
                null => "null",
                _ => throw new NotImplementedException($"{nameof(PrintDefaultValue)}(typeof({value.GetType()}))")
            };
    }
}