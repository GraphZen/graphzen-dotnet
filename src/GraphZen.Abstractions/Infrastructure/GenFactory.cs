// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    [AttributeUsage(AttributeTargets.Constructor)]
    internal class GenFactory : Attribute
    {
        public string FactoryClassName { get; }

        public GenFactory(string factoryClassName)
        {
            FactoryClassName = factoryClassName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal class GenAccessorExtensions : Attribute
    {
    }
}