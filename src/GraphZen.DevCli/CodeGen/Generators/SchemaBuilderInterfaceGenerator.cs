// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.CodeGen.CodeGenFx;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using static GraphZen.TypeSystem.TypeKind;

namespace GraphZen.CodeGen.Generators
{
    public class SchemaBuilderInterfaceGenerator : PartialTypeGenerator
    {
        public SchemaBuilderInterfaceGenerator() : base(typeof(ISchemaBuilder<>))
        {
        }

        public static IReadOnlyDictionary<string, KindConfig> Kinds { get; } =
            new Dictionary<string, KindConfig>
            {
                {
                    nameof(Directive),
                    new KindConfig {TypeName = nameof(Directive), SimpleBuilder = true}
                },
                {
                    "Type", new KindConfig {TypeParamName = "ClrType", TypeName = nameof(NamedType)}
                },
                {
                    nameof(Object),
                    new KindConfig {TypeName = nameof(ObjectType), ContextBuilder = true}
                },
                {
                    nameof(Union),
                    new KindConfig {TypeName = nameof(UnionType), ContextBuilder = true}
                },
                {
                    nameof(Scalar),
                    new KindConfig {TypeName = nameof(ScalarType)}
                },
                {
                    nameof(Enum),
                    new KindConfig {TypeName = nameof(EnumType), SimpleBuilder = true, DefaultTypeName = "string"}
                },
                {
                    nameof(Interface),
                    new KindConfig {TypeName = nameof(InterfaceType), ContextBuilder = true}
                },
                {
                    nameof(InputObject),
                    new KindConfig {TypeName = nameof(InputObjectType), SimpleBuilder = true}
                }
            };


        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, config) in Kinds)
            {
                csharp.Region(kind + "s", region =>
                {
                    var typeParam = "T" + (config.TypeParamName ?? kind);

                    region.AppendLine($@"

        IEnumerable<I{config.TypeName}Definition> Get{kind}s(bool includeSpec{kind}s = false);
");

                    if (config.SimpleBuilder)
                    {
                        region.AppendLine($@"

       
        {config.TypeName}Builder<{config.DefaultTypeName}> {kind}(string name);


        {config.TypeName}Builder<{typeParam}> {kind}<{typeParam}>() where {typeParam} : notnull;
        {config.TypeName}Builder<{typeParam}> {kind}<{typeParam}>(string name) where {typeParam} : notnull;


        {config.TypeName}Builder<{config.DefaultTypeName}> {kind}(Type clrType); 
        {config.TypeName}Builder<{config.DefaultTypeName}> {kind}(Type clrType, string name); 
");
                    }
                    else if (config.ContextBuilder)
                    {
                        region.AppendLine($@"

       
        {config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(string name);


        {config.TypeName}Builder<{typeParam}, TContext> {kind}<{typeParam}>() where {typeParam} : notnull;
        {config.TypeName}Builder<{typeParam}, TContext> {kind}<{typeParam}>(string name) where {typeParam} : notnull;


        {config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(Type clrType); 
        {config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(Type clrType, string name); 


   


");
                    }


                    region.AppendLine($@"


        SchemaBuilder<TContext> Unignore{kind}<{typeParam}>() where {typeParam}: notnull;

         SchemaBuilder<TContext> Unignore{kind}(Type clrType);

         SchemaBuilder<TContext> Unignore{kind}(string name);


         SchemaBuilder<TContext> Ignore{kind}<{typeParam}>() where {typeParam}: notnull;

         SchemaBuilder<TContext> Ignore{kind}(Type clrType);

         SchemaBuilder<TContext> Ignore{kind}(string name);

        SchemaBuilder<TContext> Remove{kind}<{typeParam}>() where {typeParam}: notnull;

         SchemaBuilder<TContext> Remove{kind}(Type clrType);

         SchemaBuilder<TContext> Remove{kind}(string name);


");
                });
            }
        }


        public class KindConfig
        {
            public string? TypeName { get; set; }
            public bool SimpleBuilder { get; set; }
            public string DefaultTypeName { get; set; } = "object";
            public bool ContextBuilder { get; set; }
            public string? TypeParamName { get; set; }
        }
    }
}