// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [DisplayName("union")]
    public class MutableUnionType : MutableNamedTypeDefinition, IMutableUnionType
    {
        private readonly List<MutableObjectType> _types = new List<MutableObjectType>();

        public MutableUnionType(TypeIdentity identity, MutableSchema schema,
            ConfigurationSource configurationSource)
            : base(identity, schema, configurationSource)
        {
            InternalBuilder = new InternalUnionTypeBuilder(this);
            Builder = new UnionTypeBuilder(InternalBuilder);
        }

        public override IEnumerable<IMember> Children()
        {
            yield break;
        }

        private string DebuggerDisplay => $"union {Name}";


        internal new InternalUnionTypeBuilder InternalBuilder { get; }
        public new UnionTypeBuilder Builder { get; }
        protected override INamedTypeDefinitionBuilder GetBuilder() => Builder;

        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;


        public IEnumerable<MutableObjectType> GetMemberTypes() => _types;

        public ConfigurationSource? FindIgnoredMemberTypeConfigurationSource(string name) =>
            throw new NotImplementedException();

        public TypeResolver<object, GraphQLContext>? ResolveType { get; set; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Union;


        public override TypeKind Kind { get; } = TypeKind.Union;

        IEnumerable<IObjectType> IMemberTypesDefinition.GetMemberTypes() => GetMemberTypes();


        public void AddType(MutableObjectType type)
        {
            Check.NotNull(type, nameof(type));
            if (type.Name == null)
            {
                throw new ArgumentException(
                    $"Cannot include {type} in {Name} union type definition unless a name is defined");
            }

            if (!_types.Contains(type))
            {
                _types.Add(type);
            }
        }
    }
}