// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable AssignNullToNotNullAttribute

namespace GraphZen.MetaModel
{
    public static class GraphQLMetaModel
    {
        [NotNull]
        public static Vector EnumValue() => new Vector(nameof(EnumValue))
        {
            Name(), Description(),
            DirectiveAnnotations()
            // // new LeafElement("CustomValue", null) { Optional = true }
        };

        [NotNull]
        public static Vector EnumType() => new Vector(nameof(EnumType))
        {
            Name(), Description(),
            DirectiveAnnotations(),
            new Collection("Values", EnumValue())
        };


        public static Vector ScalarType() => new Vector(nameof(ScalarType))
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
            new Vector(nameof(InterfaceType))
            {
                Name(), Description(),
                // DirectiveAnnotations(), 
                Fields()
            }.SetConventions("ViaClrInterface");

        [NotNull]
        public static Vector ObjectType(params string[] conventions) => new Vector(nameof(ObjectType))
        {
            Name(),
            Description(),
            Fields()
            // DirectiveAnnotations(), 
        }.SetConventions(conventions);


        [NotNull]
        public static Vector UnionType() => new Vector(nameof(UnionType))
        {
            Name(),
            Description(),
            // DirectiveAnnotations(), // new LeafElement("ClrType", null),
            new Collection("MemberTypes", null)
        };

        [NotNull]
        public static Vector InputObjectType() =>
            new Vector(nameof(InputObjectType))
            {
                Name(), Description(),
                // DirectiveAnnotations(),
                new Collection("InputFields", InputField())
            }.SetConventions("ViaClrClass");

        [NotNull]
        public static LeafElement Name() => new LeafElement<INamed, IMutableNamed>(nameof(Name));

        [NotNull]
        public static LeafElement Description() => new LeafElement<IDescription, IMutableDescription>(
            nameof(Description)
        );

        [NotNull]
        public static Vector Directive(bool includeDirectives) => new Vector(nameof(Directive))
        {
            Name(),
            Description(),
            Arguments(false, "ViaClrAttributeProperty", "ViaClrAttributeConstructorParameter")
                .SetConventions("ViaClrAttributeProperties", "ViaClrAttributeConstructorParameters")
        };

        [NotNull]
        public static Collection DirectiveAnnotations(bool includeDirectives = true) =>
            new Collection(nameof(DirectiveAnnotations), null).SetConventions("ViaClrAttributes");


        public static Collection Fields() =>
            new Collection(nameof(Fields), Field());


        [NotNull]
        public static Vector Schema() => new Vector(nameof(Schema))
        {
            Description(),
            // DirectiveAnnotations(),
            new Collection("Directives", Directive(false)),
            new Collection("Objects", ObjectType()).SetConventions("ViaClrClasses"),
            new Collection("Interfaces", InterfaceType()),
            new Collection("Unions", UnionType()),
            new Collection("Scalars", ScalarType()),
            new Collection("Enums", EnumType()),
            new Collection("InputObjects", InputObjectType())
            // new LeafElement("QueryType", ),
            // new LeafElement("MutationType", null),
            // new LeafElement("SubscriptionType", null)
        };


        [NotNull]
        public static Vector Field() => new Vector(nameof(Field))
        {
            Name(),
            Description(),
            Arguments(true, "ViaClrMethodParameter"),
            DirectiveAnnotations()
        }.SetConventions("ViaClrMethod", "ViaClrProperty");

        [NotNull]
        private static Vector Argument(bool includeDirectives) => InputValue(nameof(Argument), includeDirectives);

        public static Collection Arguments(bool includeDirectives, params string[] argumentConventions) =>
            new Collection(nameof(Arguments), Argument(includeDirectives).SetConventions(argumentConventions));

        public static Vector InputField() => InputValue(nameof(InputField)).SetConventions("ViaClrProperty");

        [NotNull]
        public static Vector InputValue([NotNull] string name, bool includeDirectives = true)
        {
            var inputValue = new Vector(name)
            {
                Name(),
                Description()
            };
            if (includeDirectives)
            {
                inputValue.Add(DirectiveAnnotations());
            }

            return inputValue;
        }
    }
}