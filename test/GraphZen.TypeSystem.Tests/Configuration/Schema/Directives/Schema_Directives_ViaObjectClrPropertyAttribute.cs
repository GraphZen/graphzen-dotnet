// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.Directives;

// ReSharper disable once InconsistentNaming
public class Schema_Directives_ViaObjectClrPropertyAttribute : Schema_Directives,
    ICollectionConventionConfigurationFixture
{
    public CollectionConventionContext GetContext() => new()
    {
        ItemNamedByConvention = nameof(FieldDefinition),
        ItemNamedByDataAnnotation = "hello"
    };

    public void ConfigureContextConventionally(SchemaBuilder sb)
    {
        sb.Object<FooObject>();
    }

    public void ConfigureClrContext(SchemaBuilder sb, string parentName)
    {
        sb.Object<FooObject>();
    }

    [GraphQLName("hello")]
    public class FieldDefinitionTwoAttribute : Attribute, IGraphQLDirective
    {
        public IEnumerable<DirectiveLocation> GetDirectiveLocations()
        {
            yield return DirectiveLocation.FieldDefinition;
        }
    }

    public class FieldDefinitionAttribute : Attribute, IGraphQLDirective
    {
        public IEnumerable<DirectiveLocation> GetDirectiveLocations()
        {
            yield return DirectiveLocation.FieldDefinition;
        }
    }

    public class FooObject
    {
        [FieldDefinition] public string? Bar { get; set; }
    }
}
