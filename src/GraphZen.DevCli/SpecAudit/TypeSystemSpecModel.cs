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
            var name = new Subject(nameof(INamed.Name)).WithSpecs<NameSpecs>();

            var description = new Subject(nameof(IDescription.Description))
                .WithSpecs<DescriptionSpecs>()
                .WithSpecs<SdlSpec>();

            var typeRef = new Subject("Type")
                    .WithSpecs<TypeReferenceSpecs>()
                    .WithSpecs<SdlSpec>();

            var member = new Subject(nameof(Member)).WithSpecs<MemberSpecs>();

            var inputTypeRef = typeRef.WithName("InputTypeRef");
            var outputTypeRef = typeRef.WithName("OutputTypeRef");


            // ReSharper disable once UnusedVariable
            var directiveAnnotation = new Subject(nameof(DirectiveAnnotation));

            var directiveAnnotations = new Subject(nameof(AnnotatableMemberDefinition.DirectiveAnnotations))
                .WithSpecs<DirectiveAnnotationSpecs>(SpecPriority.Medium);

            var argument = new Subject("Argument")
                                        .WithChild(name)
                                        //.WithSpecs<SdlSpec, SdlExtensionSpec>()
                                        .WithChild(new Subject("Value"));

            // ReSharper disable once UnusedVariable
            var argumentCollection = new Subject("Arguments")
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(argument);


            var inputValue = member.WithName(nameof(InputValue))
                .WithSpecs<SdlSpec, SdlExtensionSpec>()
                .WithChild(description)
                .WithChild(new Subject(nameof(InputValue.DefaultValue)))
                .WithChild(name)
                .WithChild(directiveAnnotations);



            var argumentDef = inputValue.WithName(nameof(ArgumentDefinition))
                .WithChild(inputTypeRef.WithName(nameof(Argument.ArgumentType)));

            var argumentDefCollection = new Subject(nameof(IArguments.Arguments))
                .WithChild(argumentDef)
                .WithSpecs<NamedCollectionSpecs>();

            var outputField = member.WithName(nameof(Field))
                .WithSpecs<SdlSpec, SdlExtensionSpec>()
                .WithChild(name)
                .WithChild(description)
                .WithChild(argumentDefCollection)
                .WithChild(directiveAnnotations)
                .WithChildren(outputTypeRef.WithName(nameof(Field.FieldType)));

            var outputFields = new Subject(nameof(FieldsDefinition.Fields))
                .WithSpecs<NamedCollectionSpecs>();

            var implementsInterfaces = new Subject(nameof(ObjectType.Interfaces));

            var clrType = new Subject(nameof(IClrType.ClrType))
                .WithSpecs<ClrTypeSpecs>();

            var graphQLType = member.WithName(nameof(NamedType))
                .WithSpecs<SdlSpec, SdlExtensionSpec>()
                .WithChild(name)
                .WithChild(description)
                .WithChild(clrType)
                .WithChild(directiveAnnotations);

            var graphQLTypes = new Subject("types")
                .WithSpecs<NamedCollectionSpecs>()
                .WithSpecs<ClrTypedCollectionSpecs>();

            var objectType = graphQLType.WithName(nameof(ObjectType))
                .WithChild(outputFields.WithChild(outputField))
                .WithChild(implementsInterfaces);

            var objects = graphQLTypes.WithName(nameof(Schema.Objects))
                .WithSpecs<InputXorOutputTypeSpecs>()
                .WithChild(objectType);

            var scalar = graphQLType.WithName(nameof(ScalarType));

            var scalars = graphQLTypes.WithName(nameof(Schema.Scalars))
                .WithSpecs<InputAndOutputTypeSpecs>()
                .WithChild(scalar);

            var interfaceType = graphQLType.WithName(nameof(InterfaceType))
                .WithChild(outputFields.WithChild(outputField))
                .WithChild(implementsInterfaces);

            var interfaces = graphQLTypes.WithName(nameof(Schema.Interfaces))
                .WithSpecs<InputXorOutputTypeSpecs>()
                .WithChild(interfaceType);

            var unionType = graphQLType.WithName(nameof(UnionType))
                .WithChild(new Subject(nameof(UnionType.MemberTypes)));

            var unions = graphQLTypes.WithName(nameof(Schema.Unions))
                .WithSpecs<InputXorOutputTypeSpecs>()
                .WithChild(unionType);


            var enumValue = new Subject(nameof(EnumValue))
                .WithSpecs<SdlSpec, SdlExtensionSpec>()
                .WithChild(name)
                .WithChild(directiveAnnotations)
                .WithChild(description);

            var enumValues = new Subject(nameof(EnumType.Values))
                .WithSpecs<NamedCollectionSpecs>()
                .WithChild(enumValue);


            var enumType = graphQLType.WithName(nameof(EnumType))
                .WithoutSpecsDeep(nameof(ClrTypeSpecs.setting_clr_type_and_inferring_name_name_should_be_valid))
                .WithChild(enumValues);

            var enums = graphQLTypes.WithName(nameof(Schema.Enums))
                .WithSpecs<InputAndOutputTypeSpecs>()
                .WithChild(enumType);


            var inputField = inputValue.WithName(nameof(InputField))
                .WithChild(inputTypeRef.WithName(nameof(InputField.FieldType)));
            var inputObjectType = graphQLType.WithName(nameof(InputObjectType))
                .WithChild(new Subject(nameof(InputObjectType.Fields))
                    .WithSpecs<NamedCollectionSpecs>()
                    .WithChild(inputField));

            var inputObjects = graphQLTypes.WithName(nameof(Schema.InputObjects))
                .WithSpecs<InputXorOutputTypeSpecs>()
                .WithChild(inputObjectType);

            var directive = member.WithName(nameof(Directive))
                .WithChild(name)
                .WithChild(clrType)
                .WithChild(argumentDefCollection)
                .WithChild(new Subject(nameof(Directive.IsRepeatable)).WithSpecs<DirectiveRepeatableSpecs, SdlSpec>())
                .WithChild(new Subject(nameof(Directive.Locations)).WithSpecs<DirectiveLocationsSpecs, SdlSpec>())
                .WithChild(description)
                .WithSpecs<SdlSpec>()
                .WithoutSpecs<SdlExtensionSpec>(true);

            var directives = new Subject(nameof(Schema.Directives))
                .WithSpecs<NamedCollectionSpecs>()
                .WithSpecs<ClrTypedCollectionSpecs>()
                .WithChild(directive);

            var schemaBuilder = member.WithName("Schema_")
                .WithChild(description)
                .WithChild(directiveAnnotations)
                .WithChild(new Subject(nameof(Schema.QueryType)))
                .WithChild(new Subject(nameof(Schema.MutationType)))
                .WithChild(new Subject(nameof(Schema.SubscriptionType)))
                .WithChild(directives)
                .WithChild(inputObjects)
                .WithChild(scalars)
                .WithChild(unions)
                .WithChild(objects)
                .WithChild(interfaces)
                .WithChild(enums);

            return new SpecSuite(nameof(TypeSystem), schemaBuilder, typeof(TypeSystemSpecs));
        }
    }
}