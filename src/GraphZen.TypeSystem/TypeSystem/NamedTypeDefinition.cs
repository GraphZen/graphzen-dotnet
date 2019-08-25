// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class NamedTypeDefinition : AnnotatableMemberDefinition, IMutableGraphQLTypeDefinition
    {
        private ConfigurationSource _nameConfigurationSource;
        private ConfigurationSource _clrTypeConfiguraitonSource = ConfigurationSource.Convention;

        protected NamedTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(configurationSource)
        {
            Identity = identity;
            Schema = schema;
            if (identity.ClrType != null)
            {
                if (identity.ClrType.TryGetGraphQLNameFromDataAnnotation(out var customName) &&
                    customName == identity.Name)
                    _nameConfigurationSource = ConfigurationSource.DataAnnotation;
                else
                    _nameConfigurationSource = ConfigurationSource.Convention;
            }
            else
            {
                _nameConfigurationSource = ConfigurationSource.Explicit;
            }
        }


        public TypeIdentity Identity { get; }


        public SchemaDefinition Schema { get; }


        private string DebuggerDisplay => ClrType != null ? $"{Kind}: {Name} ({ClrType.Name})" : $"{Kind}: {Name}";

        public abstract TypeKind Kind { get; }

        public string Name => Identity.Name;

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_nameConfigurationSource)) return false;

            _nameConfigurationSource = configurationSource;
            // ReSharper disable once AssignNullToNotNullAttribute
            Identity.Name = name;
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource()
        {
            return _nameConfigurationSource;
        }

        public Type ClrType => Identity.ClrType;

        public virtual bool SetClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_clrTypeConfiguraitonSource)) return false;
            _clrTypeConfiguraitonSource = configurationSource;
            Identity.ClrType = clrType;
            return true;
        }

        public ConfigurationSource GetClrTypeConfigurationSource()
        {
            return _clrTypeConfiguraitonSource;
        }


        public TypeReference GetTypeReference()
        {
            return new TypeReference(Identity,
                ClrType != null ? SyntaxFactory.NamedType(ClrType) : SyntaxFactory.NamedType(SyntaxFactory.Name(Name)));
        }

        public override string ToString()
        {
            return $"{Kind} {Name}";
        }
    }
}