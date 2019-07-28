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
    public class InterfaceType__Field__Argument_ViaClrMethodParameter__Description : InterfaceType__Field__Argument_ViaClrMethodParameter__Description_Cases
    {
        public override string GreatGrandparentName => nameof(TestInterface);
        public override string GrandparentName => nameof(TestInterface.Hello).FirstCharToLower();

        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);
        public override string DataAnnotationValue => DataAnnotationDescription;

        public interface TestInterface
        {
            string Hello(string arg1,
                [Description(DataAnnotationDescription)]
                string arg2);
        }

        public override void ConfigureParent(SchemaBuilder sb, out string parentName, ConfigurationSource scenario)
        {
            sb.Interface<TestInterface>();
            if (scenario == ConfigurationSource.Convention)
            {
                parentName = "arg1";
            }
            else if (scenario == ConfigurationSource.DataAnnotation)
            {
                parentName = "arg2";
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface<TestInterface>().Field(GrandparentName, fb => fb.Argument(parentName, ab => ab.Description(null)));
        }
    }
}
