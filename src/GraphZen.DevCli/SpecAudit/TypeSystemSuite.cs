// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.FunctionalTests.Directives;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.SpecAudit
{
    public static class TypeSystemSuite
    {
        private static readonly Lazy<SpecSuite> SpecSuite = new Lazy<SpecSuite>(Create);

        public static SpecSuite Get() => SpecSuite.Value;

        private static SpecSuite Create()
        {
            var name = new Subject(nameof(INamed.Name))
                .WithSpecs<UpdateableSpecs>()
                .WithSpecs<RequiredSpecs>();

            var description = new Subject(nameof(IDescription.Description))
                .WithSpecs<UpdateableSpecs>()
                .WithSpecs<OptionalSpecs>();

            var typeRef = new Subject("Type")
                .WithSpecs<RequiredSpecs>()
                .WithSpecs<UpdateableSpecs>();

            var inputTypeRef = typeRef.WithName("InputTypeRef");
            var outputTypeRef = typeRef.WithName("OutputTypeRef");

            var argument = new Subject("Argument").WithChild(name)
                .WithChild(new Subject("Value"));

            var argumentCollection = new Subject("Arguments");
            // .WithSpecs<NamedCollectionSpecs>();
            //.WithChild(argument);
            var directiveAnnotation = new Subject(nameof(DirectiveAnnotation))
                .WithChild(name)
                .WithChild(argumentCollection);


            var directiveAnnotations = new Subject(nameof(AnnotatableMemberDefinition.DirectiveAnnotations))
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(directiveAnnotation);

            var inputValue = new Subject(nameof(InputValue))
                .WithChild(description)
                .WithChild(inputTypeRef)
                .WithChild(new Subject(nameof(InputValue.DefaultValue)).WithSpecs<OptionalSpecs>())
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var argumentDef = inputValue.WithName(nameof(ArgumentDefinition));
            var argumentDefCollection = new Subject(nameof(IArguments.Arguments))
                .WithChild(argumentDef)
                .WithSpecs<NamedCollectionSpecs>();

            var outputField = new Subject(nameof(Field))
                .WithChild(name)
                .WithChild(description)
                .WithChild(argumentDefCollection)
                .WithChild(directiveAnnotations)
                .WithChildren(outputTypeRef.WithName(nameof(Field.FieldType)));

            var outputFields = new Subject(nameof(FieldsDefinition.Fields))
                .WithSpecs<NamedCollectionSpecs>();

            var implementsInterfaces = new Subject(nameof(ObjectType.Interfaces))
                .WithSpecs<NamedTypeSetSpecs>();

            var objectType = new Subject(nameof(ObjectType))
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(outputFields.WithChild(outputField))
                .WithChild(implementsInterfaces);

            var objects = new Subject(nameof(Schema.Objects))
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(objectType);


            var scalar = new Subject(nameof(ScalarType))
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var scalars = new Subject(nameof(Schema.Scalars))
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(scalar);

            var interfaceType = new Subject(nameof(InterfaceType))
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(outputFields.WithChild(outputField))
                .WithChild(implementsInterfaces);

            var interfaces = new Subject(nameof(Schema.Interfaces))
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(interfaceType);

            var unionType = new Subject(nameof(UnionType)).WithChild(description).WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject(nameof(UnionType.MemberTypes)).WithSpecs<NamedTypeSetSpecs>());

            var unions = new Subject(nameof(Schema.Unions))
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(unionType);

            var enumType = new Subject(nameof(EnumType))
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(description)
                .WithChild(
                    new Subject(nameof(EnumType.Values))
                        .WithSpecs<NamedCollectionSpecs>()
                        .WithChild(new Subject(nameof(EnumValue))
                            .WithChild(name)
                            .WithChild(directiveAnnotations)
                            .WithChild(description)));

            var enums = new Subject(nameof(Schema.Enums))
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(enumType);

            var inputObjectType = new Subject(nameof(InputObjectType))
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject(nameof(InputObjectType.Fields))
                    .WithSpecs<NamedCollectionSpecs>()
                    .WithChild(inputValue.WithName(nameof(InputField))));

            var inputObjects = new Subject(nameof(Schema.InputObjects))
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(inputObjectType);

            var directive = new Subject(nameof(Directive))
                .WithChild(name)
                .WithChild(new Subject("Repeatable").WithSpecs<OptionalSpecs>())
                .WithChild(new Subject(nameof(Directive.Locations)))
                .WithChild(description);

            var directives = new Subject(nameof(Schema.Directives))
                .WithSpecs<NamedCollectionSpecs>()
                .WithSpecs<DirectivesSpecs>()
                .WithChild(directive);

            var schemaBuilder = new Subject(nameof(SchemaBuilder))
                .WithChild(description)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject(nameof(Schema.QueryType)))
                .WithChild(new Subject(nameof(Schema.MutationType)))
                .WithChild(new Subject(nameof(Schema.SubscriptionType)))
                .WithChild(directives.WithSpecPriority(SpecPriority.High, true))
                .WithChild(scalars)
                .WithChild(objects)
                .WithChild(interfaces)
                .WithChild(unions)
                .WithChild(enums)
                .WithChild(inputObjects);

            var specs = Spec.GetSpecs(typeof(TypeSystemSpecs));
            return new SpecSuite("Type System", schemaBuilder, specs, typeof(DirectiveCreationTests).Assembly);
        }
    }
}