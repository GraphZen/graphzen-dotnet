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
// hello GraphZen.TypeSystem.SchemaBuilder 
#region Directives


public IDirectiveBuilder<object> Directive(string name) {
    Check.NotNull(name, nameof(name));
    var internalBuilder = Builder.Directive(name, ConfigurationSource.Explicit);
    var builder = new DirectiveBuilder<object>(internalBuilder);
    return builder;
} 


      //  IDirectiveBuilder<TDirective> Directive<TDirective>() where TDirective : notnull;


     //   IDirectiveBuilder<object> Directive(Type clrType); 






      //  ISchemaBuilder<TContext> UnignoreDirective<TDirective>() where TDirective: notnull;

      //   ISchemaBuilder<TContext> UnignoreDirective(Type clrType);

      //   ISchemaBuilder<TContext> UnignoreDirective(string name);


      //   ISchemaBuilder<TContext> IgnoreDirective<TDirective>() where TDirective: notnull;

      //   ISchemaBuilder<TContext> IgnoreDirective(Type clrType);

      //   ISchemaBuilder<TContext> IgnoreDirective(string name);


#endregion
}
public  partial class SchemaBuilder<TContext> {
// hello GraphZen.TypeSystem.SchemaBuilder`1[TContext] 
}
}
