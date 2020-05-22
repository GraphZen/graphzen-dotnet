// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public abstract class NamedTypeDefinition : AnnotatableMemberDefinition, IMutableNamedTypeDefinition
    {
        protected NamedTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(configurationSource)
        {
            Identity = identity;
            Schema = schema;
            IsIntrospection = SpecReservedNames.IntrospectionTypeNames.Contains(Name);
        }


        protected override SchemaDefinition Schema { get; }

        public TypeIdentity Identity { get; }
        private string DebuggerDisplay => ClrType != null ? $"{Kind}: {Name} ({ClrType.Name})" : $"{Kind}: {Name}";

        public abstract TypeKind Kind { get; }
        public bool IsIntrospection { get; }
        public string Name => Identity.Name;

        public bool SetName(string name, ConfigurationSource configurationSource) =>
            Identity.SetName(name, configurationSource);

        public ConfigurationSource GetNameConfigurationSource() => Identity.GetNameConfigurationSource();
        public Type? ClrType => Identity.ClrType;

        public bool SetClrType(Type clrType, string name, ConfigurationSource configurationSource) =>
            Identity.SetClrType(clrType, name, configurationSource);

        public virtual bool SetClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
            => Identity.SetClrType(clrType, inferName, configurationSource);

        public bool RemoveClrType(ConfigurationSource configurationSource) =>
            Identity.RemoveClrType(configurationSource);

        public ConfigurationSource? GetClrTypeConfigurationSource() => Identity.GetClrTypeConfigurationSource();
        public override string ToString() => $"{Kind.ToDisplayStringLower()} {Name}";
    }
}