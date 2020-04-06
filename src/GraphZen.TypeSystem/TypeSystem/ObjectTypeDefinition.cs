// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class ObjectTypeDefinition : FieldsDefinition, IMutableObjectTypeDefinition
    {
        private readonly Dictionary<string, ConfigurationSource> _ignoredInterfaces =
            new Dictionary<string, ConfigurationSource>();


        private readonly List<InterfaceTypeDefinition> _interfaces = new List<InterfaceTypeDefinition>();


        public ObjectTypeDefinition(TypeIdentity identity, SchemaDefinition schema,
            ConfigurationSource configurationSource) : base(
            Check.NotNull(identity, nameof(identity)),
            Check.NotNull(schema, nameof(schema)), configurationSource)
        {
            Builder = new InternalObjectTypeBuilder(this, schema.Builder);
            identity.Definition = this;
        }

        private string DebuggerDisplay => $"type {Name}";


        public InternalObjectTypeBuilder Builder { get; }

        public IsTypeOf<object, GraphQLContext>? IsTypeOf { get; set; }

        public IEnumerable<InterfaceTypeDefinition> GetInterfaces() => _interfaces;

        public ConfigurationSource? FindIgnoredInterfaceConfigurationSource(string name)
        {
            Check.NotNull(name, nameof(name));
            return _ignoredInterfaces.TryGetValue(name, out var cs) ? cs : (ConfigurationSource?)null;
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Object;

        public override TypeKind Kind { get; } = TypeKind.Object;


        public bool AddInterface(InterfaceTypeDefinition @interface, ConfigurationSource configurationSource)
        {
            Check.NotNull(@interface, nameof(@interface));

            if (@interface.Name == null) throw new ArgumentException("Interface must have a name", nameof(@interface));

            var interfaceName = @interface.Name;
            var ignoredInterfaceConfigurationSource = FindIgnoredInterfaceConfigurationSource(interfaceName);
            if (ignoredInterfaceConfigurationSource.HasValue)
            {
                if (!configurationSource.Overrides(ignoredInterfaceConfigurationSource)) return false;

                UnignoreInterface(interfaceName);
            }


            if (_interfaces.Contains(@interface)) return true;

            _interfaces.Add(@interface);
            return true;
        }

        public void UnignoreInterface(string name)
        {
            _ignoredInterfaces.Remove(name);
        }


        public bool IgnoreInterface(string interfaceName, ConfigurationSource configurationSource)
        {
            Check.NotNull(interfaceName, nameof(interfaceName));
            var ignoredConfigurationSource = FindIgnoredInterfaceConfigurationSource(interfaceName);
            if (!configurationSource.Overrides(ignoredConfigurationSource)) return false;

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

        private bool RemoveInterface(string interfaceName, ConfigurationSource configurationSource)
        {
            var existing = _interfaces.SingleOrDefault(_ => _.Name == interfaceName);
            if (existing != null)
            {
                if (!configurationSource.Overrides(existing.GetConfigurationSource())) return false;
                _interfaces.Remove(existing);
                return true;
            }

            return false;
        }


        IEnumerable<IInterfaceTypeDefinition> IInterfacesDefinition.GetInterfaces() => GetInterfaces();
    }
}