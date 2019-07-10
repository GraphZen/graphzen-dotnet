using System.Collections.Generic;
using System.Collections.Immutable;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.MetaModel
{
    public static class GraphQLMetaModel
    {
        [NotNull]
        public static LeafElement Name { get; } = LeafElement.Create<INamed, IMutableNamed>(nameof(Name),
            new ConfigurationScenarios
            {
                Define = new[]
                    {ConfigurationSource.Convention, ConfigurationSource.DataAnnotation, ConfigurationSource.Explicit}
            });

        [NotNull]
        public static LeafElement Description { get; } = LeafElement.Create<IDescription, IMutableDescription>(
            nameof(Description), new ConfigurationScenarios
            {
                Define = new[] {ConfigurationSource.DataAnnotation, ConfigurationSource.Explicit},
                Remove = new[] {ConfigurationSource.Explicit}
            }, true);
        
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

        public static Vector ScalarType { get; } = new Vector(nameof(ScalarType))
        {
            Name, Description, DirectiveAnnotations,
            // new LeafElement("ValueParser", null),
            // new LeafElement("LiteralParser", null),
            // new LeafElement("ClrType", null)
        };

        [NotNull]
        public static Vector InterfaceType { get; } = new Vector(nameof(InterfaceType))
        {
            Name, Description, DirectiveAnnotations, Fields, 
           //  new LeafElement("ClrType", null)
        };

        [NotNull]
        public static Vector ObjectType { get; } = new Vector(nameof(ObjectType))
        {
            Name, Description, DirectiveAnnotations, Fields,
            new Collection("Interfaces", InterfaceType),
            // new LeafElement("ClrType", null)
        };


        [NotNull]
        public static Vector UnionType { get; } = new Vector(nameof(UnionType))
        {
            Name, Description, DirectiveAnnotations, // new LeafElement("ClrType", null),
            new Collection("MemberTypes", ObjectType)
        };

        [NotNull]
        public static Vector InputObjectType { get; } = new Vector(nameof(InputObjectType))
        {
            Name, Description, DirectiveAnnotations, new Collection("InputFields", InputValue("InputField"))
        };

        [NotNull]
        public static Vector EnumValue { get; } = new Vector(nameof(EnumValue))
        {
            Name, Description, DirectiveAnnotations,// new LeafElement("CustomValue", null) { Optional = true }
        };

        [NotNull]
        public static Vector EnumType { get; } = new Vector(nameof(EnumType))
        {
            Name, Description, DirectiveAnnotations, new Collection("Values", EnumValue)
        };


        [NotNull]
        public static Vector Schema { get; } = new Vector(nameof(Schema))
        {
            DirectiveAnnotations,
            new Collection("Directives", Directive),
            new Collection("Objects", ObjectType),
            new Collection("Interfaces", InterfaceType),
            new Collection("Unions", UnionType),
            new Collection("Scalars", ScalarType),
            new Collection("Enums", EnumType),
            new Collection("InputObjects", InputObjectType),
            // new LeafElement("QueryType", null),
            // new LeafElement("MutationType", null),
            // new LeafElement("SubscriptionType", null)
        };

        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<Element> Elements { get; } =
            ImmutableArray.Create(Schema, Directive, Argument(), EnumValue, EnumType, ScalarType, UnionType, InputObjectType, ObjectType,
                InterfaceType, Field(), InputField());


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
            Name, Description, DirectiveAnnotations, 
            /*new LeafElement("DefaultValue", null)
            {
                Optional = true
            }*/
        };
    }
}