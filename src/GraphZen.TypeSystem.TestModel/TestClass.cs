// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;

namespace GraphZen
{
    public class TestClass
    {
        public TestClass(string name, Element element)
        {
            Name = name;
            Element = element;
        }

        public Element Element { get; }

        public string Name { get; }
        public bool Generated { get; set; }
        public bool Abstract { get; set; }
        public List<string> Cases { get; } = new List<string>();
        public List<TestClass> SubClasses { get; } = new List<TestClass>();
    }
}