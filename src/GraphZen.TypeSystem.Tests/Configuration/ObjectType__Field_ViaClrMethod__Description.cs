// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using System.ComponentModel;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration
{
    public class ObjectType__Field_ViaClrMethod__Description : ObjectType__Field_ViaClrMethod__Description_Cases
    {
        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);
        public override string DataAnnotationValue => DataAnnotationDescription;

        public override string GrandparentName => nameof(ObjectGqlType);

        public class ObjectGqlType
        {
            public string Hello() => nameof(Hello);
            [Description(DataAnnotationDescription)]
            public string HelloWithDescription() => nameof(HelloWithDescription);
        }

        public override void ConfigureParent(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            sb.Object<ObjectGqlType>();
            switch (scenario)
            {
                case ConfigurationSource.DataAnnotation:
                    parentName = nameof(ObjectGqlType.HelloWithDescription).FirstCharToLower();
                    break;
                case ConfigurationSource.Convention:
                    parentName = nameof(ObjectGqlType.Hello).FirstCharToLower();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scenario), scenario, null);
            }
        }

        public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Object(GrandparentName).Field(parentName, fb => fb.Description(null));
        }
    }
}
