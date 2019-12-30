// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class GenerateSyntaxFactory
    {
        public static IEnumerable<Type> GetConstructableSyntaxTypes()
            => typeof(SyntaxNode).Assembly.GetTypes()
                .Where(_ => _.IsSubclassOf(typeof(SyntaxNode)) && !_.IsAbstract);


        public static IEnumerable<(string className, string method)> GetFactoryMethod(Type type)
        {
            var name = type.Name;
            var methodName = name.Substring(0, name.LastIndexOf("Syntax", StringComparison.Ordinal));
            foreach (var ctor in type.GetConstructors())
            {
                var genFactory = ctor.GetCustomAttribute<GenFactory>();
                if (genFactory != null)
                {
                    var methodParams = ctor.GetParameters().Select(p =>
                    {
                                            return
                                $"{p.ParameterType.Name} {p.Name} {(p.HasDefaultValue ? " = " + p.DefaultValue : "")}";
                    });


                    var method = $"public static {type.Name} {methodName}() => new {name}({string.Join(", ", methodParams)})";
                    yield return (genFactory.FactoryClassName, method);
                }
            }
        }
    }
}