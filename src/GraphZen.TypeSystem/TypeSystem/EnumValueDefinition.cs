// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class EnumValueDefinition : AnnotatableMemberDefinition, IMutableEnumValueDefinition
    {
        private ConfigurationSource _nameConfigurationSource;

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

        public InternalEnumValueBuilder Builder { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.EnumValue;

        public object Value { get; set; }
        IEnumTypeDefinition IEnumValueDefinition.DeclaringType => DeclaringType;

        public EnumTypeDefinition DeclaringType { get; }
        public string Name { get; private set; }


        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(_nameConfigurationSource)) return false;

            _nameConfigurationSource = configurationSource;
            if (name != Name)
            {
                Name = name;
                return true;
            }

            return false;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;

        public bool MarkAsDeprecated(string reason, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public bool RemoveDeprecation(ConfigurationSource configurationSource) => throw new NotImplementedException();

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public bool IsDeprecated { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public string? DeprecationReason { get; }
    }
}