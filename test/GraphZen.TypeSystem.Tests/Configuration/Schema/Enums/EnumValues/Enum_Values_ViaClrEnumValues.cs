// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.Enums.EnumValues;

// ReSharper disable once InconsistentNaming
public class Enum_Values_ViaClrEnumValues : Enum_Values, ICollectionConventionConfigurationFixture
{
    public enum ExampleEnum
    {
        HelloWorld,
        [GraphQLName(DataAnnotationName)] NamedByDataAnnotation,
        [GraphQLIgnore] IgnoredByDataAnnotation
    }

    public const string DataAnnotationName = nameof(DataAnnotationName);

    public CollectionConventionContext GetContext() =>
        new()
        {
            ParentName = nameof(ExampleEnum),
            ItemNamedByConvention = nameof(ExampleEnum.HelloWorld),
            ItemNamedByDataAnnotation = DataAnnotationName,
            ItemIgnoredByConvention = "IgnoredByConvention",
            ItemIgnoredByDataAnnotation = nameof(ExampleEnum.IgnoredByDataAnnotation)
        };

    public void ConfigureContextConventionally(SchemaBuilder sb)
    {
        sb.Enum<ExampleEnum>();
    }

    public void ConfigureClrContext(SchemaBuilder sb, string parentName)
    {
        sb.Enum(parentName).ClrType<ExampleEnum>();
    }

    public override string ToString() => nameof(Enum_Values_ViaClrEnumValues);
}