// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Interfaces.Fields;
using GraphZen.Objects.Fields;
using GraphZen.Objects.Fields.Arguments;

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
}