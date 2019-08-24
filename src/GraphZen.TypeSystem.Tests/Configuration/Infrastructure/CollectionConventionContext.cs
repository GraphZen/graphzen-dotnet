#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen
{
    public class CollectionConventionContext
    {
        public string ParentName { get; set; }
        public string ItemIgnoredByDataAnnotation { get; set; }
        public string ItemIgnoredByConvention { get; set; }
        public string ItemNamedByConvention { get; set; }
        public string ItemNamedByDataAnnotation { get; set; }

        public ConfigurationSource? DefaultItemConfigurationSource { get; set; }
    }
}