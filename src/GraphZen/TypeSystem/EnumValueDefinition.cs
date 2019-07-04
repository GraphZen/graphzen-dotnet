// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;


namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnumValueDefinition : AnnotatableMemberDefinition, IMutableEnumValueDefinition
    {
        private readonly ConfigurationSource _nameConfigurationSource;
        private string _deprecationReason;
        private bool _isDeprecated;

        public EnumValueDefinition(string name, ConfigurationSource nameConfigurationSource,
            EnumTypeDefinition declaringType,
            SchemaDefinition schema, ConfigurationSource configurationSource) :
            base(configurationSource)
        {
            _nameConfigurationSource = nameConfigurationSource;
            DeclaringType = Check.NotNull(declaringType, nameof(declaringType));
            Value = Name = Check.NotNull(name, nameof(name));
            Builder = new InternalEnumValueBuilder(this, Check.NotNull(schema, nameof(schema)).Builder);
        }

        private string DebuggerDisplay => $"enum value {Name}";

        [NotNull]
        public InternalEnumValueBuilder Builder { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.EnumValue;

        public object Value { get; set; }
        IEnumTypeDefinition IEnumValueDefinition.DeclaringType => DeclaringType;

        public EnumTypeDefinition DeclaringType { get; }
        public string Name { get; private set; }


        public bool IsDeprecated
        {
            get => _isDeprecated || DeprecationReason != null;
            set
            {
                _isDeprecated = value;
                if (!_isDeprecated)
                {
                    DeprecationReason = null;
                }
            }
        }

        public string DeprecationReason
        {
            get => _deprecationReason;
            set
            {
                _deprecationReason = value;
                if (_deprecationReason != null)
                {
                    IsDeprecated = true;
                }
            }
        }

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(_nameConfigurationSource))
            {
                Check.NotNull(name, nameof(name));
                Name = name;
                return true;
            }


            return false;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;

        public bool MarkAsDeprecated(string reason, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public bool RemoveDeprecation(ConfigurationSource configurationSource) => throw new NotImplementedException();
    }
}