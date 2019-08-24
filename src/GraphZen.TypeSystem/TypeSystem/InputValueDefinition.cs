// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public abstract class InputValueDefinition : AnnotatableMemberDefinition, IMutableInputValueDefinition
    {
        private ConfigurationSource? _defaultValueConfigurationSource;
        protected ConfigurationSource NameConfigurationSource;

        public InputValueDefinition(
             string name,
            ConfigurationSource nameConfigurationSource,
             SchemaDefinition schema,
            ConfigurationSource configurationSource,
            object clrInfo,  IMemberDefinition declaringMember) : base(configurationSource)
        {
            ClrInfo = clrInfo;
            DeclaringMember = declaringMember;
            NameConfigurationSource = nameConfigurationSource;
            Name = Check.NotNull(name, nameof(name));
            Builder = new InternalInputValueBuilder(this, schema.Builder);
        }


        
        public InternalInputValueBuilder Builder { get; }

        public IGraphQLTypeReference InputType { get; set; }

        public bool SetDefaultValue(object value, ConfigurationSource configurationSource)
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

        public IMemberDefinition DeclaringMember { get; }
        public object DefaultValue { get; private set; }
        public bool HasDefaultValue { get; private set; }


        public string Name { get; protected set; }

        public abstract bool SetName(string name, ConfigurationSource configurationSource);

        public ConfigurationSource GetNameConfigurationSource() => NameConfigurationSource;
        public object ClrInfo { get; }
    }
}