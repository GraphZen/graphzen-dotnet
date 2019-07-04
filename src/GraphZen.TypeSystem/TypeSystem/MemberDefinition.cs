// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;


namespace GraphZen.TypeSystem
{
    public abstract class MemberDefinition : IMutableDefinition
    {
        private ConfigurationSource _configurationSource;
        private ConfigurationSource? _descriptionConfigurationSource;

        public MemberDefinition(ConfigurationSource configurationSource)
        {
            _configurationSource = configurationSource;
        }

        public string Description { get; private set; }


        public bool SetDescription(string description, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(_descriptionConfigurationSource))
            {
                Description = description;
                _descriptionConfigurationSource = configurationSource;
                return true;
            }

            return false;
        }

        public ConfigurationSource? GetDescriptionConfigurationSource() => _descriptionConfigurationSource;

        public ConfigurationSource GetConfigurationSource() => _configurationSource;


        public void UpdateConfigurationSource(ConfigurationSource configurationSource) =>
            _configurationSource = _configurationSource.Max(configurationSource);
    }
}