// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public class LeafElement : Element
    {
        private LeafElement([NotNull] string name, [NotNull] ConfigurationScenarios scenarios) : base(name)
        {
            ConfigurationScenarios = scenarios;
        }

        public Type MarkerInterface { get; private set; }
        public Type MutableMarkerInterface { get; private set; }
        public ConfigurationScenarios ConfigurationScenarios { get; }
        public override IEnumerator<Element> GetEnumerator() => Enumerable.Empty<Element>().GetEnumerator();

        [NotNull]
        public static LeafElement Create<TMarker, TMutableMarker>([NotNull] string name,
            [NotNull] ConfigurationScenarios scenarios, bool optional = false) where TMutableMarker : TMarker =>
            new LeafElement(name, scenarios)
            {
                MarkerInterface = typeof(TMarker),
                MutableMarkerInterface = typeof(TMutableMarker),
                Optional = optional
            };
    }
}