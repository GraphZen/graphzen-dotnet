// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public partial class DirectiveDefinition : MemberDefinition, IMutableDirectiveDefinition,
        IInfrastructure<InternalDirectiveBuilder>
    {
        private readonly Dictionary<string, ArgumentDefinition> _arguments =
            new Dictionary<string, ArgumentDefinition>();

        private readonly ConcurrentDictionary<DirectiveLocation, ConfigurationSource> _ignoredLocations =
            new ConcurrentDictionary<DirectiveLocation, ConfigurationSource>();

        private readonly ConcurrentDictionary<DirectiveLocation, ConfigurationSource> _locations =
            new ConcurrentDictionary<DirectiveLocation, ConfigurationSource>();

        private ConfigurationSource? _clrTypeConfigurationSource;

        private ConfigurationSource _nameConfigurationSource;


        public DirectiveDefinition(string? name, Type? clrType, SchemaDefinition schema,
            ConfigurationSource configurationSource) :
            base(configurationSource)
        {
            Schema = schema;
            if (clrType != null)
            {
                ClrType = clrType;
                _clrTypeConfigurationSource = configurationSource;
            }

            if (name != null)
            {
                name = name.IsValidGraphQLName()
                    ? name
                    : throw new InvalidNameException(TypeSystemExceptionMessages.InvalidNameException
                        .CannotCreateDirectiveWithInvalidName(name));
                _nameConfigurationSource = ConfigurationSource.Explicit;
            }
            else if (clrType != null)
            {
                if (clrType.TryGetGraphQLNameFromDataAnnotation(out var n))
                {
                    if (!n.IsValidGraphQLName())
                    {
                        throw new InvalidNameException(TypeSystemExceptionMessages.InvalidNameException
                            .CannotCreateDirectiveFromClrTypeWithInvalidNameAttribute(clrType, n));
                    }


                    name = n;
                    _nameConfigurationSource = ConfigurationSource.DataAnnotation;
                }
                else
                {
                    name = clrType.GetGraphQLName();
                    _nameConfigurationSource = ConfigurationSource.Convention;
                }
            }

            Name = Check.NotNull(name, nameof(name));
            Builder = new InternalDirectiveBuilder(this, schema.Builder);
            IsSpecDirective = SpecReservedNames.DirectiveNames.Contains(Name);
        }

        protected override SchemaDefinition Schema { get; }


        internal InternalDirectiveBuilder Builder { get; }

        private string DebuggerDisplay => $"directive {Name}";

        InternalDirectiveBuilder IInfrastructure<InternalDirectiveBuilder>.Instance => Builder;

        public string Name { get; private set; }

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptionMessages.InvalidNameException.CannotRename(name, this));
            }

            if (!configurationSource.Overrides(GetNameConfigurationSource()))
            {
                return false;
            }

            if (Name != name)
            {
                Builder.Schema.RenameDirective(this, name, configurationSource);
            }

            Name = name;
            _nameConfigurationSource = configurationSource;
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;

        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetNameConfigurationSource()))
            {
                return false;
            }

            if (TryGetArgument(name, out var existing) && existing != argument)
            {
                throw new DuplicateNameException(
                    TypeSystemExceptionMessages.DuplicateNameException.CannotRenameArgument(argument, name));
            }

            _arguments.Remove(argument.Name);
            _arguments[name] = argument;
            return true;
        }

        public IEnumerable<IArgumentDefinition> GetArguments() => _arguments.Values;

        [GenDictionaryAccessors(nameof(ArgumentDefinition.Name), "Argument")]
        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _arguments;

        IEnumerable<ArgumentDefinition> IMutableArgumentsDefinition.GetArguments() => Arguments.Values;

        public bool AddLocation(DirectiveLocation location, ConfigurationSource configurationSource)
        {
            var locationConfigurationSource = FindDirectiveLocationConfigurationSource(location);
            if (configurationSource.Overrides(locationConfigurationSource))
            {
                _locations.AddOrUpdate(location, configurationSource, (dl, cs) => configurationSource);
                return true;
            }

            return false;
        }

        public bool IgnoreLocation(DirectiveLocation location, ConfigurationSource configurationSource)
        {
            var existingConfigurationSource = FindDirectiveLocationConfigurationSource(location);
            if (configurationSource.Overrides(existingConfigurationSource))
            {
                var ignoredConfigurationSource = FindIgnoredDirectiveLocationConfigurationSource(location);
                var newIgnoredConfigurationSource = configurationSource.Max(ignoredConfigurationSource);
                _ignoredLocations.AddOrUpdate(location, newIgnoredConfigurationSource,
                    (dl, cs) => newIgnoredConfigurationSource);
                return true;
            }

            return false;
        }

        public bool UnignoreLocation(DirectiveLocation location, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredDirectiveLocationConfigurationSource(location);
            if (ignoredConfigurationSource.HasValue && configurationSource.Overrides(ignoredConfigurationSource))
            {
                _ignoredLocations.Remove(location, out _);
                return true;
            }

            return false;
        }


        public ConfigurationSource? FindDirectiveLocationConfigurationSource(DirectiveLocation directiveLocation) =>
            _locations.TryGetValue(directiveLocation, out var cs) ? cs : (ConfigurationSource?)null;

        public ConfigurationSource?
            FindIgnoredDirectiveLocationConfigurationSource(DirectiveLocation directiveLocation) =>
            _ignoredLocations.TryGetValue(directiveLocation, out var cs) ? cs : (ConfigurationSource?)null;

        public IReadOnlyCollection<DirectiveLocation> Locations =>
            (IReadOnlyCollection<DirectiveLocation>)_locations.Keys;

        public Type? ClrType { get; private set; }

        public bool SetClrType(Type clrType, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetClrTypeConfigurationSource()))
            {
                return false;
            }

            if (Schema.TryGetDirective(clrType, out var existing) && !existing.Equals(this))
            {
                throw new DuplicateClrTypeException(
                    TypeSystemExceptionMessages.DuplicateClrTypeException.CannotChangeClrType(this, clrType, existing));
            }


            ClrType = clrType;
            _clrTypeConfigurationSource = configurationSource;
            return true;
        }

        public bool RemoveClrType(ConfigurationSource configurationSource) => throw new NotImplementedException();


        public ConfigurationSource? GetClrTypeConfigurationSource() => _clrTypeConfigurationSource;
        public bool IsSpecDirective { get; }
        public ArgumentDefinition GetOrAddArgument(string name, ConfigurationSource configurationSource)
        {
            if (!_arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var argument))
            {
                argument = new ArgumentDefinition(name, ConfigurationSource.Convention, Builder.Schema,
                    configurationSource, this, null);
                _arguments[name] = argument;
            }

            return argument;
        }

        public override string ToString() => $"directive {Name}";
    }
}