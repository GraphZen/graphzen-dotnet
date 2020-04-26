using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class SpecAttribute : Attribute
    {
        public SpecAttribute(string specId)
        {
            SpecId = specId;
        }

        public string SpecId { get; }
        public string? Subject { get; set; }
    }
}