// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public abstract class NamedTypeDefinition : AnnotatableMemberDefinition, IMutableNamedTypeDefinition
    {
        private ConfigurationSource _nameConfigurationSource;
        private ConfigurationSource? _clrTypeConfigurationSource;

        protected NamedTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(configurationSource)
        {
            Identity = identity;
            Schema = schema;
            var clrType = identity.ClrType;
            if (clrType != null)
            {

                if (clrType.TryGetGraphQLNameFromDataAnnotation(out var customName) &&
                    customName == identity.Name)
                {
                    _nameConfigurationSource = ConfigurationSource.DataAnnotation;
                }
                else
                {
                    _nameConfigurationSource = ConfigurationSource.Convention;
                }
            }
            else
            {
                _nameConfigurationSource = ConfigurationSource.Explicit;
            }

            IsIntrospection = SpecReservedNames.IntrospectionTypeNames.Contains(Name);
        }

        public TypeIdentity Identity { get; }

        public SchemaDefinition Schema { get; }

        private string DebuggerDisplay => ClrType != null ? $"{Kind}: {Name} ({ClrType.Name})" : $"{Kind}: {Name}";

        public abstract TypeKind Kind { get; }
        public bool IsIntrospection { get; }

        public string Name => Identity.Name;

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptionMessages.InvalidNameException.CannotRename(name, this));
            }

            if (!configurationSource.Overrides(_nameConfigurationSource))
            {
                return false;
            }

            _nameConfigurationSource = configurationSource;
            Identity.Name = name;
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;

        public Type? ClrType => Identity.ClrType;

        public virtual bool SetClrType(Type? clrType, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_clrTypeConfigurationSource))
            {
                return false;
            }

            _clrTypeConfigurationSource = configurationSource;
            Identity.ClrType = clrType;
            return true;
        }

        public bool RemoveClrType(ConfigurationSource configurationSource) => throw new NotImplementedException();

        public ConfigurationSource? GetClrTypeConfigurationSource() => _clrTypeConfigurationSource;


        public override string ToString() => $"{Kind.ToDisplayStringLower()} {Name}";
    }
}