// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration.Interfaces.ClrType;

[NoReorder]
public class InterfaceClrTypeConfigurationTests
{
    public interface IExampleInterface
    {
    }

    [Fact]
    public void interface_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
    {
        var schema = Schema.Create(sb =>
        {
            sb.Interface(nameof(IExampleInterface));
            var def = sb.GetDefinition().GetInterface(nameof(IExampleInterface));
            sb.Interface<IExampleInterface>();
            Assert.Equal(typeof(IExampleInterface), def.ClrType);
        });
        Assert.Equal(typeof(IExampleInterface), schema.GetInterface<IExampleInterface>().ClrType);
    }

    [Fact]
    public void
        interface_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
    {
        var schema = Schema.Create(sb =>
        {
            sb.Interface(nameof(IExampleInterface));
            sb.Object("Object").Field<IExampleInterface>("field");
            var def = sb.GetDefinition().GetInterface(nameof(IExampleInterface));
            sb.Interface<IExampleInterface>();
            Assert.Equal(typeof(IExampleInterface), def.ClrType);
        });
        Assert.Equal(typeof(IExampleInterface), schema.GetInterface<IExampleInterface>().ClrType);
    }
}