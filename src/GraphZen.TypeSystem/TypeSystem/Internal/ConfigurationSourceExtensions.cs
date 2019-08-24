using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
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

            return oldConfigurationSource != ConfigurationSource.DataAnnotation;
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
            => Max((ConfigurationSource?)left, right).Value;
    }
}