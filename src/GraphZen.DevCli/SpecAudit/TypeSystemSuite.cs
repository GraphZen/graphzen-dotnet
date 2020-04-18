// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit
{
    public static class TypeSystemSuite
    {
        public static SpecSuite Create()
        {
            var name = new SpecSubject("Name").WithSpecs<ConfigurableItemSpecs>();
            var description = new SpecSubject("Description").WithSpecs<ConfigurableItemSpecs>();
            var inputValue = new SpecSubject("Input Value").WithChild(name);
            var inputField = inputValue.WithName("Input Field");
            var argument = inputValue.WithName("Argument");
            var argumentCollection = new SpecSubject("Arguments");
            var directiveAnnoation = new SpecSubject("Directive Annotation");
            var directiveAnnotationCollection = new SpecSubject("Directive Annotations");
            var inputOutputType = new SpecSubject("Input or Output Type Reference");
            var inputTypeRef = inputOutputType.WithName("Input Type Reference");
            var outputTypeRef = inputOutputType.WithName("Output Type Reference");

            var outputField = new SpecSubject("Field")
                .WithChildren(name, outputTypeRef.WithName("Field Type"));

            var objectType = new SpecSubject("Object Type")
                .WithChild(new SpecSubject("Fields").WithChild(outputField.WithName("Object Field")))
                .WithChild(new SpecSubject("Implemented Interface"));

            var objectTypeCollection = new SpecSubject("Object Types")
                .WithChild(objectType);

            var schema = new SpecSubject("Schema")
                .WithChild(description)
                .WithChild(directiveAnnotationCollection)
                .WithChild(new SpecSubject("Query Type"))
                .WithChild(new SpecSubject("Mutation Type"))
                .WithChild(new SpecSubject("Subscription Type"))
                .WithChild(objectTypeCollection);

            var specs = Spec.From(
                typeof(ConfigurableItemSpecs),
                typeof(OptionalItemSpecs)
            );


            return new SpecSuite("Type System", schema, specs);
        }
    }
}