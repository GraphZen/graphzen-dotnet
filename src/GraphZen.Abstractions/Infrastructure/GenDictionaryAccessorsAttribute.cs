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