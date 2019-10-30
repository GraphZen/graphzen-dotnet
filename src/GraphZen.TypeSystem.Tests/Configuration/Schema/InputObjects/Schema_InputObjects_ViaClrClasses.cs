// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Configuration.InputObjects
{
    public class Schema_InputObjects_ViaClrClasses : Schema_InputObjects, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


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
            sb.InputObject<ParentInputObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.InputObject<ParentInputObject>();
        }

        public void AddItemNamedByDataAnnotationViaClrType(SchemaBuilder sb)
        {
            throw new NotImplementedException();
        }


#nullable disable
        public class ParentInputObject
        {
            public NamedByConvention ConventionallyNamed { get; set; }

            [GraphQLIgnore] public IgnoredByConvention IgnoredByConvention { get; set; }

            public IgnoredByDataAnnotation IgnoredByDataAnnotation { get; set; }

            public NamedByDataAnnotation NamedByDataAnnoation { get; set; }
        }
#nullable restore

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
    }
}