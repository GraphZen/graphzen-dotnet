// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public abstract class Element
    {
        public Element([NotNull] string name, Type markerInterfaceType, Type mutableMarkerInterfaceType)
        {
            Name = name;
            MarkerInterfaceType = markerInterfaceType;
            MutableMarkerInterfaceType = mutableMarkerInterfaceType;
        }

        public Type MarkerInterfaceType { get; }
        public Type MutableMarkerInterfaceType { get; }


        public IReadOnlyList<string> Conventions { get; set; } = new List<string>();
        public string Name { get; }
    }
}