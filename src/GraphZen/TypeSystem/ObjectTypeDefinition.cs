// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ObjectTypeDefinition : FieldsContainerDefinition, IMutableObjectTypeDefinition
    {
        [NotNull] private readonly Dictionary<string, ConfigurationSource> _ignoredInterfaces =
            new Dictionary<string, ConfigurationSource>();

        [NotNull]
        private readonly Dictionary<string, (INamedTypeReference interfaceRef, ConfigurationSource configurationSource)>
            _interfaces =
                new Dictionary<string, (INamedTypeReference interfaceRef, ConfigurationSource configurationSource)>();


        public ObjectTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(
            Check.NotNull(identity, nameof(identity)),
            Check.NotNull(schema, nameof(schema)), configurationSource)
        {
            Builder = new InternalObjectTypeBuilder(this, schema.Builder);
            identity.Definition = this;
        }

        private string DebuggerDisplay => $"type {Name}";

        [NotNull]
        public InternalObjectTypeBuilder Builder { get; }

        public IsTypeOf<object, GraphQLContext> IsTypeOf { get; set; }
        public IEnumerable<INamedTypeReference> Interfaces => _interfaces.Values.Select(_ => _.interfaceRef);

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Object;

        public override TypeKind Kind { get; } = TypeKind.Object;

        public ConfigurationSource? FindIgnoredInterfaceConfigurationSource([NotNull] string interfaceName) =>
            _ignoredInterfaces.TryGetValue(interfaceName, out var cs) ? cs : (ConfigurationSource?) null;


        public bool AddInterface([NotNull] INamedTypeReference interfaceRef, ConfigurationSource configurationSource)
        {
            Check.NotNull(interfaceRef, nameof(interfaceRef));

            if (interfaceRef.Name == null)
            {
                throw new ArgumentException("Interface must have a name", nameof(interfaceRef));
            }

            var interfaceName = interfaceRef.Name;
            var ignoredInterfaceConfigurationSource = FindIgnoredInterfaceConfigurationSource(interfaceName);
            if (ignoredInterfaceConfigurationSource.HasValue)
            {
                if (ignoredInterfaceConfigurationSource.Overrides(configurationSource))
                {
                    return false;
                }

                _ignoredInterfaces.Remove(interfaceName);
            }


            if (_interfaces.TryGetValue(interfaceName, out var existing) &&
                existing.configurationSource.Overrides(configurationSource))
            {
                return true;
            }

            _interfaces[interfaceName] = (interfaceRef, configurationSource);
            return true;
        }


        public bool IgnoreInterface(string interfaceName, ConfigurationSource configurationSource)
        {
            Check.NotNull(interfaceName, nameof(interfaceName));
            var ignoredConfigurationSource = FindIgnoredInterfaceConfigurationSource(interfaceName);
            if (!configurationSource.Overrides(ignoredConfigurationSource))
            {
                return false;
            }

            if (_ignoredInterfaces.TryGetValue(interfaceName, out var existingIgnoredConfigurationSource))
            {
                configurationSource = configurationSource.Max(existingIgnoredConfigurationSource);
                _ignoredInterfaces[interfaceName] = configurationSource;
            }
            else
            {
                _ignoredInterfaces[interfaceName] = configurationSource;
            }

            return RemoveInterface(interfaceName, configurationSource);
        }

        private bool RemoveInterface([NotNull] string interfaceName, ConfigurationSource configurationSource)
        {
            if (_interfaces.TryGetValue(interfaceName, out var existing) &&
                !configurationSource.Overrides(existing.configurationSource))
            {
                return false;
            }

            _interfaces.Remove(interfaceName);
            return true;
        }
    }
}