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
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        protected NamedTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(configurationSource)
        {
            Identity = identity;
            Schema = schema;

            if (this.IsInputType() && !this.IsOutputType() && identity.IsOutputType() == true)
            {
                throw new InvalidTypeException(
                    $"Cannot create {Kind.ToDisplayStringLower()} {identity.Name}: {Kind.ToDisplayString()} types are input types and an object or interface field already references a type named '{identity.Name}'. GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
            }

            if (!this.IsInputType() && this.IsOutputType() && identity.IsInputType() == true)
            {
                throw new InvalidTypeException(
                    $"Cannot create {Kind.ToDisplayStringLower()} {identity.Name}: {Kind.ToDisplayString()} types are output types and an input field or argument already references a type named '{identity.Name}'. GraphQL input type references are reserved for scalar, enum, or input object types.");
            }
        }


        protected override SchemaDefinition Schema { get; }

        public TypeIdentity Identity { get; }
        private string DebuggerDisplay => ClrType != null ? $"{Kind}: {Name} ({ClrType.Name})" : $"{Kind}: {Name}";

        public abstract TypeKind Kind { get; }
        public bool IsIntrospection => SpecReservedNames.IntrospectionTypeNames.Contains(Name);

        public bool IsSpec =>
            IsIntrospection || Kind == TypeKind.Scalar && SpecReservedNames.ScalarTypeNames.Contains(Name);

        public string Name => Identity.Name;

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetNameConfigurationSource()))
            {
                return false;
            }

            if (Schema.TryGetTypeIdentity(name, out var existing) && !existing.Equals(Identity))
            {
                if (this.IsInputType() && !this.IsOutputType() && existing.IsOutputType() == true)
                {
                    throw new InvalidTypeException(
                        $"Cannot rename {this} to \"{name}\": {Kind.ToDisplayString()} types are input types and an object or interface field already references a type named \"{name}\". GraphQL output type references are reserved for scalar, enum, interface, object, or union types.");
                }

                if (!this.IsInputType() && this.IsOutputType() && existing.IsInputType() == true)
                {
                    throw new InvalidTypeException(
                        $"Cannot rename {this} to \"{name}\": {Kind.ToDisplayString()} types are output types and an input field or argument already references a type named \"{name}\". GraphQL input type references are reserved for scalar, enum, or input object types.");
                }
            }


            if (Identity.SetName(name, configurationSource))
            {
            }

            return false;
        }

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