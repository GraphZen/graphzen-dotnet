using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace GraphZen.Scalars.ClrType
{
    [NoReorder]
    public class ScalarClrTypeConfigurationTests
    {
        public class ExampleScalar { }
        [Fact]
        public void scalar_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Scalar(nameof(ExampleScalar));
                var def = sb.GetDefinition().GetScalar(nameof(ExampleScalar));
                sb.Scalar<ExampleScalar>();
                def.ClrType.Should().Be<ExampleScalar>();
            });
            schema.GetScalar<ExampleScalar>().ClrType.Should().Be<ExampleScalar>();
        }

        [Fact]
        public void scalar_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Scalar(nameof(ExampleScalar));
                sb.Object("Parent").Field<ExampleScalar>("field");
                var def = sb.GetDefinition().GetScalar(nameof(ExampleScalar));
                sb.Scalar<ExampleScalar>();
                def.ClrType.Should().Be<ExampleScalar>();
            });
            schema.GetScalar<ExampleScalar>().ClrType.Should().Be<ExampleScalar>();
        }
    }
}
