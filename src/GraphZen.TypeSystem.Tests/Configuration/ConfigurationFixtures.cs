// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Interfaces.Fields;
using GraphZen.Objects.Fields;
using GraphZen.Objects.Fields.Arguments;
using Xunit;

namespace GraphZen
{
    public static class ConfigurationFixtures
    {
        [NotNull]
        [ItemNotNull]
        public static IEnumerable<T> GetAll<T>() where T : IConfigurationFixture => new List<IConfigurationFixture>
        {
            new ObjectFields_Explicit(),
            new ObjectFields_ViaClrProperties(),
            new ObjectField_Arguments_Explicit(),
            new InterfaceFields_Explicit(),
        }.OfType<T>();



    }

    public class ConfigurationFixturesTests
    {
        [Fact]
        public void ensure_configuration_fixtures_implement_a_marker_interface()
        {
            foreach (var fixture in ConfigurationFixtures.GetAll<IConfigurationFixture>())
            {
                if (fixture is ICollectionConfigurationFixture)
                {
                    if (fixture is IConventionalCollectionConfigurationFixture ||
                        fixture is IExplicitCollectionConfigurationFixture)
                    {

                    }
                    else
                    {
                        throw new Exception($"{fixture.GetType().Name} needs to implement either {typeof(IConventionalCollectionConfigurationFixture).Name} or {typeof(IExplicitCollectionConfigurationFixture).Name}");
                    }

                }

            }

        }
    }
}