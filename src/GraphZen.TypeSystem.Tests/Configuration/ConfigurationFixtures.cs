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
            // SCHEMA

            // OBJECT
            // Object fields collection
            new ObjectFields_Explicit(),
            new ObjectFields_ViaClrProperties(),

            // OBJECT FIELD
            // Object field arguments
            new ObjectField_Arguments_Explicit(),
            new InterfaceFields_Explicit(),
            new Object_ViaClrClass_Description(),
            new Object_Explicit_Description()

        }.OfType<T>();
    }
}