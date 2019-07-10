// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace GraphZen.Configuration
{
    public abstract class ScalarTypeNameTests : ScalarTypeNameTestsBase
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);


        public override string ConventionalParentName { get; } = nameof(ConventionalScalarName);
        public override object ConventionalValue => ConventionalParentName;
        public override object DataAnnotationValue => DataAnnotationName;

        public override string DataAnnotationParentName => nameof(NamedByDataAnnotation);

        public override ConfigurationSource GetElementConfigurationSource(IMutableNamed definition) =>
            definition.GetNameConfigurationSource();

        public override IMutableNamed GetParentDefinition(SchemaDefinition schemaDefinition, string parentName) =>
            schemaDefinition.GetScalar(parentName);

        public override INamed GetParent(Schema schema, string parentName) => schema.GetScalar(parentName);

        public override void DefineByConvention(SchemaBuilder sb) => sb.Scalar<ConventionalScalarName>();

        public override void DefineByDataAnnotation(SchemaBuilder sb) => sb.Scalar<NamedByDataAnnotation>();

        public class ConventionalScalarName
        {
        }

        [GraphQLName(DataAnnotationName)]
        public class NamedByDataAnnotation
        {
        }
    }
}