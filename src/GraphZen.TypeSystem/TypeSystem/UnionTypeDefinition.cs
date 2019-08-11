﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UnionTypeDefinition : NamedTypeDefinition, IMutableUnionTypeDefinition
    {
        [NotNull]
        private readonly Dictionary<string, ObjectTypeDefinition> _types =
            new Dictionary<string, ObjectTypeDefinition>();


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


        public IReadOnlyDictionary<string, ObjectTypeDefinition> MemberTypes => _types;
        public IEnumerable<ObjectTypeDefinition> GetMemberTypes() => MemberTypes.Values;

        public ConfigurationSource? FindIgnoredMemberTypeConfigurationSource(string name) => throw new NotImplementedException();

        public TypeResolver<object, GraphQLContext> ResolveType { get; set; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Union;


        public override TypeKind Kind { get; } = TypeKind.Union;


        public void AddType(ObjectTypeDefinition type)
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

        IEnumerable<IObjectTypeDefinition> IMemberTypesContainerDefinition.GetMemberTypes() => GetMemberTypes();
    }
}