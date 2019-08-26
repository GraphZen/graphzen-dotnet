// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Enums.EnumValues
{
    public class Enum_Values_ViaClrEnumValues : Enum_Values, ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);

        public CollectionConventionContext GetContext()
        {
            return new CollectionConventionContext
            {
                ParentName = nameof(ExampleEnum),
                ItemNamedByConvention = nameof(ExampleEnum.HelloWorld),
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = "IgnoredByConvention",
                ItemIgnoredByDataAnnotation = nameof(ExampleEnum.IgnoredByDataAnnotation)
            };
        }

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Enum<ExampleEnum>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Enum(parentName).ClrType<ExampleEnum>();
        }


        public enum ExampleEnum
        {
            HelloWorld,
            [GraphQLName(DataAnnotationName)] NamedByDataAnnotation,
            [GraphQLIgnore] IgnoredByDataAnnotation
        }

        public override string ToString()
        {
            return nameof(Enum_Values_ViaClrEnumValues);
        }
    }
}