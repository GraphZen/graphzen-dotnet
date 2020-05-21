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
    [DebuggerDisplay(nameof(DebuggerDisplay))]
    public class TypeIdentity : IMutableNamed, IMutableClrType, IMutableDefinition
    {
        private readonly TypeKind? _kind;
        private ConfigurationSource? _clrTypeConfigurationSource;

        private bool? _isInputType;
        private bool? _isOutputType;
        private ConfigurationSource _nameConfigurationSource = ConfigurationSource.Explicit;


        private INamedTypeDefinition? _typeDefinition;


        public TypeIdentity(string name, SchemaDefinition schema, TypeKind? kind = null)
        {
            Name = name.IsValidGraphQLName() ? name : throw new InvalidNameException($"Cannot create Type Identity: \"{name}\" is not a valid GraphQL name.");
            Schema = schema;
            _kind = kind;
        }

        public TypeIdentity(Type clrType, SchemaDefinition schema, TypeKind? kind = null)
        {
            ClrType = clrType.GetEffectiveClrType();
            _clrTypeConfigurationSource = ConfigurationSource.Convention;
            Schema = schema;
            _kind = kind;
            if (ClrType.TryGetGraphQLNameFromDataAnnotation(out var annotated))
            {
                _nameConfigurationSource = ConfigurationSource.DataAnnotation;
                Name = annotated;
            }
            else
            {
                Name = ClrType.Name;
                _nameConfigurationSource = ConfigurationSource.Convention;
            }
        }

        private SchemaDefinition Schema { get; }


        public INamedTypeDefinition? Definition
        {
            get => _typeDefinition;
            set
            {
                if (_typeDefinition != null)
                {
                    throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(Definition)} with value {value}, it's value has already been set with {_typeDefinition}.");
                }

                _typeDefinition =
                    value ?? throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(Definition)} to null.");
            }
        }

        public TypeKind? Kind => _typeDefinition?.Kind ?? _kind;

        public bool? IsInputType
        {
            get => Kind?.IsInputType() ?? _isInputType;
            set
            {
                if (_kind.HasValue)
                {
                    throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(IsInputType)}, because the identity's type kind ({Kind}) is already set.");
                }

                _isInputType = value;
            }
        }

        public bool? IsOutputType
        {
            get => Kind?.IsOutputType() ?? _isOutputType;
            set
            {
                if (_kind.HasValue)
                {
                    throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(IsOutputType)}, because the type identity's kind ({Kind}) is already set.");
                }

                _isOutputType = value;
            }
        }

        internal string DebuggerDisplay =>
            $"Identity:{(name: Name, clrType: ClrType, Kind, IsInputType, IsOutputType)}";


        public Type? ClrType { get; private set; }

        public bool SetClrType(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetClrTypeConfigurationSource()))
            {
                return false;
            }

            if (Schema.TryGetType(clrType, out var existingTyped) && !existingTyped.Equals(Definition))
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotChangeClrType(this, clrType,
                        existingTyped));
            }

            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    $"Cannot set CLR type on {Definition} with custom name: the custom name \"{name}\" is not a valid GraphQL name.");
            }

            if (Schema.TryGetType(name, out var existingNamed) && !existingNamed.Equals(Definition))
            {
                throw new DuplicateItemException(
                    $"Cannot set CLR type on {Definition} with custom name: the custom name \"{name}\" conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named '{existingNamed.Name}'. All type names must be unique.");
            }

            SetName(name, configurationSource);
            return SetClrType(clrType, false, configurationSource);
        }

        public bool SetClrType(Type clrType, bool inferName, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_clrTypeConfigurationSource))
            {
                return false;
            }

            if (Definition != null && Schema.TryGetType(clrType, out var existingTyped) &&
                !existingTyped.Equals(Definition))
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotChangeClrType(Definition, clrType,
                        existingTyped));
            }

            if (inferName)
            {
                if (clrType.TryGetGraphQLNameFromDataAnnotation(out var annotated))
                {
                    if (!annotated.IsValidGraphQLName())
                    {
                        throw new InvalidNameException(
                            $"Cannot set CLR type on {Definition} and infer name: the annotated name \"{annotated}\" on CLR {clrType.GetClrTypeKind()} '{clrType.Name}' is not a valid GraphQL name.");
                    }

                    if (Schema.TryGetType(annotated, out var existingNamed) && !existingNamed.Equals(Definition))
                    {
                        throw new DuplicateItemException(
                            $"Cannot set CLR type on {Definition} and infer name: the annotated name \"{annotated}\" on CLR {clrType.GetClrTypeKind()} '{clrType.Name}' conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named {existingNamed.Name}. All GraphQL type names must be unique.");
                    }

                    SetName(annotated, configurationSource);
                }
                else
                {
                    if (!clrType.Name.IsValidGraphQLName())
                    {
                        throw new InvalidNameException(
                            $"Cannot set CLR type on {Definition} and infer name: the CLR {clrType.GetClrTypeKind()} name '{clrType.Name}' is not a valid GraphQL name.");
                    }

                    if (Schema.TryGetType(clrType.Name, out var existingNamed) && !existingNamed.Equals(Definition))
                    {
                        throw new DuplicateItemException(
                            $"Cannot set CLR type on {Definition} and infer name: the CLR {clrType.GetClrTypeKind()} name '{clrType.Name}' conflicts with an existing {existingNamed.Kind.ToDisplayStringLower()} named {existingNamed.Name}. All GraphQL type names must be unique.");
                    }

                    SetName(clrType.Name, configurationSource);
                }
            }

            _clrTypeConfigurationSource = configurationSource;
            ClrType = clrType;
            return true;
        }

        public bool RemoveClrType(ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_clrTypeConfigurationSource))
            {
                return false;
            }

            _clrTypeConfigurationSource = configurationSource;
            ClrType = null;
            return true;
        }

        public ConfigurationSource? GetClrTypeConfigurationSource() => _clrTypeConfigurationSource;
        public ConfigurationSource GetConfigurationSource() => throw new NotImplementedException();


        public string Name { get; private set; }

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_nameConfigurationSource))
            {
                return false;
            }

            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    $"Cannot rename {Definition}: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            }

            if (Definition != null && Schema.TryGetType(name, out var existingName) &&
                !existingName.Equals(Definition))
            {
                throw new DuplicateItemException(
                    $"Cannot rename {Definition} to \"{name}\": a type with that name ({existingName}) already exists. All GraphQL type names must be unique.");
            }

            Name = name;
            _nameConfigurationSource = configurationSource;
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;


        private bool Equals(TypeIdentity other) => Overlaps(other);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((TypeIdentity)obj);
        }

        // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
        public override int GetHashCode() => base.GetHashCode();


        public bool Overlaps(TypeIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            if (ClrType != null && identity.ClrType != null)
            {
                if (IsInputType == true && identity.IsInputType == true
                    || IsOutputType == true && identity.IsOutputType == true)
                {
                    return ClrType == identity.ClrType;
                }
            }

            return string.Equals(Name, identity.Name);
        }

        public override string ToString() => $"type {Name}";
    }
}