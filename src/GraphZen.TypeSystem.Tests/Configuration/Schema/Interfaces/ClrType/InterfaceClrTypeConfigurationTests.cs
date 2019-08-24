using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using Xunit;
// ReSharper disable PossibleNullReferenceException

namespace GraphZen.Interfaces.ClrType
{
    [NoReorder]
    public class InterfaceClrTypeConfigurationTests
    {
        public interface IExampleInterface { }
        [Fact]
        public void interface_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Interface(nameof(IExampleInterface));
                var def = sb.GetDefinition().GetInterface(nameof(IExampleInterface));
                sb.Interface<IExampleInterface>();
                def.ClrType.Should().Be<IExampleInterface>();
            });
            schema.GetInterface<IExampleInterface>().ClrType.Should().Be<IExampleInterface>();
        }

        [Fact]
        public void interface_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Interface(nameof(IExampleInterface));
                sb.Object("Object").Field<IExampleInterface>("field");
                var def = sb.GetDefinition().GetInterface(nameof(IExampleInterface));
                sb.Interface<IExampleInterface>();
                def.ClrType.Should().Be<IExampleInterface>();
            });
            schema.GetInterface<IExampleInterface>().ClrType.Should().Be<IExampleInterface>();
        }
    }
}
