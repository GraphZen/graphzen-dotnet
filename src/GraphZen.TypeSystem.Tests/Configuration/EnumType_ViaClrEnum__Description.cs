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
       
    }
}