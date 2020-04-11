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
            foreach (var (kind, type) in TypeSystemCodeGen.NamedTypes)
            {
                csharp.Region($"{kind} type accessors", region =>
                {
                    region.AppendLine($@"
    public static {type} Get{kind}(this Schema schema, string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<{type}>(name);


        public static {type} Get{kind}(this Schema schema, Type clrType) => Check.NotNull(schema, nameof(schema))
            .GetType<{type}>(Check.NotNull(clrType, nameof(clrType)));


        public static {type} Get{kind}<TClrType>(this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<{type}>(typeof(TClrType));

        public static {type}? Find{kind}(this Schema schema, string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<{type}>(name);

        public static {type}? Find{kind}<TClrType>(this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<{type}>(typeof(TClrType));


        public static {type}? Find{kind}(this Schema schema, Type clrType) => Check.NotNull(schema, nameof(schema))
            .FindType<{type}>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGet{kind}(this Schema schema, Type clrType, [NotNullWhen(true)] out {type}? type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGet{kind}<TClrType>(this Schema schema, [NotNullWhen(true)] out {type}? type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGet{kind}(this Schema schema, string name, [NotNullWhen(true)] out {type}? type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool Has{kind}(this Schema schema, Type clrType) => Check.NotNull(schema, nameof(schema))
            .HasType<{type}>(Check.NotNull(clrType, nameof(clrType)));

        public static bool Has{kind}<TClrType>(this Schema schema) =>
            Check.NotNull(schema, nameof(schema)).HasType<{type}>(typeof(TClrType));

        public static bool Has{kind}(this Schema schema, string name) => Check.NotNull(schema, nameof(schema))
            .HasType<{type}>(Check.NotNull(name, nameof(name)));

");
                });
            }
        }
    }

    public class SchemaDefinitionTypeAccessorGenerator : PartialTypeGenerator<SchemaDefinition>
    {
        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, type) in TypeSystemCodeGen.NamedTypes)
            {
                csharp.Region($"{kind} type accessors", region =>
                {
                    region.AppendLine($@"
     public static {type}Definition Get{kind}(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).GetType<{type}Definition>(name);


        public static {type}Definition Get{kind}(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .GetType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));


        public static {type}Definition Get{kind}<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).GetType<{type}Definition>(typeof(TClrType));

        public static {type}Definition? Find{kind}(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema)).FindType<{type}Definition>(name);

        public static {type}Definition? Find{kind}<TClrType>(this SchemaDefinition schema) =>
            Check.NotNull(schema, nameof(schema)).FindType<{type}Definition>(typeof(TClrType));


        public static {type}Definition? Find{kind}(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .FindType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool TryGet{kind}(this SchemaDefinition schema, Type clrType,
            out {type}Definition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public static bool TryGet{kind}<TClrType>(this SchemaDefinition schema,
            out {type}Definition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(typeof(TClrType), out type);

        public static bool TryGet{kind}(this SchemaDefinition schema, string name,
            out {type}Definition type) =>
            Check.NotNull(schema, nameof(schema)).TryGetType(Check.NotNull(name, nameof(name)), out type);

        public static bool Has{kind}(this SchemaDefinition schema, Type clrType) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));

        public static bool Has{kind}<TClrType>(this SchemaDefinition schema) => Check.NotNull(schema, nameof(schema))
            .HasType<{type}Definition>(typeof(TClrType));

        public static bool Has{kind}(this SchemaDefinition schema, string name) =>
            Check.NotNull(schema, nameof(schema))
                .HasType<{type}Definition>(Check.NotNull(name, nameof(name)));


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

        public static IEnumerable<PartialTypeGenerator> FromType(Type type)
        {
            foreach (var gen in DictionaryAccessorGenerator.FromType(type))
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