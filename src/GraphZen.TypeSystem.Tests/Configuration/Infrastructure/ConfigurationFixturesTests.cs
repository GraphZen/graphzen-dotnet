using System;
using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen
{
    public class ConfigurationFixturesTests
    {
        [Fact]
        public void ensure_configuration_fixtures_implement_a_marker_interface()
        {
            foreach (var fixture in ConfigurationFixtures.GetAll<IConfigurationFixture>())
            {
                switch (fixture)
                {
                    case ICollectionConfigurationFixture _ when !(fixture is ICollectionConventionConfigurationFixture) &&
                                                                !(fixture is ICollectionExplicitConfigurationFixture):
                        throw new Exception(
                            $"{fixture.GetType().Name} needs to implement either {typeof(ICollectionConventionConfigurationFixture).Name} or {typeof(ICollectionExplicitConfigurationFixture).Name}");
                    case ILeafConfigurationFixture _ when !(fixture is ILeafConventionConfigurationFixture) &&
                                                          !(fixture is ILeafExplicitConfigurationFixture):
                        throw new Exception(
                            $"{fixture.GetType().Name} needs to implement either {typeof(ILeafConventionConfigurationFixture).Name} or {typeof(ILeafExplicitConfigurationFixture).Name}");
                }
            }
        }
    }
}