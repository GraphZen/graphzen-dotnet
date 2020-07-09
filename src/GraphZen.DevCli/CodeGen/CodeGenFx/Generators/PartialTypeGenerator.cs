// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.CodeGenFx.Generators
{
    public abstract class PartialTypeGenerator<T> : PartialTypeGenerator
    {
        protected PartialTypeGenerator() : base(typeof(T))
        {
        }
    }

    public abstract class PartialTypeGenerator
    {
        private static readonly Lazy<IReadOnlyList<string>> CSharpFilesLazy =
            new Lazy<IReadOnlyList<string>>(() =>
            {

                var startingDir = Directory.GetCurrentDirectory();
                while (!File.Exists("GraphZen.sln"))
                {
                    Directory.SetCurrentDirectory("..");
                }
                
                var files = Directory.GetFiles(".", "*.cs", SearchOption.AllDirectories);

                return files;
            });

        protected PartialTypeGenerator(Type targetType)
        {
            TargetType = targetType;
            TypeShortName = GetShortName(targetType);
        }


        public string TypeShortName { get; }


        public Type TargetType { get; }

        public static IReadOnlyList<string> CSharpFiles => CSharpFilesLazy.Value;

        private static string GetShortName(Type type) => type.IsGenericType ? type.Name.Split("`")[0] : type.Name;

        private static string GetDisplayName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            var gType = type.GetGenericTypeDefinition();
            var gArgs = string.Join(",", gType.GetGenericArguments().Select(_ => _.Name));
            var name = gType.Name.Split("`")[0];
            return $"{name}<{gArgs}>";
        }

        public abstract void Apply(StringBuilder csharp);

        public virtual IReadOnlyList<string> Usings { get; } = ImmutableList<string>.Empty;

        public static IEnumerable<PartialTypeGenerator> FromTypes(IEnumerable<Type> types) =>
            types.SelectMany(FromType);

        public static IEnumerable<PartialTypeGenerator> FromType(Type type)
        {
            foreach (var gen in DictionaryAccessorGenerator.FromTypeProperties(type))
            {
                yield return gen;
            }

            foreach (var gen in SyntaxFactoryGenerator.FromTypeConstructors(type))
            {
                yield return gen;
            }
        }

        public static IEnumerable<GeneratedCode> Generate(IEnumerable<PartialTypeGenerator> generators)
        {
            var tasksByShortName = generators
                .GroupBy(_ => _.TypeShortName)
                .Select(_ => (fileName: _.Key, tasks: _.ToImmutableList()));

            foreach (var (typeShortName, fileTasks) in tasksByShortName)
            {
                var targetFilename = typeShortName + ".cs";
                var tasksByType = fileTasks.GroupBy(_ => _.TargetType)
                    .Select(_ => (targetType: _.Key, tasks: _.ToImmutableList()));

                var targetPath = CSharpFiles.SingleOrDefault(_ => Path.GetFileName(_) == targetFilename);
                if (targetPath == null)
                {
                    throw new InvalidOperationException($"A code-gen task could not find file: {targetFilename}");
                }


                var genDirPath = Path.GetDirectoryName(targetPath) ?? throw new NotImplementedException();
                var genFilename = Path.GetFileNameWithoutExtension(targetFilename) + ".Generated.cs";
                var genPath = Path.Combine(genDirPath, genFilename);
                var namespaceLine = File.ReadLines(targetPath).SingleOrDefault(_ => _.StartsWith("namespace"));
                if (namespaceLine == null)
                {
                    throw new InvalidOperationException($"Expected file to contain a namespace: {targetPath}");
                }

                var namespaceName = namespaceLine.Split(' ')[1];

                var csharp = CSharpStringBuilder.Create();

                foreach (var use in fileTasks.SelectMany(_ => _.Usings).Distinct())
                {
                    csharp.AppendLine($"using {use};");
                }

                csharp.AppendLine(@"
// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity
");


                csharp.Namespace(namespaceName, ns =>
                {
                    foreach (var (targetType, tasks) in tasksByType)
                    {
                        var typeType = targetType.IsInterface ? "interface" :
                            targetType.IsClass ? "class" :
                            throw new NotImplementedException($"unsupported target type: {targetType}");

                        var maybeStatic = targetType.IsClass && targetType.IsAbstract && targetType.IsSealed
                            ? "static"
                            : null;

                        var fullName = GetDisplayName(targetType);

                        var visibility = targetType.IsPublic ? "public" : "internal";
                        ns.Block($"{visibility} {maybeStatic} partial {typeType} {fullName} {{", "}", type =>
                        {
                            foreach (var task in tasks)
                            {
                                type.Region(task.GetType().Name, r => { task.Apply(r); });
                            }
                        });
                    }
                });

                yield return new GeneratedCode(genPath, csharp.ToString());
            }
        }
    }
}