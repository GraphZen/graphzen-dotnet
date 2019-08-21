using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableClrType : IClrType
    {
        bool SetClrType(Type clrType, ConfigurationSource configurationSource);
        ConfigurationSource GetClrTypeConfigurationSource();
    }
}