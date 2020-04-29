#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using GraphZen.TypeSystem.Internal;

// ReSharper disable InconsistentNaming

namespace GraphZen.TypeSystem {
public  partial class SchemaBuilder {
#region Directives


public IDirectiveBuilder<object> Directive(string name) {
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.Directive(name, ConfigurationSource.Explicit);
    var builder = new DirectiveBuilder<object>(internalBuilder);
    return builder;
} 


public  IDirectiveBuilder<TDirective> Directive<TDirective>() where TDirective : notnull {
    var internalBuilder = Builder.Directive(typeof(TDirective), ConfigurationSource.Explicit);
    var builder = new DirectiveBuilder<TDirective>(internalBuilder);
    return builder;
}

public  IDirectiveBuilder<object> Directive(Type clrType)  {
            Check.NotNull(clrType, nameof(clrType));
    var internalBuilder = Builder.Directive(clrType, ConfigurationSource.Explicit);
    var builder = new DirectiveBuilder<object>(internalBuilder);
    return builder;
}







public ISchemaBuilder<GraphQLContext> UnignoreDirective<TDirective>() where TDirective: notnull {
    Builder.UnignoreDirective(typeof(TDirective), ConfigurationSource.Explicit);
    return this;
}

public ISchemaBuilder<GraphQLContext> UnignoreDirective(Type clrType) {
    Check.NotNull(clrType, nameof(clrType));
    Builder.UnignoreDirective(clrType, ConfigurationSource.Explicit);
    return this;
}

public ISchemaBuilder<GraphQLContext> UnignoreDirective(string name) {
    Check.NotNull(name, nameof(name));
    Builder.UnignoreDirective(name, ConfigurationSource.Explicit);
    return this;
}

      //   ISchemaBuilder<GraphQLContext> UnignoreDirective(Type clrType);

      //   ISchemaBuilder<GraphQLContext> UnignoreDirective(string name);


      //   ISchemaBuilder<GraphQLContext> IgnoreDirective<TDirective>() where TDirective: notnull;

      //   ISchemaBuilder<GraphQLContext> IgnoreDirective(Type clrType);

      //   ISchemaBuilder<GraphQLContext> IgnoreDirective(string name);


#endregion
}
public  partial class SchemaBuilder<TContext> {
// hello GraphZen.TypeSystem.SchemaBuilder`1[TContext] 
}
}
