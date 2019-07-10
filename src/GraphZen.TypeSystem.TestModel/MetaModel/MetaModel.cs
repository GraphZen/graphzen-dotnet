using System.Collections.Generic;
using System.Collections.Immutable;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.MetaModel
{
    public static class GraphQLMetaModel
    {
        [NotNull]
        public static LeafElement Name { get; } = new LeafElement(nameof(Name), new ConfigurationScenarios
        {
            Define = new[]
                {ConfigurationSource.Convention, ConfigurationSource.DataAnnotation, ConfigurationSource.Explicit}
        });


        [NotNull]
        public static LeafElement Description { get; } = new LeafElement(nameof(Description), new ConfigurationScenarios
        {
            Define = new[] { ConfigurationSource.DataAnnotation, ConfigurationSource.Explicit },
            Remove = new[] { ConfigurationSource.Explicit }
        })
        {
            Optional = true
        };

        [NotNull]
        public static Vector Directive { get; } = new Vector(nameof(Directive))
        {
            Name, Description, Arguments()
        };

        [NotNull]
        public static Collection DirectiveAnnotations { get; } =
            new Collection(nameof(DirectiveAnnotations), Directive);

        [NotNull]
        public static Collection Fields { get; } =
            new Collection(nameof(Fields), Field());

        [NotNull]

        public static Vector Scalar { get; } = new Vector(nameof(Scalar))
        {
            Name, Description, DirectiveAnnotations,
            new LeafElement("ValueParser", null),
            new LeafElement("LiteralParser", null),
            new LeafElement("ClrType", null)
        };

        [NotNull]
        public static Vector Interface { get; } = new Vector(nameof(Interface))
        {
            Name, Description, DirectiveAnnotations, Fields, new LeafElement("ClrType", null)
        };

        [NotNull]
        public static Vector Object { get; } = new Vector(nameof(Object))
        {
            Name, Description, DirectiveAnnotations, Fields,
            new Collection("Interfaces", Interface), new LeafElement("ClrType", null)
        };


        [NotNull]
        public static Vector Union { get; } = new Vector(nameof(Union))
        {
            Name, Description, DirectiveAnnotations, new LeafElement("ClrType", null),
            new Collection("MemberTypes", Object)
        };

        [NotNull]
        public static Vector InputObject { get; } = new Vector(nameof(InputObject))
        {
            Name, Description, DirectiveAnnotations, new Collection("InputFields", InputValue("InputField"))
        };

        [NotNull]
        public static Vector EnumValue { get; } = new Vector(nameof(EnumValue))
        {
            Name, Description, DirectiveAnnotations, new LeafElement("CustomValue", null)
            {
                Optional = true
            }
        };

        [NotNull]
        public static Vector Enum { get; } = new Vector(nameof(Enum))
        {
            Name, Description, DirectiveAnnotations, new Collection("Values", EnumValue)
        };


        [NotNull]
        public static Vector Schema { get; } = new Vector(nameof(Schema))
        {
            DirectiveAnnotations,
            new Collection("Directives", Directive),
            new Collection("Objects", Object),
            new Collection("Interfaces", Interface),
            new Collection("Unions", Union),
            new Collection("Scalars", Scalar),
            new Collection("Enums", Enum),
            new Collection("InputObjects", InputObject),
            new LeafElement("QueryType", null),
            new LeafElement("MutationType", null),
            new LeafElement("SubscriptionType", null)
        };

        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<Element> Elements { get; } =
            ImmutableArray.Create(Schema, Directive, Argument(), EnumValue, Enum, Scalar, Union, InputObject, Object,
                Interface, Field(), InputField());


        [NotNull]
        public static Collection Arguments() =>
            new Collection(nameof(Arguments), Argument());

        [NotNull]
        public static Vector Field() => new Vector(nameof(Field))
        {
            Name, Description, DirectiveAnnotations
        };

        [NotNull]
        public static Vector Argument() => InputValue(nameof(Argument));

        public static Vector InputField() => InputValue(nameof(InputField));

        [NotNull]
        public static Vector InputValue([NotNull] string name) => new Vector(name)
        {
            Name, Description, DirectiveAnnotations, new LeafElement("DefaultValue", null)
            {
                Optional = true
            }
        };
    }
}