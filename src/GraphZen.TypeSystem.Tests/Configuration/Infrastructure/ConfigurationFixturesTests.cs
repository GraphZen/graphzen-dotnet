// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen
{
    public class ConfigurationFixturesTests
    {
        [Fact]
        public void ensure_configuration_fixtures_implement_a_marker_interface()
        {
            foreach (var fixture in ConfigurationFixtures.GetAll<IConfigurationFixture>())
                switch (fixture)
                {
                    case ICollectionConfigurationFixture _
                        when !(fixture is ICollectionConventionConfigurationFixture) &&
                             !(fixture is ICollectionExplicitConfigurationFixture):
                        throw new Exception(
                            $"{fixture.GetType().Name} needs to implement either {typeof(ICollectionConventionConfigurationFixture).Name} or {typeof(ICollectionExplicitConfigurationFixture).Name}");
                    case ILeafConfigurationFixture _ when !(fixture is ILeafConventionConfigurationFixture) &&
                                                          !(fixture is ILeafExplicitConfigurationFixture):
                        throw new Exception(
                            $"{fixture.GetType().Name} needs to implement either {typeof(ILeafConventionConfigurationFixture).Name} or {typeof(ILeafExplicitConfigurationFixture).Name}");
                }
        }

        [Fact]
        public void ensure_all_known_fixtures_are_included()
        {
            var type = typeof(IConfigurationFixture);
            var configuredTypes = ConfigurationFixtures.GetAll<IConfigurationFixture>().Select(_ => _.GetType())
                .ToArray();
            var knownTypes = AppDomain.CurrentDomain.GetAssemblies()
                // ReSharper disable once PossibleNullReferenceException
                .SelectMany(_ => _.GetTypes())
                // ReSharper disable once PossibleNullReferenceException
                .Where(_ => type.IsAssignableFrom(_) && !_.IsAbstract && _.IsClass);

            var missingTypeNames = knownTypes.Where(_ => !configuredTypes.Contains(_)).Select(_ => $"new {_.Name}(),")
                .ToArray();

            if (missingTypeNames.Any())
                throw new Exception(
                    $"The following types need to be added to {nameof(ConfigurationFixtures)}.{nameof(ConfigurationFixtures.GetAll)}:\n\n{string.Join(Environment.NewLine, missingTypeNames)} \n\n");
        }
    }
}