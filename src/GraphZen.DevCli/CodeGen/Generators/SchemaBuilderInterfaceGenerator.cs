// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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


        private class KindConfig
        {
            public bool SimpleBuilder { get; set; }
            public bool ContextBuilder { get; set; }
            public string? TypeParamName { get; set; }
        }


        public override void Apply(StringBuilder csharp)
        {
            var kinds = new Dictionary<string, KindConfig>()
            {
                {nameof(Directive), new KindConfig {SimpleBuilder = true, ContextBuilder = false}},
                {"Type", new KindConfig {SimpleBuilder = false, ContextBuilder = false, TypeParamName = "ClrType"}},
                {nameof(Object), new KindConfig {SimpleBuilder = false, ContextBuilder = false}},
                {nameof(Union), new KindConfig {SimpleBuilder = false, ContextBuilder = false}},
                {nameof(Scalar), new KindConfig {SimpleBuilder = false, ContextBuilder = false}},
                {nameof(Enum), new KindConfig {SimpleBuilder = false, ContextBuilder = false}},
                {nameof(Interface), new KindConfig {SimpleBuilder = false, ContextBuilder = false}},
                {nameof(InputObject), new KindConfig {SimpleBuilder = false, ContextBuilder = false}},
            };

            foreach (var (kind, config) in kinds)
            {
                var typeParam = "T" + config.TypeParamName ?? kind;
                csharp.AppendLine($@"


        ISchemaBuilder<TContext> Unignore{kind}<{typeParam}>() where {typeParam}: notnull;

         ISchemaBuilder<TContext> Unignore{kind}(Type clrType);

         ISchemaBuilder<TContext> Unignore{kind}(string name);


         ISchemaBuilder<TContext> Ignore{kind}<{typeParam}>() where {typeParam}: notnull;

         ISchemaBuilder<TContext> Ignore{kind}(Type clrType);

         ISchemaBuilder<TContext> Ignore{kind}(string name);

");
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
                    else
                    {
                    }
                });
            }
        }
    }
}