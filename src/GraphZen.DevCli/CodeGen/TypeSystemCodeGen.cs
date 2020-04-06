// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
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
            GenerateTypeSystemDictionaryAccessors();
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


        public static void GenerateTypeSystemDictionaryAccessors()
        {
            var csharp = new StringBuilder();

            csharp.Append(@"
using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

#nullable restore

namespace GraphZen.TypeSystem {
");


            var fieldDefinitionAccessors = new List<(string containerType, string valueType)>
            {
                ("InterfaceTypeDefinition", "FieldDefinition"),
                ("ObjectTypeDefinition", "FieldDefinition"),
                ("InputObjectTypeDefinition", "InputFieldDefinition"),
                ("FieldsDefinition", "FieldDefinition")
            };

            foreach (var (containerType, valueType) in fieldDefinitionAccessors)
            {
                csharp.AppendDictionaryAccessor(
                    containerType, "Fields", "name", "string", "Field", valueType);
            }

            var fieldAccessors = new List<(string containerType, string valueType)>
            {
                ("InterfaceType", "Field"),
                ("ObjectType", "Field"),
                ("InputObjectType", "InputField")
            };

            foreach (var (containerType, valueType) in fieldAccessors)
            {
                csharp.AppendDictionaryAccessor(containerType, "Fields", "name", "string", "Field", valueType);
            }

            csharp.AppendDictionaryAccessor("EnumTypeDefinition", "Values", "name", "string", "Value",
                "EnumValueDefinition");
            csharp.AppendDictionaryAccessor("EnumType", "Values", "name", "string", "Value", "EnumValue");
            csharp.AppendDictionaryAccessor("EnumType", "ValuesByValue", "value", "object", "Value", "EnumValue");


            var argumentDefinitionAccessors = new List<(string containerType, string valueType)>
            {
                ("FieldDefinition", "ArgumentDefinition"),
                ("DirectiveDefinition", "ArgumentDefinition")
            };

            foreach (var (containerType, valueType) in argumentDefinitionAccessors)
            {
                csharp.AppendDictionaryAccessor(
                    containerType, "Arguments", "name", "string", "Argument", valueType);
            }

            var argumentAccessors = new List<(string containerType, string valueType)>
            {
                ("Field", "Argument"),
                ("IArguments", "Argument")
            };

            foreach (var (containerType, valueType) in argumentAccessors)
            {
                csharp.AppendDictionaryAccessor(
                    containerType, "Arguments", "name", "string", "Argument", valueType);
            }

            csharp.Append("}");
            CodeGenHelpers.WriteFile("./src/Linked/TypeSystem/TypeSystemAccessors.Generated.cs", csharp.ToString());
        }
    }
}