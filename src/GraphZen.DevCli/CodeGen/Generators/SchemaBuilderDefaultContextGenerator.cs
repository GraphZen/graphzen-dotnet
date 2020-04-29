// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GraphZen.CodeGen.CodeGenFx;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.CodeGen.Generators;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public class SchemaBuilderDefaultContextGenerator : PartialTypeGenerator<SchemaBuilder>
    {
        public override void Apply(StringBuilder csharp)
        {
            csharp.AppendLine($"// hello {TargetType} ");

            foreach (var (kind, config) in SchemaBuilderInterfaceGenerator.Kinds.Take(1))
            {
                csharp.Region(kind + "s", region =>
                {
                    var typeParam = "T" + (config.TypeParamName ?? kind);

                    if (config.SimpleBuilder)
                    {
                        region.AppendLine($@"

       
      /*
public I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(string name) {{
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.{kind}(name, ConfigurationSource.Explicit)
    var builder = new {kind}Builder<{config.DefaultTypeName}>(internalBuilder);
    return builder;
}} 
*/


      //  I{config.TypeName}Builder<{typeParam}> {kind}<{typeParam}>() where {typeParam} : notnull;


     //   I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(Type clrType); 


");
                    }
                    else if (config.ContextBuilder)
                    {
                        region.AppendLine($@"

       
     //   I{config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(string name);


      //  I{config.TypeName}Builder<{typeParam}, TContext> {kind}<{typeParam}>() where {typeParam} : notnull;


     //   I{config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(Type clrType); 


   


");
                    }


                    region.AppendLine($@"


      //  ISchemaBuilder<TContext> Unignore{kind}<{typeParam}>() where {typeParam}: notnull;

      //   ISchemaBuilder<TContext> Unignore{kind}(Type clrType);

      //   ISchemaBuilder<TContext> Unignore{kind}(string name);


      //   ISchemaBuilder<TContext> Ignore{kind}<{typeParam}>() where {typeParam}: notnull;

      //   ISchemaBuilder<TContext> Ignore{kind}(Type clrType);

      //   ISchemaBuilder<TContext> Ignore{kind}(string name);

");
                });
            }
        }
    }
}