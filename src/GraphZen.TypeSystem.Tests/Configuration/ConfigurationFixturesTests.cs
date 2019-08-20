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
                if (fixture is ICollectionConfigurationFixture)
                {
                    if (fixture is ICollectionConventionConfigurationFixture ||
                        fixture is ICollectionExplicitConfigurationFixture)
                    {

                    }
                    else
                    {
                        throw new Exception($"{fixture.GetType().Name} needs to implement either {typeof(ICollectionConventionConfigurationFixture).Name} or {typeof(ICollectionExplicitConfigurationFixture).Name}");
                    }

                }

            }

        }
    }
}