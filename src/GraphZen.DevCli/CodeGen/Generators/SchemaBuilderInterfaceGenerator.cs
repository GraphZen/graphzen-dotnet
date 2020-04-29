// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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
        public SchemaBuilderInterfaceGenerator() : base(typeof(ISchemaBuilder<>))
        {
        }

        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, type) in TypeSystemCodeGen.NamedTypes
                .Where(_ => _.kind == "Object")
                .Where(_ => _.kind != "Scalar" && _.kind != "Enum"))
            {
                csharp.Region($"{kind} type accessors", region =>
                {
                    region.AppendLine($@"


         ISchemaBuilder<TContext> Ignore{kind}(Type clrType);
         ISchemaBuilder<TContext> Ignore{kind}(string name);
        I{type}Builder<object, TContext> {kind}(Type clrType);


        I{type}Builder<object, TContext> {kind}(string name);


        I{type}Builder<TObject, TContext> {kind}<T{kind}>();
");
                });
            }
        }
    }
}