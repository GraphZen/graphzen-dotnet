// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types.Internal
{
    public enum ConfigurationSource
    {
        Explicit,
        DataAnnotation,
        Convention
    }

    public static class ConfigurationSourceExtensions
    {
        public static bool Overrides(this ConfigurationSource newConfigurationSource,
            ConfigurationSource? oldConfigurationSource)
        {
            if (oldConfigurationSource == null)
            {
                return true;
            }

            if (newConfigurationSource == ConfigurationSource.Explicit)
            {
                return true;
            }

            if (oldConfigurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            if (newConfigurationSource == ConfigurationSource.DataAnnotation)
            {
                return true;
            }

            return oldConfigurationSource == ConfigurationSource.DataAnnotation ? false : true;
        }


        public static bool Overrides(this ConfigurationSource? newConfigurationSource,
            ConfigurationSource? oldConfigurationSource)
            => newConfigurationSource?.Overrides(oldConfigurationSource) ?? oldConfigurationSource == null;


        public static bool OverridesStrictly(this ConfigurationSource newConfigurationSource,
            ConfigurationSource? oldConfigurationSource)
            => newConfigurationSource.Overrides(oldConfigurationSource) &&
               newConfigurationSource != oldConfigurationSource;


        [ContractAnnotation("left:notnull => notnull;right:notnull => notnull")]
        public static ConfigurationSource? Max(this ConfigurationSource? left, ConfigurationSource? right) =>
            !right.HasValue
            || left.HasValue
            && left.Value.Overrides(right.Value)
                ? left
                : right.Value;

        public static ConfigurationSource Max(this ConfigurationSource left, ConfigurationSource? right)
            => Max((ConfigurationSource?) left, right).Value;
    }
}