using System.Diagnostics.CodeAnalysis;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Configuration.Interfaces.Fields.Arguments
{
    public class Interface_Field_Arguments_ViaClrMethodParameters : Interface_Field_Arguments,
        ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);

        [GraphQLName(Grandparent)]
        public interface ISomeInterface
        {
            string SomeField(
                string arg1,
                [GraphQLIgnore] string ignoreMe,
                [GraphQLName(DataAnnotationName)] string arg2);
        }

        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ParentName = nameof(ISomeInterface.SomeField).FirstCharToLower(),
                ItemIgnoredByDataAnnotation = "ignoreMe",
                ItemNamedByConvention = "arg1",
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = "na"
            };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Interface<ISomeInterface>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Interface<ISomeInterface>();
        }
    }
}