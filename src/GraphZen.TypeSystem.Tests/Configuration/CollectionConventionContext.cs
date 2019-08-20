using GraphZen.Infrastructure;

namespace GraphZen
{
    public class CollectionConventionContext
    {
        public string ParentName { get; set; }
        public string ItemIgnoredByDataAnnotation { get; set; }
        public string ItemIgnoredByConvention { get; set; }
        public string ItemNamedByConvention { get; set; }
        public string ItemNamedByDataAnnotation { get; set; }
    }
}