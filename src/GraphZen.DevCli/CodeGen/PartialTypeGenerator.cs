// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{

    public class SchemaTypeAccessorGenerator : PartialTypeGenerator<Schema>
    {
        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, type) in CodeGenFx.NamedTypes)
            {
                csharp.Region($"{kind} type accessors", region =>
                {
                    region.AppendLine($@"
        public {type} Get{kind}(string name) => GetType<{type}>(name);

        public {type} Get{kind}(Type clrType) => GetType<{type}>(Check.NotNull(clrType, nameof(clrType)));
        
        public {type} Get{kind}<TClrType>() => GetType<{type}>(typeof(TClrType));

        public {type}? Find{kind}(string name) => FindType<{type}>(name);

        public {type}? Find{kind}<TClrType>() => FindType<{type}>(typeof(TClrType));

        public {type}? Find{kind}(Type clrType) => FindType<{type}>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGet{kind}(Type clrType, [NotNullWhen(true)] out {type}? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGet{kind}<TClrType>([NotNullWhen(true)] out {type}? type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGet{kind}(string name, [NotNullWhen(true)] out {type}? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool Has{kind}(Type clrType) => HasType<{type}>(Check.NotNull(clrType, nameof(clrType)));

        public bool Has{kind}<TClrType>() => HasType<{type}>(typeof(TClrType));

        public bool Has{kind}(string name) => HasType<{type}>(Check.NotNull(name, nameof(name)));

");
                });
            }
        }
    }

    public class SchemaDefinitionTypeAccessorGenerator : PartialTypeGenerator<SchemaDefinition>
    {
        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, type) in CodeGenFx.NamedTypes)
            {
                csharp.Region($"{kind} type accessors", region =>
                {
                    region.AppendLine($@"
        public {type}Definition Get{kind}(string name) => GetType<{type}Definition>(name);

        public {type}Definition Get{kind}(Type clrType) =>
                GetType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));

        public {type}Definition Get{kind}<TClrType>() => GetType<{type}Definition>(typeof(TClrType));

        public {type}Definition? Find{kind}(string name) => FindType<{type}Definition>(name);

        public {type}Definition? Find{kind}<TClrType>() => FindType<{type}Definition>(typeof(TClrType));

        public {type}Definition? Find{kind}(Type clrType) => 
            FindType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGet{kind}(Type clrType, out {type}Definition type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGet{kind}<TClrType>(out {type}Definition type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGet{kind}(string name, out {type}Definition type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool Has{kind}(Type clrType) => HasType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));

        public bool Has{kind}<TClrType>(this SchemaDefinition schema) => HasType<{type}Definition>(typeof(TClrType));

        public bool Has{kind}(string name) => HasType<{type}Definition>(Check.NotNull(name, nameof(name)));
");
                });
            }
        }
    }


    public abstract class PartialTypeGenerator<T> : PartialTypeGenerator
    {
        protected PartialTypeGenerator() : base(typeof(T))
        {
        }
    }


    public abstract class PartialTypeGenerator
    {
        protected PartialTypeGenerator(Type targetType)
        {
            TargetType = targetType;
        }


        public Type TargetType { get; }

        public abstract void Apply(StringBuilder csharp);

        private static readonly Lazy<IReadOnlyList<string>> _csharpFiles =
            new Lazy<IReadOnlyList<string>>(() => Directory.GetFiles(".", "*.cs", SearchOption.AllDirectories));

        public static IReadOnlyList<string> CSharpFiles => _csharpFiles.Value;

        public static IEnumerable<PartialTypeGenerator> FromTypes(IEnumerable<Type> types) => types.SelectMany(FromType);

        public static IEnumerable<PartialTypeGenerator> FromType(Type type)
        {
            foreach (var gen in DictionaryAccessorGenerator.FromTypeProperties(type))
            {
                yield return gen;
            }
        }

        public static void Generate(IEnumerable<PartialTypeGenerator> generators)
        {
            var tasksByTarget = generators
                .GroupBy(_ => _.TargetType)
                .Select(_ => (targetType: _.Key, tasks: _.ToReadOnlyList()));

            foreach (var (targetType, tasks) in tasksByTarget)
            {
                var targetFilename = targetType.Name + ".cs";
                var targetPath = CSharpFiles.SingleOrDefault(_ => Path.GetFileName(_) == targetFilename);
                if (targetPath == null)
                    throw new InvalidOperationException($"A code-gen task could not find file: {targetFilename}");


                var genDirPath = Path.GetDirectoryName(targetPath) ?? throw new NotImplementedException();
                var genFilename = Path.GetFileNameWithoutExtension(targetFilename) + ".Generated.cs";
                var genPath = Path.Combine(genDirPath, genFilename);

                string? namespaceLine = File.ReadLines(targetPath).SingleOrDefault(_ => _.StartsWith("namespace"));
                if (namespaceLine == null)
                    throw new InvalidOperationException($"Expected file to contain a namespace: {targetPath}");
                var namespaceName = namespaceLine.Split(' ')[1];

                var csharp = CSharpStringBuilder.Create();
                csharp.Namespace(namespaceName, ns =>
                {
                    var typeType = targetType.IsInterface ? "interface" :
                        targetType.IsClass ? "class" :
                        throw new NotImplementedException($"unsupported target type: {targetType}");
                    ns.Block($"public partial {typeType} {targetType.Name} {{", "}", type =>
                    {
                        foreach (var task in tasks)
                        {
                            task.Apply(type);
                        }
                    });
                });

                Directory.CreateDirectory(genDirPath);
                csharp.WriteToFile(genPath);
            }
        }

        public static string GetTargetFileName(Type targetType) => $"{targetType.Name}.cs";

        public static string? GetTargetPath(Type targetType) =>
            CSharpFiles.SingleOrDefault(path => Path.GetFileName(path) == GetTargetFileName(targetType));
    }
}