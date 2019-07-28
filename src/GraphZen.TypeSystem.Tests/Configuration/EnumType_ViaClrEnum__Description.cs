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
    public class EnumType_ViaClrEnum__Description : EnumType_ViaClrEnum__Description_Cases
    {
        public enum EnumNoDescription
        {
        }

        [Description(DataAnnotationDescription)]
        public enum ExampleEnum
        {
        }

        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);
        public override string DataAnnotationValue => DataAnnotationDescription;

        public override void ConfigureParent(SchemaBuilder sb, out string parentName,
            ConfigurationSource scenario)
        {
            if (scenario == ConfigurationSource.DataAnnotation)
            {
                sb.Enum<ExampleEnum>();
                parentName = nameof(ExampleEnum);
            }
            else if (scenario == ConfigurationSource.Convention)
            {
                sb.Enum<EnumNoDescription>();
                parentName = nameof(EnumNoDescription);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Enum(parentName).Description(null);
        }
    }
}