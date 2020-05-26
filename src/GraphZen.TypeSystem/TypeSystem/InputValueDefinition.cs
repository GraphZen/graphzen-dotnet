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
    public abstract class InputValueDefinition : AnnotatableMemberDefinition, IMutableInputValueDefinition
    {
        private readonly TypeReferenceConfiguration<InputValueDefinition> _typeRefConfig;
        private ConfigurationSource? _defaultValueConfigurationSource;
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
            _typeRefConfig = new TypeReferenceConfiguration<InputValueDefinition>(typeIdentity, typeSyntax, this);
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

        public ConfigurationSource GetTypeReferenceConfigurationSource() =>
            _typeRefConfig.GetTypeReferenceConfigurationSource();

        public TypeReference TypeReference => _typeRefConfig.TypeReference;

        public bool SetTypeReference(TypeReference type, ConfigurationSource configurationSource) =>
            _typeRefConfig.SetTypeReference(type, configurationSource);


        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => TypeReference;
    }
}