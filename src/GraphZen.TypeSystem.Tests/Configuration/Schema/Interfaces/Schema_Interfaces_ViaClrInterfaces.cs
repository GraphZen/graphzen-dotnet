// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.Interfaces
{
    public class Schema_Interfaces_ViaClrInterfaces : Schema_Interfaces, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


        public CollectionConventionContext GetContext() => new CollectionConventionContext
        {
            ItemNamedByConvention = nameof(INamedByConvention),
            ItemNamedByDataAnnotation = DataAnnotationName,
            ItemIgnoredByConvention = nameof(IIgnoredByConvention),
            ItemIgnoredByDataAnnotation = nameof(IIgnoredByDataAnnotation)
        };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<Query>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<Query>();
        }

        public class Query
        {
            public INamedByConvention ConventionallyNamed { get; set; }

            [GraphQLIgnore]
            public IIgnoredByConvention IgnoredByConvention { get; set; }

            public IIgnoredByDataAnnotation IgnoredByDataAnnotation { get; set; }

            public INamedByDataAnnotation NamedByDataAnnoation { get; set; }
        }

        public interface INamedByConvention
        {
        }


        [GraphQLName(DataAnnotationName)]
        public interface INamedByDataAnnotation
        {
        }

        public interface IIgnoredByConvention
        {
        }

        [GraphQLIgnore]
        public interface IIgnoredByDataAnnotation
        {
        }
    }
}