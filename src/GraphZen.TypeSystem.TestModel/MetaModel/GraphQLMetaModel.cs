// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable AssignNullToNotNullAttribute

namespace GraphZen.MetaModel
{
    public static class GraphQLMetaModel
    {
        [NotNull]
        public static Vector EnumValue() => new Vector<IEnumValueDefinition, IMutableEnumValueDefinition, EnumValue, EnumValueDefinition>(nameof(EnumValue)) {
            Name(), Description(),
            //DirectiveAnnotations()
            // // new LeafElement("CustomValue", null) { Optional = true }
        }.SetConventions("ViaClrEnumValue");

        [NotNull]
        public static Vector EnumType() => new Vector<IEnumTypeDefinition, IMutableEnumTypeDefinition, EnumType, EnumTypeDefinition>(nameof(EnumType))
        {
            Name(), Description(),
            //DirectiveAnnotations(),
            new Collection<IEnumValuesContainerDefinition, IMutableEnumValuesContainerDefinition>("Values", EnumValue())
        }.SetConventions("ViaClrEnum");


        public static Vector ScalarType() => new Vector<IScalarTypeDefinition, IMutableScalarTypeDefinition, ScalarType, ScalarTypeDefinition>(nameof(ScalarType))
            {
                Name(), Description()
                // DirectiveAnnotations()
                //new LeafElement("ValueParser" ),
                //new LeafElement("LiteralParser", null),
                // new LeafElement("ClrType", null)
            }
            .SetConventions("ViaClrClass", "ViaClrInterface");


        [NotNull]
        public static Vector InterfaceType() =>
            new Vector<IInterfaceTypeDefinition, IMutableInterfaceTypeDefinition, InterfaceType, InterfaceTypeDefinition>(nameof(InterfaceType))
            {
                Name(), Description(),
                // DirectiveAnnotations(), 
                Fields()
            }.SetConventions("ViaClrInterface");

        [NotNull]
        public static Vector ObjectType() => new Vector<IObjectTypeDefinition, IMutableObjectTypeDefinition, ObjectType, ObjectTypeDefinition>(nameof(ObjectType))
        {
            Name(),
            Description(),
            Fields()
            // DirectiveAnnotations(), 
        };


        [NotNull]
        public static Vector UnionType() => new Vector<IUnionTypeDefinition, IMutableUnionTypeDefinition, UnionType, UnionTypeDefinition>(nameof(UnionType))
        {
            Name(),
            Description(),
            // DirectiveAnnotations(), // new LeafElement("ClrType", null),
            new Collection<IMemberTypesContainerDefinition, IMutableMemberTypesContainerDefinition>("MemberTypes", null)
        };

        [NotNull]
        public static Vector InputObjectType() =>
            new Vector<IInputObjectTypeDefinition, IMutableInputObjectTypeDefinition, InputObjectType, InputObjectTypeDefinition>(nameof(InputObjectType))
            {
                Name(), Description(),
                // DirectiveAnnotations(),
                new Collection<IInputFieldsContainerDefinition, IMutableInputFieldsContainerDefinition>("InputFields", InputField())
            }.SetConventions("ViaClrClass");

        [NotNull]
        public static LeafElement Name() => new LeafElement<INamed, IMutableNamed, string>(nameof(Name));

        [NotNull]
        public static LeafElement Description() => new LeafElement<IDescription, IMutableDescription, string>(
            nameof(Description)
        )
        {
            Optional = true
        };

        [NotNull]
        public static Vector Directive(bool includeDirectives) => new Vector<IDirectiveDefinition, IMutableDirectiveDefinition, Directive, DirectiveDefinition>(nameof(Directive))
        {
            Name(),
            Description(),
            Arguments(false, "ViaClrAttributeProperty", "ViaClrAttributeConstructorParameter")
                .SetConventions("ViaClrAttributeProperties", "ViaClrAttributeConstructorParameters")
        };

        //[NotNull]
        //public static Collection DirectiveAnnotations(bool includeDirectives = true) =>
        //    new Collection(nameof(DirectiveAnnotations), null).SetConventions("ViaClrAttributes");


        public static Collection Fields() =>
            new Collection<IFieldsContainerDefinition, IMutableFieldsContainerDefinition>(nameof(Fields), Field());


        [NotNull]
        public static Vector Schema() => new Vector<ISchemaDefinition, IMutableSchemaDefinition, Schema, SchemaDefinition>(nameof(Schema))
        {
            Description(),
            // DirectiveAnnotations(),
            new Collection<IDirectivesContainerDefinition, IMutableDirectivesContainerDefinition>("Directives", Directive(false)),
            new Collection<IObjectTypesContainerDefinition, IMutableObjectTypesContainerDefinition>("Objects", ObjectType()).SetConventions("ViaClrClasses"),
            new Collection<IInterfaceTypesContainerDefinition, IMutableInterfaceTypesContainerDefinition>("Interfaces", InterfaceType()),
            new Collection<IUnionTypesContainerDefinition, IMutableUnionTypesContainerDefinition>("Unions", UnionType()),
            new Collection<IScalarTypesContainerDefinition, IMutableScalarTypesContainerDefinition>("Scalars", ScalarType()),
            new Collection<IEnumTypesContainerDefinition, IMutableEnumTypesContainerDefinition>("Enums", EnumType()),
            new Collection<IInputObjectTypesContainerDefinition, IMutableInputObjectTypesContainerDefinition>("InputObjects", InputObjectType())
            // new LeafElement("QueryType", ),
            // new LeafElement("MutationType", null),
            // new LeafElement("SubscriptionType", null)
        };


        [NotNull]
        public static Vector Field() => new Vector<IFieldDefinition, IMutableFieldDefinition, Field, FieldDefinition>(nameof(Field))
        {
            Name(),
            Description(),
            Arguments(true, "ViaClrMethodParameter"),
            //DirectiveAnnotations()
        }.SetConventions("ViaClrMethod", "ViaClrProperty");

        [NotNull]
        private static Vector Argument(bool includeDirectives) => InputValue(nameof(Argument), includeDirectives);

        public static Collection Arguments(bool includeDirectives, params string[] argumentConventions) =>
            new Collection<IArgumentsContainerDefinition, IMutableArgumentsContainerDefinition>(nameof(Arguments), Argument(includeDirectives).SetConventions(argumentConventions));

        public static Vector InputField() => InputValue(nameof(InputField)).SetConventions("ViaClrProperty");

        [NotNull]
        public static Vector InputValue([NotNull] string name, bool includeDirectives = true)
        {
            var inputValue = new Vector<IInputValueDefinition, IMutableInputValueDefinition, InputValue, InputValueDefinition>(name)
            {
                Name(),
                Description()
            };
            if (includeDirectives)
            {
                // inputValue.Add(DirectiveAnnotations());
            }

            return inputValue;
        }
    }
}