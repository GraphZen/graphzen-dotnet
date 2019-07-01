// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Types.Builders.Internal;
using GraphZen.Types.Internal;
using JetBrains.Annotations;

namespace GraphZen.Types.Builders
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InterfaceTypeDefinition : FieldsContainerDefinition, IMutableInterfaceTypeDefinition
    {
        public InterfaceTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(
            Check.NotNull(identity, nameof(identity)), Check.NotNull(schema, nameof(schema)), configurationSource)
        {
            Builder = new InternalInterfaceTypeBuilder(this, schema.Builder);
            identity.Definition = this;
        }

        private string DebuggerDisplay => $"interface {Name}";

        [NotNull]
        public InternalInterfaceTypeBuilder Builder { get; }

        public TypeResolver<object, GraphQLContext> ResolveType { get; set; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Interface;

        public override TypeKind Kind { get; } = TypeKind.Interface;
    }
}