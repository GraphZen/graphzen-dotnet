// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

[assembly: InternalsVisibleTo("LINQPadQuery")]

namespace GraphZen.Infrastructure
{
    [AttributeUsage(AttributeTargets.Constructor)]
    internal class GenFactoryAttribute : Attribute
    {
        public Type FactoryType { get; }

        public GenFactoryAttribute(Type factoryType)
        {
            FactoryType = factoryType;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    internal class GenDictionaryAccessorsAttribute : Attribute
    {
        public string ValueName { get; }
        public string KeyName { get; }

        public GenDictionaryAccessorsAttribute(string keyName, string valueName)
        {
            KeyName = keyName;
            ValueName = valueName;
        }
    }
}