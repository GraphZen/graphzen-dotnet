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




        public override void Apply(StringBuilder csharp)
        {

            foreach (var kind in _ignorableKinds)
            {
                csharp.AppendLine($@"


        ISchemaBuilder<TContext> Unignore{kind}<T{kind}>() where T{kind}: notnull;

         ISchemaBuilder<TContext> Unignore{kind}(Type clrType);

         ISchemaBuilder<TContext> Unignore{kind}(string name);


         ISchemaBuilder<TContext> Ignore{kind}<T{kind}>() where T{kind}: notnull;

         ISchemaBuilder<TContext> Ignore{kind}(Type clrType);

         ISchemaBuilder<TContext> Ignore{kind}(string name);

");

            }

            foreach (var (kind, type) in TypeSystemCodeGen.NamedTypes
                .Where(_ => _.kind != "Scalar" && _.kind != "Enum"))
            {
                csharp.Region($"{kind} type accessors", region =>
                {

                    if (IsInputKind(kind))
                    {

                    }
                    else
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
    }
}