// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Utilities.Internal;


namespace GraphZen.TypeSystem
{
    internal struct StringLiteral : IInspectable
    {
        public StringLiteral(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public string GetDisplayValue() => Value;
    }
}