// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using GraphZen.TypeSystem.FunctionalTests.Directives;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSytemSpecs;

namespace GraphZen.SpecAudit
{
    public static class TypeSystemSuite
    {
        public static SpecSuite Create()
        {
            var name = new Subject("Name")
                .WithSpecs<Updateable>()
                .WithSpecs<Required>();

            var description = new Subject("Description")
                .WithSpecs<Updateable>()
                .WithSpecs<Optional>();

            var typeRef = new Subject("Type")
                .WithSpecs<Required>()
                .WithSpecs<Updateable>();

            var inputTypeRef = typeRef.WithName("Input Type Reference");
            var outputTypeRef = typeRef.WithName("Output Type Reference");

            var argument = new Subject("Argument").WithChild(name)
                .WithChild(new Subject("Value").WithSpecs<Required>().WithSpecs<Updateable>());

            var argumentCollection = new Subject("Arguments")
                .WithSpecs<NamedCollection>()
                .WithChild(argument);
            var directiveAnnotation = new Subject("Directive Annotation")
                .WithChild(name)
                .WithChild(argumentCollection);

            var directiveAnnotations = new Subject("Directive Annotations")
                .WithSpecs<NamedCollection>()
                .WithChild(directiveAnnotation);

            var inputValue = new Subject("Input Value")
                .WithChild(description)
                .WithChild(inputTypeRef)
                .WithChild(new Subject("Default Value").WithSpecs<Optional>())
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var argumentDef = inputValue.WithName("Argument Definition");
            var argumentDefCollection = new Subject("Arguments Definition")
                .WithChild(argumentDef)
                .WithSpecs<NamedCollection>();

            var outputField = new Subject("Field")
                .WithChild(name)
                .WithChild(description)
                .WithChild(argumentDefCollection)
                .WithChild(directiveAnnotations)
                .WithChildren(outputTypeRef.WithName("Field Type"));

            var outputFields = new Subject("Fields")
                .WithSpecs<NamedCollection>();

            var implementsInterfaces = new Subject("Implements Interfaces").WithSpecs<NamedTypeSet>();

            var objectType = new Subject("Object Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(outputFields.WithChild(outputField.WithName("Object Field")))
                .WithChild(implementsInterfaces);

            var objects = new Subject("Objects")
                .WithSpecs<NamedCollection>()
                .WithChild(objectType);


            var scalar = new Subject("Scalar Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var scalars = new Subject("Scalars")
                .WithSpecs<NamedCollection>()
                .WithChild(scalar);

            var interfaceType = new Subject("Interface Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(outputFields.WithChild(outputField.WithName("Interface Field")))
                .WithChild(implementsInterfaces);

            var interfaces = new Subject("Interfaces")
                .WithSpecs<NamedCollection>()
                .WithChild(interfaceType);

            var unionType = new Subject("Union Type").WithChild(description).WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Union Member Types").WithSpecs<NamedTypeSet>());

            var unions = new Subject("Unions")
                .WithSpecs<NamedCollection>()
                .WithChild(unionType);

            var enumType = new Subject("Enum Type")
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(description)
                .WithChild(
                    new Subject("Enum Values")
                        .WithSpecs<NamedCollection>()
                        .WithChild(new Subject("Enum Value")
                            .WithChild(name)
                            .WithChild(directiveAnnotations)
                            .WithChild(description)));

            var enums = new Subject("Enums")
                .WithSpecs<NamedCollection>()
                .WithChild(enumType);

            var inputObjectType = new Subject("Input Object Type")
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject("Fields")
                    .WithSpecs<NamedCollection>()
                    .WithChild(inputValue.WithName("Input Field")));

            var inputObjects = new Subject("Input Objects")
                .WithSpecs<NamedCollection>()
                .WithChild(inputObjectType);

            var directive = new Subject("Directive")
                .WithChild(name)
                .WithChild(new Subject("Repeatable").WithSpecs<Optional>())
                .WithChild(new Subject("Locations").WithSpecs<NamedCollection>())
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

            var specs = Spec.GetSpecs(typeof(TypeSytemSpecs));
            return new SpecSuite("Type System", schema, specs, typeof(DirectiveCreationTests).Assembly);
        }
    }
}