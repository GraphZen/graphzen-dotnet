// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using GraphZen.Infrastructure;

namespace GraphZen.MetaModel
{
    public static class ElementExtensions
    {
        public static T Set<T>(this T element, [NotNull] Action<T> elementAction) where T : Element
        {
            elementAction(element);
            return element;
        }

        public static T SetConventions<T>(this T element, params string[] conventions) where T : Element
        {
            element.Conventions = conventions;
            return element;
        }

    }


    public abstract class Element 
    {
        public Element([NotNull] string name)
        {
            Name = name;
        }

        public IReadOnlyList<string> Conventions { get; set; } = new List<string>();
        public string Name { get; }
        public bool Optional { get; set; }
        public bool IsConfigurableViaDataAnnotation { get; set; }
    }
}