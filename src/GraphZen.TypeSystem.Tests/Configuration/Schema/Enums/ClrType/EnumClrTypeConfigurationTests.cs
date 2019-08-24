#nullable disable
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using Xunit;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen.Enums.ClrType
{
    [NoReorder]
    public class EnumClrTypeConfigurationTests
    {
        public enum ExampleEnum
        {
        }

        [Fact]
        public void enum_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Enum(nameof(ExampleEnum));
                var def = sb.GetDefinition().GetEnum(nameof(ExampleEnum));
                sb.Enum<ExampleEnum>();
                def.ClrType.Should().Be<ExampleEnum>();
            });
            schema.GetEnum<ExampleEnum>().ClrType.Should().Be<ExampleEnum>();
        }

        [Fact]
        public void
            enum_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Enum(nameof(ExampleEnum));
                sb.Object("Parent").Field<ExampleEnum>("field");
                var def = sb.GetDefinition().GetEnum(nameof(ExampleEnum));
                sb.Enum<ExampleEnum>();
                def.ClrType.Should().Be<ExampleEnum>();
            });
            schema.GetEnum<ExampleEnum>().ClrType.Should().Be<ExampleEnum>();
        }
    }
}