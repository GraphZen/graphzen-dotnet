// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using GraphZen.TypeSystem.FunctionalTests.Directives;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.SpecAudit
{
    public static class TypeSystemSuite
    {
        public static SpecSuite Create()
        {
            var name = new Subject("Name")
                .WithSpecs<UpdateableSpecs>()
                .WithSpecs<RequiredSpecs>();

            var description = new Subject("Description")
                .WithSpecs<UpdateableSpecs>()
                .WithSpecs<OptionalSpecs>();

            var typeRef = new Subject("Type")
                .WithSpecs<RequiredSpecs>()
                .WithSpecs<UpdateableSpecs>();

            var inputTypeRef = typeRef.WithName("Input Type Reference");
            var outputTypeRef = typeRef.WithName("Output Type Reference");

            var argument = new Subject("Argument").WithChild(name)
                .WithChild(new Subject("Value").WithSpecs<RequiredSpecs>().WithSpecs<UpdateableSpecs>());

            var argumentCollection = new Subject("Arguments")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(argument);
            var directiveAnnotation = new Subject("Directive Annotation")
                .WithChild(name)
                .WithChild(argumentCollection);

            var directiveAnnotations = new Subject("Directive Annotations")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(directiveAnnotation);

            var inputValue = new Subject("Input Value")
                .WithChild(description)
                .WithChild(inputTypeRef)
                .WithChild(new Subject("Default Value").WithSpecs<OptionalSpecs>())
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var argumentDef = inputValue.WithName("Argument Definition");
            var argumentDefCollection = new Subject("Arguments Definition")
                .WithChild(argumentDef)
                .WithSpecs<NamedCollectionSpecs>();

            var outputField = new Subject("Field")
                .WithChild(name)
                .WithChild(description)
                .WithChild(argumentDefCollection)
                .WithChild(directiveAnnotations)
                .WithChildren(outputTypeRef.WithName("Field Type"));

            var outputFields = new Subject("Fields")
                .WithSpecs<NamedCollectionSpecs>();

            var implementsInterfaces = new Subject("Implements Interfaces").WithSpecs<NamedTypeSetSpecs>();

            var objectType = new Subject("Object Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(outputFields.WithChild(outputField.WithName("Object Field")))
                .WithChild(implementsInterfaces);

            var objects = new Subject("Objects")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(objectType);


            var scalar = new Subject("Scalar Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var scalars = new Subject("Scalars")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(scalar);

            var interfaceType = new Subject("Interface Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(outputFields.WithChild(outputField.WithName("Interface Field")))
                .WithChild(implementsInterfaces);

            var interfaces = new Subject("Interfaces")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(interfaceType);

            var unionType = new Subject("Union Type").WithChild(description).WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Union Member Types").WithSpecs<NamedTypeSetSpecs>());

            var unions = new Subject("Unions")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(unionType);

            var enumType = new Subject("Enum Type")
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(description)
                .WithChild(
                    new Subject("Enum Values")
                        .WithSpecs<NamedCollectionSpecs>()
                        .WithChild(new Subject("Enum Value")
                            .WithChild(name)
                            .WithChild(directiveAnnotations)
                            .WithChild(description)));

            var enums = new Subject("Enums")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(enumType);

            var inputObjectType = new Subject("Input Object Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Fields")
                    .WithSpecs<NamedCollectionSpecs>()
                    .WithChild(inputValue.WithName("Input Field")));

            var inputObjects = new Subject("Input Objects")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(inputObjectType);

            var directive = new Subject("Directive")
                .WithChild(name)
                .WithChild(new Subject("Repeatable").WithSpecs<OptionalSpecs>())
                .WithChild(new Subject("Locations").WithSpecs<NamedCollectionSpecs>())
                .WithChild(description);

            var directives = new Subject("Directives")
                .WithSpecs<NamedCollectionSpecs>()
                .WithSpecs<DirectivesSpecs>()
                .WithChild(directive);

            var schema = new Subject("Schema")
                .WithChild(description)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Query Type"))
                .WithChild(new Subject("Mutation Type"))
                .WithChild(new Subject("Subscription Type"))
                .WithChild(directives.WithSpecPriority(SpecPriority.High, true))
                .WithChild(scalars)
                .WithChild(objects)
                .WithChild(interfaces)
                .WithChild(unions)
                .WithChild(enums)
                .WithChild(inputObjects);

            var specs = Spec.GetSpecs(typeof(TypeSystemSpecs));
            return new SpecSuite("Type System", schema, specs, typeof(DirectiveCreationTests).Assembly);
        }
    }
}