using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen
{
    [NoReorder]
    public class LeafConventionConfigurationTests : FixtureRunner<ILeafConventionConfigurationFixture>
    {
        protected override IEnumerable<ILeafConventionConfigurationFixture> GetFixtures() =>
            ConfigurationFixtures.GetAll<ILeafConventionConfigurationFixture>();
    }
}