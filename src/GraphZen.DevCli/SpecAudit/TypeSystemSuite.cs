// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using GraphZen.TypeSystem.FunctionalTests.Directives;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit
{
    public static class TypeSystemSuite
    {
        public static SpecSuite Create()
        {
            var name = new Subject("Name").WithSpecs<ConfigurableItemSpecs>();
            var description = new Subject("Description").WithSpecs<ConfigurableItemSpecs>();
            var inputValue = new Subject("Input Value").WithChild(name);
            var inputField = inputValue.WithName("Input Field");
            var argument = inputValue.WithName("Argument");
            var argumentCollection = new Subject("Arguments");
            var directiveAnnoation = new Subject("Directive Annotation");
            var directiveAnnotationCollection = new Subject("Directive Annotations");
            var inputOutputType = new Subject("Input or Output Type Reference");
            var inputTypeRef = inputOutputType.WithName("Input Type Reference");
            var outputTypeRef = inputOutputType.WithName("Output Type Reference");

            var outputField = new Subject("Field")
                .WithChildren(name, outputTypeRef.WithName("Field Type"));

            var objectType = new Subject("Object Type")
                .WithChild(new Subject("Fields").WithChild(outputField.WithName("Object Field")))
                .WithChild(new Subject("Implemented Interface"));

            var objectTypeCollection = new Subject("Object Types")
                .WithChild(objectType);

            var schema = new Subject("Schema")
                .WithSpecs<ConfigurableItemSpecs>(SpecPriority.High)
                .WithChild(description)
                .WithChild(directiveAnnotationCollection)
                .WithChild(new Subject("Query Type"))
                .WithChild(new Subject("Mutation Type"))
                .WithChild(new Subject("Subscription Type"))
                .WithChild(objectTypeCollection);

            var specs = Spec.From(
                typeof(ConfigurableItemSpecs),
                typeof(OptionalItemSpecs)
            );


            return new SpecSuite("Type System", schema, specs, typeof(DirectiveCreationTests).Assembly);
        }
    }
}