// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InterfaceTypeDefinition : FieldsContainerDefinition, IMutableInterfaceTypeDefinition
    {
        public InterfaceTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(
            Check.NotNull(identity, nameof(identity)), Check.NotNull(schema, nameof(schema)), configurationSource)
        {
            Builder = new InternalInterfaceTypeBuilder(this, schema.Builder);
            if (identity.ClrType != null && !identity.ClrType.IsInterface)
            {
                throw new InvalidOperationException($"Cannot create GraphQL interface '{identity.Name}' from CLR type. '{identity.ClrType}' is not an interface type.");
            }
            identity.Definition = this;

        }

        private string DebuggerDisplay => $"interface {Name}";

        public override bool SetClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (clrType != null && !clrType.IsInterface)
            {
                throw new InvalidOperationException($"Cannot set CLR type for GraphQL interface '{Name}'. '{clrType}' is not an interface type.");
            }
            return base.SetClrType(clrType, configurationSource);
        }


        
        public InternalInterfaceTypeBuilder Builder { get; }

        public TypeResolver<object, GraphQLContext> ResolveType { get; set; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Interface;

        public override TypeKind Kind { get; } = TypeKind.Interface;
    }
}