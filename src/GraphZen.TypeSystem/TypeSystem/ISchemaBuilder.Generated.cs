#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming

namespace GraphZen.TypeSystem {
public  partial interface ISchemaBuilder<TContext> {
#region Object type accessors



         ISchemaBuilder<TContext> IgnoreObject(Type clrType);
         ISchemaBuilder<TContext> IgnoreObject(string name);
        IObjectTypeBuilder<object, TContext> Object(Type clrType);


        IObjectTypeBuilder<object, TContext> Object(string name);


        IObjectTypeBuilder<TObject, TContext> Object<TObject>();

#endregion
}
}
