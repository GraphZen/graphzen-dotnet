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
    }
}
