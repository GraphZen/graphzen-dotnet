// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.Types.Internal;
using JetBrains.Annotations;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Types.Builders
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class NamedTypeDefinition : AnnotatableMemberDefinition, IMutableGraphQLTypeDefinition
    {
        private ConfigurationSource _nameConfigurationSource;

        protected NamedTypeDefinition([NotNull] TypeIdentity identity, [NotNull] SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(configurationSource)
        {
            Identity = identity;
            Schema = schema;
            if (identity.ClrType != null)
            {
                if (identity.ClrType.TryGetGraphQLNameFromDataAnnotation(out var customName) &&
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
        }

        [NotNull]
        public TypeIdentity Identity { get; }

        [NotNull]
        public SchemaDefinition Schema { get; }


        private string DebuggerDisplay => ClrType != null ? $"{Kind}: {Name} ({ClrType.Name})" : $"{Kind}: {Name}";

        public abstract TypeKind Kind { get; }

        public string Name => Identity.Name;

        public bool SetName([CanBeNull] string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_nameConfigurationSource))
            {
                return false;
            }

            _nameConfigurationSource = configurationSource;
            // ReSharper disable once AssignNullToNotNullAttribute
            Identity.Name = name;
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;

        public Type ClrType => Identity.ClrType;

        public bool SetClrType(Type clrType, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        [NotNull]
        public TypeReference GetTypeReference() =>
            new TypeReference(Identity, ClrType != null ? NamedType(ClrType) : NamedType(Name(Name)));

        public override string ToString() => $"{Kind} {Name}";
    }
}