// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
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