using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.Configuration
{
    public abstract class ScalarNameTestsBase : LeafElementConfigurationTests
    {
        [Fact]
        public override void defined_by_convention()
        {
            base.defined_by_convention();
        }
    }
}