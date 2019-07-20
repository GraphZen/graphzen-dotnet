using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IClrInfo
    {
        [CanBeNull]
        object ClrInfo { get; }
    }
}