// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.ComponentModel;
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
    [DisplayName("interface")]
    public class InterfaceTypeDefinition : FieldsDefinition, IMutableInterfaceTypeDefinition
    {
        public InterfaceTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(
            identity, schema, configurationSource)
        {
            Builder = new InternalInterfaceTypeBuilder(this);
            if (identity.ClrType != null && !identity.ClrType.IsInterface)
            {
                throw new InvalidOperationException(
                    $"Cannot create GraphQL interface '{identity.Name}' from CLR type. '{identity.ClrType}' is not an interface type.");
            }
        }

        private string DebuggerDisplay => $"interface {Name}";


        internal new InternalInterfaceTypeBuilder Builder { get; }
        protected override MemberDefinitionBuilder GetBuilder() => Builder;

        public override bool SetClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            if (!clrType.IsInterface)
            {
                throw new InvalidOperationException(
                    $"Cannot set CLR type for GraphQL interface '{Name}'. '{clrType}' is not an interface type.");
            }

            return base.SetClrType(clrType, inferName, configurationSource);
        }

        public TypeResolver<object, GraphQLContext>? ResolveType { get; set; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Interface;

        public override TypeKind Kind { get; } = TypeKind.Interface;
    }
}