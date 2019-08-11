// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public static class ElementExtensions
    {
        [NotNull]
        public static T Set<T>([NotNull]this T element, [NotNull] Action<T> elementAction) where T : Element
        {
            elementAction(element);
            return element;
        }

        [NotNull]
        public static T SetConventions<T>([NotNull]this T element, params string[] conventions) where T : Element
        {
            element.Conventions = conventions;
            return element;
        }
    }


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