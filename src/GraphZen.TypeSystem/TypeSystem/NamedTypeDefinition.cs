// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Internal;
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
        protected override SchemaDefinition Schema { get; }

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


        public bool SetClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetClrTypeConfigurationSource()))
            {
                return false;
            }

            if (Schema.TryGetType(clrType, out var existingTyped) && !existingTyped.Equals(this))
            {
                throw new DuplicateClrTypeException(
                    TypeSystemExceptionMessages.DuplicateClrTypeException.CannotChangeClrType(this, clrType,
                        existingTyped));
            }

            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException($"Cannot set CLR type on {this} with custom name: the custom name \"{name}\" is not a valid GraphQL name.");
            }

            if (Schema.TryGetType(name, out var existingNamed) && !existingNamed.Equals(this))
            {
                throw new DuplicateNameException(
                            $"Cannot set CLR type on {this} with custom name: the custom name \"{name}\" conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named '{existingNamed.Name}'. All type names must be unique.");
            }

            SetName(name, configurationSource);
            return SetClrType(clrType, false, configurationSource);

        }

        public virtual bool SetClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_clrTypeConfigurationSource))
            {
                return false;
            }

            if (Schema.TryGetType(clrType, out var existingTyped) && !existingTyped.Equals(this))
            {
                throw new DuplicateClrTypeException(
                    TypeSystemExceptionMessages.DuplicateClrTypeException.CannotChangeClrType(this, clrType,
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

                    if (Schema.TryGetType(annotated, out var existingNamed) && !existingNamed.Equals(this))
                    {
                        throw new DuplicateNameException(
                            $"Cannot set CLR type on {this} and infer name: the annotated name \"{annotated}\" on CLR {clrType.GetClrTypeKind()} '{clrType.Name}' conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named {existingNamed.Name}. All GraphQL type names must be unique.");
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

                    if (Schema.TryGetType(clrType.Name, out var existingNamed) && !existingNamed.Equals(this))
                    {
                        throw new DuplicateNameException(
                            $"Cannot set CLR type on {this} and infer name: the CLR {clrType.GetClrTypeKind()} name '{clrType.Name}' conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named {existingNamed.Name}. All GraphQL type names must be unique.");
                    }

                    SetName(clrType.Name, configurationSource);
                }
            }

            _clrTypeConfigurationSource = configurationSource;
            Identity.ClrType = clrType;
            return true;
        }

        public bool RemoveClrType(ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_clrTypeConfigurationSource))
            {
                return false;
            }

            _clrTypeConfigurationSource = configurationSource;
            Identity.ClrType = null;
            return true;
        }

        public ConfigurationSource? GetClrTypeConfigurationSource() => _clrTypeConfigurationSource;


        public override string ToString() => $"{Kind.ToDisplayStringLower()} {Name}";
    }
}