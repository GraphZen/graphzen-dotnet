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
    public class InputObjectType_ViaClrClass__Description : InputObjectType_ViaClrClass__Description_Cases
    {
        public const string DataAnnotationDescription = nameof(DataAnnotationDescription);
        public override string DataAnnotationValue => DataAnnotationDescription;

        public override void ConfigureParent(SchemaBuilder sb, out string parentName,
            ConfigurationSource scenario)
        {
            switch (scenario)
            {
                case ConfigurationSource.DataAnnotation:
                    parentName = nameof(InputObjectDescribedByDataAnnotation);
                    sb.InputObject<InputObjectDescribedByDataAnnotation>();

                    break;
                case ConfigurationSource.Convention:
                    parentName = nameof(InputObject);
                    sb.InputObject<InputObject>();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scenario), scenario, null);
            }
        }

        public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(parentName).Description(null);
        }

        public class InputObject
        {
        }

        [Description(DataAnnotationDescription)]
        public class InputObjectDescribedByDataAnnotation
        {
        }
    }
}