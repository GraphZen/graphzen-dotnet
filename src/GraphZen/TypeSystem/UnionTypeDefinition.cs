// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UnionTypeDefinition : NamedTypeDefinition, IMutableUnionTypeDefinition
    {
        [NotNull] private readonly Dictionary<string, INamedTypeReference> _types =
            new Dictionary<string, INamedTypeReference>();

        public UnionTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource)
            : base(Check.NotNull(identity, nameof(identity)), Check.NotNull(schema, nameof(schema)),
                configurationSource)
        {
            Builder = new InternalUnionTypeBuilder(this, schema.Builder);
            identity.Definition = this;
        }

        private string DebuggerDisplay => $"union {Name}";

        [NotNull]
        public InternalUnionTypeBuilder Builder { get; }


        public IReadOnlyDictionary<string, INamedTypeReference> MemberTypes => _types;
        public TypeResolver<object, GraphQLContext> ResolveType { get; set; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Union;


        public override TypeKind Kind { get; } = TypeKind.Union;


        public void AddType(INamedTypeReference type)
        {
            Check.NotNull(type, nameof(type));
            if (type.Name == null)
            {
                throw new ArgumentException(
                    $"Cannot include {type} in {Name} union type definition unless a name is defined");
            }

            if (!_types.ContainsKey(type.Name))
            {
                _types[type.Name] = type;
            }
        }
    }
}