// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class TypeSystemCodeGen
    {
        public const string TypeSystemNamespace = nameof(GraphZen) + "." + nameof(TypeSystem);

        public static void Generate()
        {
            GenSchemaExtensions();
            GenSchemaDefinitionExtensions();
        }

        public static List<(string kind, string type)> NamedTypes { get; } = typeof(NamedType).Assembly.GetTypes()
            .Where(typeof(NamedType).IsAssignableFrom)
            .Where(_ => !_.IsAbstract)
            .OrderBy(_ => _.Name)
            .Select(_ => (_.Name.Replace("Type", ""), _.Name))
            .ToList();


        public static void GenSchemaExtensions()
        {
            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(TypeSystemNamespace, ns =>
            {
                ns.StaticClass("SchemaTypeAccessorExtensions", schema =>
                {
                    foreach (var (kind, type) in NamedTypes)
                    {
                        schema.Region($"{kind} type accessors", region =>
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
                });
            });

            csharp.WriteToFile("TypeSystem", "SchemaExtensions");
        }

        public static void GenSchemaDefinitionExtensions()
        {
            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(TypeSystemNamespace, ns =>
            {
                ns.StaticClass("SchemaDefinitionExtensions", schema =>
                {
                    foreach (var (kind, type) in NamedTypes)
                    {
                        schema.Region($"{kind} type accessors", region =>
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
                });
            });

            csharp.WriteToFile("TypeSystem", "SchemaDefinitionExtensions");
        }
    }
}