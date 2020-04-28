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
            new Lazy<IReadOnlyList<string>>(() => Directory.GetFiles(".", "*.cs", SearchOption.AllDirectories));

        protected PartialTypeGenerator(Type targetType)
        {
            TargetType = targetType;
        }

        public Type TargetType { get; }

        public static IReadOnlyList<string> CSharpFiles => CSharpFilesLazy.Value;

        public abstract void Apply(StringBuilder csharp);

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
            var tasksByTarget = generators
                .GroupBy(_ => _.TargetType)
                .Select(_ => (targetType: _.Key, tasks: _.ToImmutableList()));

            foreach (var (targetType, tasks) in tasksByTarget)
            {
                var targetFilename = targetType.Name + ".cs";
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
                csharp.AppendLine(@"
// ReSharper disable InconsistentNaming
");
                csharp.Namespace(namespaceName, ns =>
                {
                    var typeType = targetType.IsInterface ? "interface" :
                        targetType.IsClass ? "class" :
                        throw new NotImplementedException($"unsupported target type: {targetType}");

                    var maybeStatic = targetType.IsClass && targetType.IsAbstract && targetType.IsSealed
                        ? "static"
                        : null;

                    ns.Block($"public {maybeStatic} partial {typeType} {targetType.Name} {{", "}", type =>
                    {
                        foreach (var task in tasks)
                        {
                            task.Apply(type);
                        }
                    });
                });

                yield return new GeneratedCode(genPath, csharp.ToString());
            }
        }
    }
}