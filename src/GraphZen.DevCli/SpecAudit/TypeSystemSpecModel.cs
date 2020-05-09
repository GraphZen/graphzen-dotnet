// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.SpecAudit.SpecFx;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.SpecAudit
{
    public static class TypeSystemSpecModel
    {
        private static readonly Lazy<SpecSuite> SpecSuite = new Lazy<SpecSuite>(Create);

        public static SpecSuite Get() => SpecSuite.Value;

        private static SpecSuite Create()
        {
            var name = new Subject(nameof(INamed.Name))
                .WithSpecs<SdlSpec>()
                .WithSpecs<UpdateableSpecs>()
                .WithSpecs<RequiredSpecs>();

            var description = new Subject(nameof(IDescription.Description))
                .WithSpecs<SdlSpec>()
                .WithSpecs<UpdateableSpecs>()
                .WithSpecs<OptionalSpecs>();

            var typeRef = new Subject("Type")
                .WithSpecs<SdlSpec>()
                .WithSpecs<RequiredSpecs>()
                .WithSpecs<UpdateableSpecs>();

            var inputTypeRef = typeRef.WithName("InputTypeRef");
            var outputTypeRef = typeRef.WithName("OutputTypeRef");

            var argument = new Subject("Argument")
                .WithSpecs<SdlSpec>()
                .WithChild(name)
                .WithChild(new Subject("Value"));

            // ReSharper disable once UnusedVariable
            var argumentCollection = new Subject("Arguments")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(argument);
            var directiveAnnotation = new Subject(nameof(DirectiveAnnotation))
                .WithChild(name);
            // .WithChild(argumentCollection);


            var directiveAnnotations = new Subject(nameof(AnnotatableMemberDefinition.DirectiveAnnotations))
                .WithSpecs<DirectiveAnnotationSpecs>()
                .WithChild(directiveAnnotation);

            var inputValue = new Subject(nameof(InputValue))
                .WithChild(description)
                .WithChild(inputTypeRef)
                .WithChild(new Subject(nameof(InputValue.DefaultValue))
                    .WithSpecs<SdlSpec>()
                    .WithSpecs<OptionalSpecs>())
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
                    .WithChildren(outputTypeRef.WithName(nameof(Field.FieldType)))
                ;

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
                .WithSpecs<ClrTypedCollectionSpecs>()
                .WithSpecs<UniquelyInputOutputTypeCollectionSpecs>()
                .WithChild(objectType);

            var scalar = new Subject(nameof(ScalarType))
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations);

            var scalars = new Subject(nameof(Schema.Scalars))
                .WithSpecs<NamedCollectionSpecs>()
                .WithSpecs<ClrTypedCollectionSpecs>()
                .WithSpecs<InputAndOutputTypeCollectionSpecs>()
                .WithChild(scalar);

            var interfaceType = new Subject(nameof(InterfaceType))
                .WithChild(description)
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(outputFields.WithChild(outputField))
                .WithChild(implementsInterfaces);

            var interfaces = new Subject(nameof(Schema.Interfaces))
                .WithSpecs<NamedCollectionSpecs>()
                .WithSpecs<ClrTypedCollectionSpecs>()
                .WithSpecs<UniquelyInputOutputTypeCollectionSpecs>()
                .WithChild(interfaceType);

            var unionType = new Subject(nameof(UnionType)).WithChild(description).WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject(nameof(UnionType.MemberTypes))
                    .WithSpecs<SdlSpec>()
                    .WithSpecs<NamedTypeSetSpecs>());

            var unions = new Subject(nameof(Schema.Unions))
                .WithSpecs<NamedCollectionSpecs>()
                .WithSpecs<ClrTypedCollectionSpecs>()
                .WithSpecs<UniquelyInputOutputTypeCollectionSpecs>()
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
                .WithSpecs<ClrTypedCollectionSpecs>()
                .WithSpecs<InputAndOutputTypeCollectionSpecs>()
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
                .WithSpecs<ClrTypedCollectionSpecs>()
                .WithSpecs<UniquelyInputOutputTypeCollectionSpecs>()
                .WithChild(inputObjectType);

            var directive = new Subject(nameof(Directive))
                .WithChild(name)
                .WithChild(new Subject("Repeatable")
                    .WithSpecs<UpdateableSpecs>()
                    .WithSpecs<SdlSpec>()
                    .WithSpecs<OptionalSpecs>())
                .WithChild(new Subject(nameof(Directive.Locations)))
                .WithChild(description);

            var directives = new Subject(nameof(Schema.Directives))
                .WithSpecs<NamedCollectionSpecs>()
                .WithSpecs<ClrTypedCollectionSpecs>()
                .WithChild(directive);

            var schemaBuilder = new Subject(nameof(SchemaBuilder))
                .WithChild(description)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject(nameof(Schema.QueryType))
                    .WithSpecs<SdlSpec>().WithSpecs<UpdateableSpecs>().WithSpecs<OptionalSpecs>()
                )
                .WithChild(new Subject(nameof(Schema.MutationType))
                    .WithSpecs<SdlSpec>().WithSpecs<UpdateableSpecs>().WithSpecs<OptionalSpecs>()
                )
                .WithChild(new Subject(nameof(Schema.SubscriptionType))
                    .WithSpecs<SdlSpec>().WithSpecs<UpdateableSpecs>().WithSpecs<OptionalSpecs>()
                )
                .WithChild(directives.WithSpecPriority(SpecPriority.High, true))
                .WithChild(inputObjects)
                .WithChild(scalars)
                .WithChild(unions)
                .WithChild(objects.WithSpecPriority(SpecPriority.High, true))
                .WithChild(interfaces)
                .WithChild(enums);


            return new SpecSuite(nameof(TypeSystem), schemaBuilder, typeof(TypeSystemSpecs));
        }
    }
}