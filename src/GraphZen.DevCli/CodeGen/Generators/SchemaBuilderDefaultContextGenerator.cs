// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
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
        public override IReadOnlyList<string> Usings { get; } =
            ImmutableList.Create("GraphZen.TypeSystem.Internal");

        public override void Apply(StringBuilder csharp)
        {

            foreach (var (kind, config) in SchemaBuilderInterfaceGenerator.Kinds.Take(3))
            {
                csharp.Region(kind + "s", region =>
                {
                    var typeParam = "T" + (config.TypeParamName ?? kind);

                    if (config.SimpleBuilder)
                    {
                        region.AppendLine($@"

public I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(string name) {{
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.{kind}(name, ConfigurationSource.Explicit);
    var builder = new {kind}Builder<{config.DefaultTypeName}>(internalBuilder);
    return builder;
}} 


public  I{config.TypeName}Builder<{typeParam}> {kind}<{typeParam}>() where {typeParam} : notnull {{
    var internalBuilder = Builder.{kind}(typeof({typeParam}), ConfigurationSource.Explicit);
    var builder = new {kind}Builder<{typeParam}>(internalBuilder);
    return builder;
}}

public  I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(Type clrType)  {{
            Check.NotNull(clrType, nameof(clrType));
    var internalBuilder = Builder.{kind}(clrType, ConfigurationSource.Explicit);
    var builder = new {kind}Builder<{config.DefaultTypeName}>(internalBuilder);
    return builder;
}}



");
                    }
                    else if (config.ContextBuilder)
                    {
                        region.AppendLine($@"

       
     //   I{config.TypeName}Builder<{config.DefaultTypeName}, GraphQLContext> {kind}(string name);


      //  I{config.TypeName}Builder<{typeParam}, GraphQLContext> {kind}<{typeParam}>() where {typeParam} : notnull;


     //   I{config.TypeName}Builder<{config.DefaultTypeName}, GraphQLContext> {kind}(Type clrType); 


   


");
                    }


                    region.AppendLine($@"


public ISchemaBuilder<GraphQLContext> Unignore{kind}<{typeParam}>() where {typeParam}: notnull {{
    Builder.Unignore{kind}(typeof({typeParam}), ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<GraphQLContext> Unignore{kind}(Type clrType) {{
    Check.NotNull(clrType, nameof(clrType));
    Builder.Unignore{kind}(clrType, ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<GraphQLContext> Unignore{kind}(string name) {{
    Check.NotNull(name, nameof(name));
    Builder.Unignore{kind}(name, ConfigurationSource.Explicit);
    return this;
}}


public ISchemaBuilder<GraphQLContext> Ignore{kind}<{typeParam}>() where {typeParam}: notnull {{
    Builder.Ignore{kind}(typeof({typeParam}), ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<GraphQLContext> Ignore{kind}(Type clrType) {{
    Check.NotNull(clrType, nameof(clrType));
    Builder.Ignore{kind}(clrType, ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<GraphQLContext> Ignore{kind}(string name) {{
    Check.NotNull(name, nameof(name));
    Builder.Ignore{kind}(name, ConfigurationSource.Explicit);
    return this;
}}
   

   ");
                });
            }
        }
    }
}