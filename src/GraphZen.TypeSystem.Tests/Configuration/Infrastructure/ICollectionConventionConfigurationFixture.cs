using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public interface ICollectionConventionConfigurationFixture : ICollectionConfigurationFixture
    {
        CollectionConventionContext GetContext();
        void ConfigureContextConventionally([NotNull] SchemaBuilder sb);

        void ConfigureClrContext([NotNull] SchemaBuilder sb, string parentName);
    }
}