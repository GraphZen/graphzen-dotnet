using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public interface ICollectionConventionConfigurationFixture : ICollectionConfigurationFixture
    {
        CollectionConventionContext ConfigureViaConvention([NotNull] SchemaBuilder sb);
    }
}