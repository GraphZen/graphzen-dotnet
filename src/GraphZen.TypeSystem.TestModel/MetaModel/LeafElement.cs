// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class LeafElement : Element
    {
        public LeafElement([NotNull] string name, [NotNull] ConfigurationScenarios scenarios) : base(name)
        {
            ConfigurationScenarios = scenarios;
        }

        public ConfigurationScenarios ConfigurationScenarios { get; }
        public override IEnumerator<Element> GetEnumerator() => Enumerable.Empty<Element>().GetEnumerator();
    }
}