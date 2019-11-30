using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Directives
{
    public class Schema_Directives_ViaObjectClrPropertyAttribute : Schema_Directives, ICollectionConventionConfigurationFixture
    {
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
            [FieldDefinition]
            public string? Bar { get; set; }
        }



        public CollectionConventionContext GetContext() => new CollectionConventionContext()
        {
            ItemNamedByConvention = nameof(FieldDefinition),
            ItemNamedByDataAnnotation = "hello",
            



        };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<FooObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<FooObject>();
        }
    }
}