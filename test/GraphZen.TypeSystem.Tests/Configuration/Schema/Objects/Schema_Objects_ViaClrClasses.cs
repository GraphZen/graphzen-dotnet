// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects
{
    // ReSharper disable once InconsistentNaming
    public class Schema_Objects_ViaClrClasses : Schema_Objects, ICollectionConventionConfigurationFixture

    {
        public const string DataAnnotationName = nameof(DataAnnotationName);

        public class Query
        {
            public NamedByConvention? ConventionallyNamed { get; set; }

            [GraphQLIgnore] public IgnoredByConvention? IgnoredByConvention { get; set; }

            public IgnoredByDataAnnotation? IgnoredByDataAnnotation { get; set; }

            public NamedByDataAnnotation? NamedByDataAnnoation { get; set; }
        }

        public class NamedByConvention
        {
        }


        [GraphQLName(DataAnnotationName)]
        public class NamedByDataAnnotation
        {
        }

        public class IgnoredByConvention
        {
        }

        [GraphQLIgnore]
        public class IgnoredByDataAnnotation
        {
        }


        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ItemNamedByConvention = nameof(NamedByConvention),
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = nameof(IgnoredByConvention),
                ItemIgnoredByDataAnnotation = nameof(IgnoredByDataAnnotation)
            };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<Query>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<Query>();
        }
    }
}