// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.CodeGen.CodeGenFx;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public class SchemaBuilderGenerator : PartialTypeGenerator
    {
        public SchemaBuilderGenerator() : base(typeof(SchemaBuilder<>))
        {
        }

        public override IReadOnlyList<string> Usings { get; } =
            ImmutableList.Create("GraphZen.TypeSystem.Internal");

        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, config) in SchemaBuilderInterfaceGenerator.Kinds)
            {
                csharp.Region(kind + "s", region =>
                {
                    var typeParam = "T" + (config.TypeParamName ?? kind);

                    if (config.SimpleBuilder)
                    {
                        region.AppendLine($@"

public I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(string name) {{
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.{kind}(name, ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{config.DefaultTypeName}>(internalBuilder);
    return builder;
}} 


public  I{config.TypeName}Builder<{typeParam}> {kind}<{typeParam}>() where {typeParam} : notnull {{
    var internalBuilder = Builder.{kind}(typeof({typeParam}), ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{typeParam}>(internalBuilder);
    return builder;
}}

public  I{config.TypeName}Builder<{typeParam}> {kind}<{typeParam}>(string name) where {typeParam} : notnull {{
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.{kind}(typeof({typeParam}), name, ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{typeParam}>(internalBuilder);
    return builder;
}}


public  I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(Type clrType)  {{
            Check.NotNull(clrType, nameof(clrType));
    var internalBuilder = Builder.{kind}(clrType, ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{config.DefaultTypeName}>(internalBuilder);
    return builder;
}}

public  I{config.TypeName}Builder<{config.DefaultTypeName}> {kind}(Type clrType, string name)  {{
            Check.NotNull(clrType, nameof(clrType));
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.{kind}(clrType, name, ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{config.DefaultTypeName}>(internalBuilder);
    return builder;
}}




");
                    }
                    else if (config.ContextBuilder)
                    {
                        region.AppendLine($@"
public I{config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(string name) {{
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.{kind}(name, ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{config.DefaultTypeName}, TContext>(internalBuilder);
    return builder;
}} 

public  I{config.TypeName}Builder<{typeParam}, TContext> {kind}<{typeParam}>() where {typeParam} : notnull {{
    var internalBuilder = Builder.{kind}(typeof({typeParam}), ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{typeParam}, TContext>(internalBuilder);
    return builder;
}}

public  I{config.TypeName}Builder<{typeParam}, TContext> {kind}<{typeParam}>(string name) where {typeParam} : notnull {{
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.{kind}(typeof({typeParam}), name, ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{typeParam}, TContext>(internalBuilder);
    return builder;
}}

public  I{config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(Type clrType)  {{
    Check.NotNull(clrType, nameof(clrType));
    var internalBuilder = Builder.{kind}(clrType, ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{config.DefaultTypeName}, TContext>(internalBuilder);
    return builder;
}}

public  I{config.TypeName}Builder<{config.DefaultTypeName}, TContext> {kind}(Type clrType, string name)  {{
    Check.NotNull(clrType, nameof(clrType));
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.{kind}(clrType, name, ConfigurationSource.Explicit)!;
    var builder = new {config.TypeName}Builder<{config.DefaultTypeName}, TContext>(internalBuilder);
    return builder;
}}



   


");
                    }


                    region.AppendLine($@"


public ISchemaBuilder<TContext> Unignore{kind}<{typeParam}>() where {typeParam}: notnull {{
    Builder.Unignore{kind}(typeof({typeParam}), ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<TContext> Unignore{kind}(Type clrType) {{
    Check.NotNull(clrType, nameof(clrType));
    Builder.Unignore{kind}(clrType, ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<TContext> Unignore{kind}(string name) {{
    Check.NotNull(name, nameof(name));
    Builder.Unignore{kind}(name, ConfigurationSource.Explicit);
    return this;
}}


public ISchemaBuilder<TContext> Ignore{kind}<{typeParam}>() where {typeParam}: notnull {{
    Builder.Ignore{kind}(typeof({typeParam}), ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<TContext> Ignore{kind}(Type clrType) {{
    Check.NotNull(clrType, nameof(clrType));
    Builder.Ignore{kind}(clrType, ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<TContext> Ignore{kind}(string name) {{
    Check.NotNull(name, nameof(name));
    Builder.Ignore{kind}(name, ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<TContext> Remove{kind}<{typeParam}>() where {typeParam}: notnull {{
    Builder.Remove{kind}(typeof({typeParam}), ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<TContext> Remove{kind}(Type clrType) {{
    Check.NotNull(clrType, nameof(clrType));
    Builder.Remove{kind}(clrType, ConfigurationSource.Explicit);
    return this;
}}

public ISchemaBuilder<TContext> Remove{kind}(string name) {{
    Check.NotNull(name, nameof(name));
    Builder.Remove{kind}(name, ConfigurationSource.Explicit);
    return this;
}}

   

   ");
                });
            }
        }
    }
}