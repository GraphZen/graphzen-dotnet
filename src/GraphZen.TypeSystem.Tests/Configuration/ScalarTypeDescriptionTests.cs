// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen.Configuration
{
    public class ScalarTypeDescriptionTests : ScalarTypeDescriptionTestsBase
    {
        public const string DataAnnotationDescription = "description from data annotation";
        public override object DataAnnotationValue => DataAnnotationDescription;
        public override string DataAnnotationParentName => nameof(ScalarDescriptionViaDataAnnotation);
        public override string NotDefinedByConventionParentName => nameof(ScalarNoDescription);
        public override object ExplicitValue { get; } = "Explicit Description";

        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription definition) =>
            definition.GetDescriptionConfigurationSource();


        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Scalar(parentName).Description(ExplicitValue as string);
        }

        public override IMutableDescription GetParentDefinition(SchemaDefinition schemaDefinition, string parentName) =>
            schemaDefinition.GetScalar(parentName);

        public override IDescription GetParent(Schema schema, string parentName) => schema.GetScalar(parentName);

        public override bool TryGetValue(IDescription parent, out object value)
        {
            value = parent.Description;
            return parent.Description != null;
        }

        public override void DefineByDataAnnotation(SchemaBuilder sb)
        {
            sb.Scalar<ScalarDescriptionViaDataAnnotation>();
        }

        public override void DefineEmptyByConvention(SchemaBuilder sb)
        {
            sb.Scalar<ScalarNoDescription>();
        }

        public class ScalarNoDescription
        {
        }

        [Description(DataAnnotationDescription)]
        public class ScalarDescriptionViaDataAnnotation
        {
        }
    }
}