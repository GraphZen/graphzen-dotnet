// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        private IReadOnlyList<string> _ignorableKinds;

        public SchemaBuilderInterfaceGenerator() : base(typeof(ISchemaBuilder<>))
        {
            _ignorableKinds = TypeSystemCodeGen.NamedTypes.Select(_ => _.kind).Append("Directive").Append("Type")
                .ToImmutableList();
        }

        private bool IsInputKind(string kind) => new[] { "Enum", "Scalar", "InputObject" }.Contains(kind);
        private bool IsOutputKind(string kind) => kind != "InputObject";


        public override void Apply(StringBuilder csharp)
        {
            var kinds = new Dictionary<string, KindConfig>
            {
                {
                    nameof(Directive),

                    new KindConfig {TypeName = nameof(Directive), SimpleBuilder = true }
                },
                {
                    "Type", new KindConfig {TypeParamName = "ClrType"}
                },
                {
                    nameof(TypeKind.Object),
                    new KindConfig {TypeName = nameof(ObjectType), ContextBuilder = false}
                },
                {
                    nameof(Union),
                    new KindConfig {TypeName = nameof(UnionType), ContextBuilder = false}
                },
                {
                    nameof(Scalar),
                    new KindConfig {TypeName = nameof(ScalarType), SimpleBuilder = false, ContextBuilder = false}
                },
                {
                    nameof(TypeKind.Enum),
                    new KindConfig {TypeName = nameof(EnumType), SimpleBuilder = true, DefaultTypeName = "string"}
                },
                {
                    nameof(Interface),
                    new KindConfig {TypeName = nameof(InterfaceType), SimpleBuilder = false, ContextBuilder = false}
                },
                {
                    nameof(InputObject),
                    new KindConfig {TypeName = nameof(InputObjectType), SimpleBuilder = false, ContextBuilder = false}
                }
            };

            foreach (var (kind, config) in kinds)
            {
                csharp.Region(kind, region =>
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


            foreach (var (kind, type) in TypeSystemCodeGen.NamedTypes
                .Where(_ => _.kind != "Scalar" && _.kind != "Enum"))
            {
                csharp.Region($"{kind} type accessors", region =>
                {
                    if (!IsInputKind(kind))
                    {
                        region.AppendLine($@"


         
        I{type}Builder<object, TContext> {kind}(Type clrType);


        I{type}Builder<object, TContext> {kind}(string name);


        I{type}Builder<T{kind}, TContext> {kind}<T{kind}>() where T{kind} : notnull;
");
                    }
                });
            }
        }


        private class KindConfig
        {
            public string? TypeName { get; set; }
            public bool SimpleBuilder { get; set; }
            public string DefaultTypeName { get; set; } = "object";
            public bool ContextBuilder { get; set; }
            public string? TypeParamName { get; set; }
        }
    }
}