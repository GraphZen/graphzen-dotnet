// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class InterfaceTypeDefinition : FieldsDefinition, IMutableInterfaceTypeDefinition
    {
        public InterfaceTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(
            Check.NotNull(identity, nameof(identity)), Check.NotNull(schema, nameof(schema)), configurationSource)
        {
            Builder = new InternalInterfaceTypeBuilder(this, schema.Builder);
            if (identity.ClrType != null && !identity.ClrType.IsInterface)
            {
                throw new InvalidOperationException(
                    $"Cannot create GraphQL interface '{identity.Name}' from CLR type. '{identity.ClrType}' is not an interface type.");
            }

            identity.Definition = this;
        }

        private string DebuggerDisplay => $"interface {Name}";

        public override bool SetClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (!clrType.IsInterface)
            {
                throw new InvalidOperationException(
                    $"Cannot set CLR type for GraphQL interface '{Name}'. '{clrType}' is not an interface type.");
            }

            return base.SetClrType(clrType, configurationSource);
        }


        public InternalInterfaceTypeBuilder Builder { get; }

        public TypeResolver<object, GraphQLContext>? ResolveType { get; set; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Interface;

        public override TypeKind Kind { get; } = TypeKind.Interface;
    }
}