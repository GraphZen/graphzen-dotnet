// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using GraphZen.TypeSystem.FunctionalTests.Directives;
using JetBrains.Annotations;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSytemSpecs;

namespace GraphZen.SpecAudit
{
    public static class TypeSystemSuite
    {
        public static SpecSuite Create()
        {
            var name = new Subject("Name").WithSpecs<ConfigurableItemSpecs>();
            var description = new Subject("Description").WithSpecs<ConfigurableItemSpecs>();
            var typeRef = new Subject("Type");
            var inputTypeRef = typeRef.WithName("Input Type Reference");
            var outputTypeRef = typeRef.WithName("Output Type Reference");

            var argument = new Subject("Argument").WithChild(name).WithChild(new Subject("Value"));
            var argumentCollection = new Subject("Arguments").WithChild(argument);
            var directiveAnnotation = new Subject("Directive Annotation").WithChild(name).WithChild(argumentCollection);
            var directiveAnnotations = new Subject("Directive Annotations").WithChild(directiveAnnotation);

            var inputValue = new Subject("Input Value")
                .WithChild(description)
                .WithChild(inputTypeRef)
                .WithChild(new Subject("Default Value"))
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var argumentDef = inputValue.WithName("Argument Definition");
            var argumentDefCollection = new Subject("Arguments Definition").WithChild(argumentDef);

            var outputField = new Subject("Field")
                .WithChild(name)
                .WithChild(description)
                .WithChild(argumentDefCollection)
                .WithChild(directiveAnnotations)
                .WithChildren(outputTypeRef.WithName("Field Type"));

            var objectType = new Subject("Object Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Fields").WithChild(outputField.WithName("Object Field")))
                .WithChild(new Subject("Implemented Interfaces"));

            var objects = new Subject("Objects")
                .WithChild(objectType);


            var scalar = new Subject("Scalar Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var scalars = new Subject("Scalars").WithChild(scalar);

            var interfaceType = new Subject("Interface Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Interface Fields").WithChild(outputField.WithName("Interface Field")))
                .WithChild(new Subject("Implemented Interfaces"));

            var interfaces = new Subject("Interfaces").WithChild(interfaceType);

            var unionType = new Subject("Union Type").WithChild(description).WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Union Member Types"));

            var unions = new Subject("Union").WithChild(unionType);

            var enumType = new Subject("Enum Type")
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(description)
                .WithChild(
                    new Subject("Enum Values")
                        .WithChild(new Subject("Enum Value")
                            .WithChild(name)
                            .WithChild(directiveAnnotations)
                            .WithChild(description)));

            var enums = new Subject("Enums").WithChild(enumType);

            var inputObjectType = new Subject("Input Object Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Fields").WithChild(inputValue.WithName("Input Field")));

            var inputObjects = new Subject("Input Objects").WithChild(inputObjectType);

            var directive = new Subject("Directive")
                .WithChild(name)
                .WithChild(new Subject("Repeatable"))
                .WithChild(new Subject("Locations"))
                .WithChild(description);

            var directives = new Subject("Directives").WithChild(directive);

            var schema = new Subject("Schema")
                .WithChild(description)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Query Type"))
                .WithChild(new Subject("Mutation Type"))
                .WithChild(new Subject("Subscription Type"))
                .WithChild(directives)
                .WithChild(scalars)
                .WithChild(objects)
                .WithChild(interfaces)
                .WithChild(unions)
                .WithChild(enums)
                .WithChild(inputObjects);


            var specs = Spec.From(
                typeof(ConfigurableItemSpecs)
            );

            return new SpecSuite("Type System", schema, specs, typeof(DirectiveCreationTests).Assembly);
        }
    }
}