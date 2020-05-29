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

        // ReSharper disable once CollectionNeverQueried.Local
        private readonly Dictionary<string, ConfigurationSource> _ignoredArguments =
            new Dictionary<string, ConfigurationSource>();

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
                    name = clrType.GetGraphQLNameAnnotation();
                    _nameConfigurationSource = ConfigurationSource.Convention;
                }
            }

            Name = Check.NotNull(name, nameof(name));
            Builder = new InternalDirectiveBuilder(this);
            IsSpec = SpecReservedNames.DirectiveNames.Contains(Name);
        }

        public override SchemaDefinition Schema { get; }


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

        public bool RemoveArgument(ArgumentDefinition argument)
        {
            if (_arguments.Remove(argument.Name, out var removed))
            {
                if (!removed.Equals(argument))
                {
                    throw new Exception("Did not remove expected argument");
                }

                return true;
            }

            return false;
        }

        public bool AddArgument(ArgumentDefinition argument)
        {
            _arguments[argument.Name] = argument;
            return true;
        }

        public ConfigurationSource? FindIgnoredArgumentConfigurationSource(string name) =>
            throw new NotImplementedException();


        [GenDictionaryAccessors(nameof(ArgumentDefinition.Name), "Argument")]
        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _arguments;

        public IEnumerable<ArgumentDefinition> GetArguments() => Arguments.Values;

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

        public bool SetClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetClrTypeConfigurationSource()))
            {
                return false;
            }

            if (Schema.TryGetDirective(clrType, out var existingTyped) && !existingTyped.Equals(this))
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotChangeClrType(this, clrType,
                        existingTyped));
            }

            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    $"Cannot set CLR type on {this} with custom name: the custom name \"{name}\" is not a valid GraphQL name.");
            }

            if (Schema.TryGetDirective(name, out var existingNamed) && !existingNamed.Equals(this))
            {
                throw new DuplicateItemException(
                    $"Cannot set CLR type on {this} with custom name: the custom name \"{name}\" conflicts with an existing directive named {existingNamed.Name}. All directive names must be unique.");
            }

            SetName(name, configurationSource);
            return SetClrType(clrType, false, configurationSource);
        }

        public bool SetClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetClrTypeConfigurationSource()))
            {
                return false;
            }

            if (Schema.TryGetDirective(clrType, out var existingTyped) && !existingTyped.Equals(this))
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotChangeClrType(this, clrType,
                        existingTyped));
            }

            if (inferName)
            {
                if (clrType.TryGetGraphQLNameFromDataAnnotation(out var annotated))
                {
                    if (!annotated.IsValidGraphQLName())
                    {
                        throw new InvalidNameException(
                            $"Cannot set CLR type on {this} and infer name: the annotated name \"{annotated}\" on CLR {clrType.GetClrTypeKind()} '{clrType.Name}' is not a valid GraphQL name.");
                    }

                    if (Schema.TryGetDirective(annotated, out var existingNamed) && !existingNamed.Equals(this))
                    {
                        throw new DuplicateItemException(
                            $"Cannot set CLR type on {this} and infer name: the annotated name \"{annotated}\" on CLR {clrType.GetClrTypeKind()} '{clrType.Name}' conflicts with an existing directive named {existingNamed.Name}. All directive names must be unique.");
                    }

                    SetName(annotated, configurationSource);
                }
                else
                {
                    if (!clrType.Name.IsValidGraphQLName())
                    {
                        throw new InvalidNameException(
                            $"Cannot set CLR type on {this} and infer name: the CLR {clrType.GetClrTypeKind()} name '{clrType.Name}' is not a valid GraphQL name.");
                    }

                    if (Schema.TryGetDirective(clrType.Name, out var existingNamed) && !existingNamed.Equals(this))
                    {
                        throw new DuplicateItemException(
                            $"Cannot set CLR type on {this} and infer name: the CLR {clrType.GetClrTypeKind()} name '{clrType.Name}' conflicts with an existing directive named {existingNamed.Name}. All directive names must be unique.");
                    }

                    SetName(clrType.Name, configurationSource);
                }
            }

            ClrType = clrType;
            _clrTypeConfigurationSource = configurationSource;
            return true;
        }

        public bool RemoveClrType(ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetClrTypeConfigurationSource()))
            {
                return false;
            }

            ClrType = null;
            _clrTypeConfigurationSource = configurationSource;
            return true;
        }


        public ConfigurationSource? GetClrTypeConfigurationSource() => _clrTypeConfigurationSource;
        public bool IsSpec { get; }
        IEnumerable<IArgumentDefinition> IArgumentsDefinition.GetArguments() => GetArguments();

        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetNameConfigurationSource()))
            {
                return false;
            }

            if (TryGetArgument(name, out var existing) && existing != argument)
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotRenameArgument(argument, name));
            }

            if (RemoveArgument(argument))
            {
                _arguments[name] = argument;
            }

            return true;
        }


        public ArgumentDefinition? AddArgument(string name, Type clrType, ConfigurationSource configurationSource)
        {
            if (!clrType.TryGetGraphQLTypeInfo(out var typeSyntax, out var innerClrType))
            {
                return null;
            }

            var typeIdentity = Schema.GetOrAddInputTypeIdentity(innerClrType);
            var argument = new ArgumentDefinition(name, configurationSource, typeIdentity, typeSyntax, Schema,
                configurationSource, this, null);
            AddArgument(argument);
            return argument;
        }

        public ArgumentDefinition AddArgument(string name, string type, ConfigurationSource configurationSource)
        {
            var typeSyntax = Schema.Builder.Parser.ParseType(type);
            var typeName = typeSyntax.GetNamedType().Name.Value;
            var typeIdentity = Schema.GetOrAddTypeIdentity(typeName);
            var argument = new ArgumentDefinition(name, configurationSource, typeIdentity, typeSyntax, Schema,
                configurationSource, this, null);
            AddArgument(argument);
            return argument;
        }

        public override string ToString() => ClrType != null && ClrType.Name != Name
            ? $"directive {Name} (CLR {ClrType.GetClrTypeKind()}: {ClrType.Name})"
            : $"directive {Name}";


        public bool IgnoreArgument(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredArgumentConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue &&
                ignoredConfigurationSource.Overrides(configurationSource))
            {
                return true;
            }

            if (ignoredConfigurationSource != null)
            {
                configurationSource = configurationSource.Max(ignoredConfigurationSource);
            }

            _ignoredArguments[name] = configurationSource;
            var existing = FindArgument(name);

            if (existing != null)
            {
                return IgnoreArgument(existing, configurationSource);
            }

            return true;
        }

        private bool IgnoreArgument(ArgumentDefinition argument, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(argument.GetConfigurationSource()))
            {
                _arguments.Remove(argument.Name);
                return true;
            }

            return false;
        }

        public void UnignoreArgument(string name)
        {
            _ignoredArguments.Remove(name);
        }
    }
}