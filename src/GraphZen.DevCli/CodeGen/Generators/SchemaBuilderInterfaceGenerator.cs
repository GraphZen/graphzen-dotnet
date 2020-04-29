// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.CodeGen.CodeGenFx;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
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
                    "Type", new KindConfig {TypeParamName = "ClrType"}
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
                    new KindConfig {TypeName = nameof(ScalarType) }
                },
                {
                    nameof(Enum),
                    new KindConfig {TypeName = nameof(EnumType), SimpleBuilder = true, DefaultTypeName = "string"}
                },
                {
                    nameof(Interface),
                    new KindConfig {TypeName = nameof(InterfaceType),  ContextBuilder = true}
                },
                {
                    nameof(InputObject),
                    new KindConfig {TypeName = nameof(InputObjectType), SimpleBuilder = false }
                }
            };


        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, config) in Kinds)
            {
                csharp.Region(kind + "s", region =>
                {
                    var typeParam = "T" + (config.TypeParamName ?? kind);

                    if (config.SimpleBuilder)
                    {
                        region.AppendLine($@"

       
        I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(string name);


        I{config.TypeName}Builder<{typeParam}> {kind}<{typeParam}>() where {typeParam} : notnull;


        I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(Type clrType); 


");
                    }
                    else if (config.ContextBuilder)
                    {
                        region.AppendLine($@"

       
        I{config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(string name);


        I{config.TypeName}Builder<{typeParam}, TContext> {kind}<{typeParam}>() where {typeParam} : notnull;


        I{config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(Type clrType); 


   


");
                    }


                    region.AppendLine($@"


        ISchemaBuilder<TContext> Unignore{kind}<{typeParam}>() where {typeParam}: notnull;

         ISchemaBuilder<TContext> Unignore{kind}(Type clrType);

         ISchemaBuilder<TContext> Unignore{kind}(string name);


         ISchemaBuilder<TContext> Ignore{kind}<{typeParam}>() where {typeParam}: notnull;

         ISchemaBuilder<TContext> Ignore{kind}(Type clrType);

         ISchemaBuilder<TContext> Ignore{kind}(string name);

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