// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects.Fields.Arguments.Description
{
    // ReSharper disable once InconsistentNaming
    public class Object_Field_Argument_ViaClrMethod_Description : Object_Field_Argument_Description,
        ILeafConventionConfigurationFixture
    {
        [GraphQLName(GreatGrandparent)]
        public class ExampleObject
        {
            [GraphQLName(Grandparent)]
            public string ExampleField([Description(DataAnnotationDescriptionValue)]
                string argName) =>
                throw new NotImplementedException();
        }

        public const string DataAnnotationDescriptionValue = nameof(DataAnnotationDescriptionValue);

        public LeafConventionContext GetContext() =>
            new LeafConventionContext
            {
                ParentName = "argName",
                DataAnnotationValue = DataAnnotationDescriptionValue
            };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<ExampleObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<ExampleObject>();
        }
    }
}