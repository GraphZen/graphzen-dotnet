// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // public abstract class TypeReferenceDefinition : AnnotatableMemberDefinition { }

    public abstract class InputValueDefinition : AnnotatableMemberDefinition, IMutableInputValueDefinition
    {
        private ConfigurationSource? _defaultValueConfigurationSource;
        private ConfigurationSource _typeReferenceConfigurationSource;
        protected ConfigurationSource NameConfigurationSource;

        protected InputValueDefinition(
            string name,
            ConfigurationSource nameConfigurationSource,
            TypeIdentity typeIdentity,
            TypeSyntax typeSyntax,
            SchemaDefinition schema,
            ConfigurationSource configurationSource,
            object? clrInfo, IMemberDefinition declaringMember) : base(configurationSource)
        {
            ClrInfo = clrInfo;
            Schema = schema;
            DeclaringMember = declaringMember;

            NameConfigurationSource = nameConfigurationSource;
            Name = name;
            Builder = new InternalInputValueBuilder(this, schema.Builder);
            TypeReference = new TypeReference(typeIdentity, typeSyntax, this, configurationSource);
            _typeReferenceConfigurationSource = configurationSource;
        }

        protected override SchemaDefinition Schema { get; }


        public InternalInputValueBuilder Builder { get; }


        public IMemberDefinition DeclaringMember { get; }

        public bool SetDefaultValue(object? value, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_defaultValueConfigurationSource))
            {
                return false;
            }

            DefaultValue = value;
            HasDefaultValue = true;
            _defaultValueConfigurationSource = configurationSource;
            return true;
        }

        public bool RemoveDefaultValue(ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_defaultValueConfigurationSource))
            {
                return false;
            }

            DefaultValue = null;
            HasDefaultValue = false;
            _defaultValueConfigurationSource = configurationSource;
            return true;
        }

        public ConfigurationSource? GetDefaultValueConfigurationSource() => _defaultValueConfigurationSource;
        public object? DefaultValue { get; private set; }
        public bool HasDefaultValue { get; private set; }
        public string Name { get; protected set; }
        public abstract bool SetName(string name, ConfigurationSource configurationSource);
        public ConfigurationSource GetNameConfigurationSource() => NameConfigurationSource;
        public object? ClrInfo { get; }
        public ConfigurationSource GetTypeReferenceConfigurationSource() => _typeReferenceConfigurationSource;

        public TypeReference TypeReference { get; private set; }
        public bool SetTypeReference(TypeReference type, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetTypeReferenceConfigurationSource()))
            {
                return false;
            }

            TypeReference = type;
            _typeReferenceConfigurationSource = configurationSource;
            return true;
        }

        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => TypeReference;
    }
}